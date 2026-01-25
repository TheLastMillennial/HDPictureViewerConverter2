using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace HDPictureViewerConverter4
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<QueueItem> _queue = new();

        public MainWindow()
        {
            InitializeComponent();

            // Use ItemsSource instead of assigning to read-only Items
            QueueItemsControl.ItemsSource = _queue;

            // Init working directory
            if (!Directory.GetCurrentDirectory().Contains("working"))
            {
                Directory.CreateDirectory("./working");
                Directory.SetCurrentDirectory("./working");
            }

        }

        private async void AddPictureButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.AllowMultiple = true;
            dlg.Filters.Add(new FileDialogFilter { Name = "Images and GIFs", Extensions = { "png", "jpg", "jpeg", "bmp", "gif", "tiff" } });
            var res = await dlg.ShowAsync(this);
            if (res == null || res.Length == 0) return;
            foreach (var f in res)
            {
                if (File.Exists(f))
                {
                    _queue.Add(new QueueItem(f));
                }
            }
        }

        private void RemoveQueueItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is QueueItem qi)
            {
                _queue.Remove(qi);
            }
        }

        // New handler: opens a file explorer at the current working directory (cross-platform)
        private void FindConvertedButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var dir = Path.GetFullPath(Directory.GetCurrentDirectory());
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // explorer accepts a directory path as argument
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "explorer",
                        Arguments = $"\"{dir}\"",
                        UseShellExecute = true
                    });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "open",
                        Arguments = dir,
                        UseShellExecute = false
                    });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "xdg-open",
                        Arguments = dir,
                        UseShellExecute = false
                    });
                }
                else
                {
                    AppendLog("Unsupported OS platform for opening explorer.");
                }
            }
            catch (Exception ex)
            {
                AppendLog($"Failed to open explorer for '{dir}': {ex.Message}");
            }
        }


        private async void StartConversionButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            StartConversionButton.IsEnabled = false;
            AddPictureButton.IsEnabled = false;
            try
            {
                while (_queue.Count > 0)
                {
                    var item = _queue[0];
                    await ProcessQueueItemAsync(item);
                    // After success remove
                    _queue.RemoveAt(0);
                }
                AppendLog("All conversions completed.");
            }
            catch (Exception ex)
            {
                AppendLog($"Error during conversion: {ex.Message}");
            }
            finally
            {
                StartConversionButton.IsEnabled = true;
                AddPictureButton.IsEnabled = true;
            }
        }

        private async Task ProcessQueueItemAsync(QueueItem item)
        {
            AppendLog($"Starting processing: {item.FileName} (ID={item.ID})");
            ImageProgressBar.Value = 0;

            var ext = Path.GetExtension(item.FilePath).ToLowerInvariant();
            var createdFiles = new System.Collections.Generic.List<string>();

            try
            {
                var settings = await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    int chromaBits = 4;
                    try { chromaBits = (int)ChromaSlider.Value; } catch { }
                    int colorCount = Colors2.IsChecked == true ? 2
                                    : Colors4.IsChecked == true ? 4
                                    : Colors16.IsChecked == true ? 16
                                    : Colors65536.IsChecked == true ? 65536
                                    : 256;
                    return new ImageProcessingSettings(
                        ResizeMaintain: ResizeMaintain.IsChecked == true,
                        ResizeStretch: ResizeStretch.IsChecked == true,
                        ResizeDoNot: ResizeDoNot.IsChecked == true,
                        ColorCount: colorCount,
                        DitherLevel: DitherSlider.Value,
                        ChromaBits: chromaBits,
                        ImageName: item.FileName
                    );
                });

                // call background work without touching UI inside it
                List<(string, int)> framesData = new List<(string, int)>();
                await Task.Run(async () =>
                {
                    if (ext == ".gif")
                    {
                        // ProcessGifAsync now returns a list of (path, delayMs) tuples
                        framesData = await ProcessGifAsync(item, createdFiles, settings.ChromaBits);
                        await convertGif(framesData, item.ID, settings);
                    }
                    else
                    {
                        var files = await ProcessImageAsync(item, createdFiles, settings);
                        await convertImg(createdFiles, item.ID, settings);
                    }
                });

                AppendLog($"Conversion completed for {item.FileName} ({item.ID}), organizing output...");

                // move .8xv files to folder named after original image in same directory, overwrite existing folders
                var dir = Directory.GetCurrentDirectory();
                var destDir = Path.Combine(dir, Path.GetFileNameWithoutExtension(item.FilePath));
                try { Directory.Delete(destDir, true); } catch { }
                Directory.CreateDirectory(destDir);
                foreach (var f in Directory.GetFiles(dir, "*.8xv"))
                {
                    var dest = Path.Combine(destDir, Path.GetFileName(f));
                    try { File.Move(f, dest, overwrite: true); } catch { }
                }

                // delete created files and .yaml, .lst, .c, .h in dir
                foreach (var f in framesData)
                {
                    try { File.Delete(f.Item1); } catch { }
                }
                foreach (var f in createdFiles)
                {
                    try { File.Delete(f); } catch { }
                }
                foreach (var f in Directory.GetFiles(dir, "*.yaml")) try { File.Delete(f); } catch { }
                foreach (var f in Directory.GetFiles(dir, "*.lst")) try { File.Delete(f); } catch { }
                foreach (var f in Directory.GetFiles(dir, "*.c")) try { File.Delete(f); } catch { }
                foreach (var f in Directory.GetFiles(dir, "*.h")) try { File.Delete(f); } catch { }


                AppendLog($"Successfully processed {item.FileName}");
            }
            catch (Exception ex)
            {
                AppendLog($"Failed to process {item.FileName}: {ex.Message}");
            }
            finally
            {
                ImageProgressBar.Value = 100;
            }
        }

        private async Task<List<string>> ProcessImageAsync(QueueItem item, List<string> createdFiles, ImageProcessingSettings settings)
        {
            var results = new List<string>();
            using var image = SixLabors.ImageSharp.Image.Load<Rgba32>(item.FilePath);

            // Resize options
            if (settings.ResizeMaintain)
            {
                image.Mutate(x => x.Resize(new SixLabors.ImageSharp.Processing.ResizeOptions
                {
                    Size = new SixLabors.ImageSharp.Size(320, 240),
                    Mode = SixLabors.ImageSharp.Processing.ResizeMode.Max
                }));
            }
            else if (settings.ResizeStretch)
            {
                image.Mutate(x => x.Resize(320, 240));
            }

            // Create forPalette.png at most 2600x2600 maintaining aspect ratio
            if (image.Bounds().Width > 2600 || image.Bounds().Height > 2600)
            {
                using (var paletteImg = image.Clone(ctx => ctx.Resize(new SixLabors.ImageSharp.Processing.ResizeOptions { Size = new SixLabors.ImageSharp.Size(2600, 2600), Mode = SixLabors.ImageSharp.Processing.ResizeMode.Max })))
                {
                    var outDir = Directory.GetCurrentDirectory();
                    var palettePath = Path.Combine(outDir, "forPalette.png");
                    await paletteImg.SaveAsPngAsync(palettePath);
                    createdFiles.Add(palettePath);
                    results.Add(palettePath);
                }
            }
            else
            {
                using (var paletteImg = image.Clone())
                {
                    var outDir = Directory.GetCurrentDirectory();
                    var palettePath = Path.Combine(outDir, "forPalette.png");
                    await paletteImg.SaveAsPngAsync(palettePath);
                    createdFiles.Add(palettePath);
                    results.Add(palettePath);
                }
            }
            // Expand right and bottom with black to be divisible by 80
            int w = image.Width; int h = image.Height;
            int paddedW = ((w + 79) / 80) * 80;
            int paddedH = ((h + 79) / 80) * 80;
            var padded = new SixLabors.ImageSharp.Image<Rgba32>(paddedW, paddedH);
            // fill with black
            for (int yy = 0; yy < paddedH; yy++)
                for (int xx = 0; xx < paddedW; xx++)
                    padded[xx, yy] = new Rgba32(0, 0, 0, 255);

            // draw original image onto padded at 0,0
            padded.Mutate(ctx => ctx.DrawImage(image, new SixLabors.ImageSharp.Point(0, 0), 1f));

            // Split into 80x80 tiles
            int cols = paddedW / 80;
            int rows = paddedH / 80;
            var outDirectory = Directory.GetCurrentDirectory();
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    var rect = new SixLabors.ImageSharp.Rectangle(x * 80, y * 80, 80, 80);
                    using var tile = padded.Clone(ctx => ctx.Crop(rect));
                    var fname = item.ID + x.ToString("D3") + y.ToString("D3") + ".png";
                    var full = Path.Combine(outDirectory, fname);
                    await tile.SaveAsPngAsync(full);
                    createdFiles.Add(full);
                    results.Add(full);
                }
                // update progress
                var progress = (double)(y + 1) / rows * 100.0;
                await Dispatcher.UIThread.InvokeAsync(() => ImageProgressBar.Value = progress);
            }

            // TODO: Apply color reduction and dithering according to selected settings. Placeholder: log selection



            padded.Dispose();
            return results;
        }

        private byte QuantizeComponent(byte v, int levels)
        {
            if (levels <= 1) return 0;
            int bucket = (v * levels) / 256; // 0..levels-1
            if (bucket < 0) bucket = 0;
            if (bucket >= levels) bucket = levels - 1;
            // representative mapped to 0..255 with equal spacing
            if (levels == 1) return 0;
            if (levels == 256) return v;
            double rep = (levels == 1) ? 0.0 : (bucket * 255.0 / (levels - 1));
            return (byte)Math.Round(rep);
        }

        private void QuantizeImageAxisAligned(SixLabors.ImageSharp.Image<Rgba32> img, int bits)
        {
            int levels = 1 << bits;
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var p = img[x, y];
                    var rq = QuantizeComponent(p.R, levels);
                    var gq = QuantizeComponent(p.G, levels);
                    var bq = QuantizeComponent(p.B, levels);
                    img[x, y] = new Rgba32(rq, gq, bq, p.A);
                }
            }
        }

        // Return list of (filePath, delayMs) so convertGif can know each frame's duration
        private async Task<List<(string Path, int DelayMs)>> ProcessGifAsync(QueueItem item, List<string> createdFiles, int chromaBits)
        {
            var results = new System.Collections.Generic.List<(string Path, int DelayMs)>();
            using var gif = SixLabors.ImageSharp.Image.Load<Rgba32>(item.FilePath);
            int frameCount = gif.Frames.Count;

            // Resize to at most 160x120 preserving aspect ratio: apply to each frame
            var frames = new System.Collections.Generic.List<SixLabors.ImageSharp.Image<Rgba32>>();
            var delays = new System.Collections.Generic.List<int>();
            var padInfos = new System.Collections.Generic.List<(int Left, int Top, int Right, int Bottom)>();

            const int TargetW = 160;
            const int TargetH = 120;

            for (int i = 0; i < frameCount; i++)
            {
                var frame = gif.Frames.CloneFrame(i); // ImageFrame<Rgba32>

                // Read GIF frame delay from metadata (units are 1/100th second => *10 = ms)
                int frameDelayUnits = 0;
                try
                {
                    frameDelayUnits = gif.Frames[i].Metadata.GetGifMetadata().FrameDelay;
                }
                catch
                {
                    frameDelayUnits = 0;
                }
                int delayMs = frameDelayUnits * 10;

                // create an Image<Rgba32> and copy pixels from frame
                var imgFrame = new SixLabors.ImageSharp.Image<Rgba32>(frame.Width, frame.Height);
                for (int yy = 0; yy < frame.Height; yy++)
                {
                    for (int xx = 0; xx < frame.Width; xx++)
                    {
                        imgFrame[xx, yy] = frame[xx, yy];
                    }
                }

                // Resize to fit within 160x120 preserving aspect ratio
                imgFrame.Mutate(x => x.Resize(new SixLabors.ImageSharp.Processing.ResizeOptions { Size = new SixLabors.ImageSharp.Size(TargetW, TargetH), Mode = SixLabors.ImageSharp.Processing.ResizeMode.Max }));

                // If resized frame isn't exactly TargetW x TargetH, pad evenly on all sides with magenta (255,0,255)
                if (imgFrame.Width != TargetW || imgFrame.Height != TargetH)
                {
                    int padLeft = (TargetW - imgFrame.Width) / 2;
                    int padTop = (TargetH - imgFrame.Height) / 2;
                    int padRight = TargetW - imgFrame.Width - padLeft;
                    int padBottom = TargetH - imgFrame.Height - padTop;

                    var padded = new SixLabors.ImageSharp.Image<Rgba32>(TargetW, TargetH);
                    // fill with magenta sentinel
                    for (int yy = 0; yy < TargetH; yy++)
                        for (int xx = 0; xx < TargetW; xx++)
                            padded[xx, yy] = new Rgba32(255, 0, 255, 255);

                    // draw the resized frame centered
                    padded.Mutate(ctx => ctx.DrawImage(imgFrame, new SixLabors.ImageSharp.Point(padLeft, padTop), 1f));

                    frames.Add(padded);
                    padInfos.Add((padLeft, padTop, padRight, padBottom));

                    imgFrame.Dispose();
                }
                else
                {
                    frames.Add(imgFrame);
                    padInfos.Add((0, 0, 0, 0));
                }

                delays.Add(delayMs);
            }

            // Apply axis-aligned quantization controlled by chromaBits slider
            AppendLog($"Applying axis-aligned quantization: {chromaBits} bits per channel (levels={1 << chromaBits})");


            // Replace unchanged pixels with magenta (255,0,255) compared to the previous non-magenta pixel and save each frame
            SixLabors.ImageSharp.Image<Rgba32>? lastReal = null; // stores the most recent non-magenta pixel per position
            var outDirectory = Directory.GetCurrentDirectory();
            for (int i = 0; i < frames.Count; i++)
            {
                var fr = frames[i];
                int delayMs = delays[i];
                var pad = padInfos[i];

                // quantize current frame in-place using axis-aligned buckets
                QuantizeImageAxisAligned(fr, chromaBits);

                // Ensure padding region retains exact magenta sentinel after quantization
                if (pad.Left != 0 || pad.Top != 0 || pad.Right != 0 || pad.Bottom != 0)
                {
                    int w = fr.Width;
                    int h = fr.Height;
                    // top rows
                    for (int y = 0; y < pad.Top; y++)
                    {
                        for (int x = 0; x < w; x++)
                            fr[x, y] = new Rgba32(255, 0, 255, 255);
                    }
                    // bottom rows
                    for (int y = h - pad.Bottom; y < h; y++)
                    {
                        for (int x = 0; x < w; x++)
                            fr[x, y] = new Rgba32(255, 0, 255, 255);
                    }
                    // left/right columns in the middle area
                    for (int y = pad.Top; y < h - pad.Bottom; y++)
                    {
                        for (int x = 0; x < pad.Left; x++)
                            fr[x, y] = new Rgba32(255, 0, 255, 255);
                        for (int x = w - pad.Right; x < w; x++)
                            fr[x, y] = new Rgba32(255, 0, 255, 255);
                    }
                }

                // If this is the first frame, initialize lastReal with the quantized pixels and do not mark anything magenta
                if (lastReal == null)
                {
                    lastReal = fr.Clone();
                }
                else
                {
                    // Ensure lastReal is at least as large as current frame so we can compare every pixel in the overlap.
                    int targetW = Math.Max(lastReal.Width, fr.Width);
                    int targetH = Math.Max(lastReal.Height, fr.Height);
                    if (targetW != lastReal.Width || targetH != lastReal.Height)
                    {
                        var expanded = new SixLabors.ImageSharp.Image<Rgba32>(targetW, targetH);
                        // Fill expanded with fully transparent pixels (so they won't accidentally match)
                        for (int yy = 0; yy < targetH; yy++)
                            for (int xx = 0; xx < targetW; xx++)
                                expanded[xx, yy] = new Rgba32(0, 0, 0, 0);

                        // copy previous lastReal into expanded
                        for (int yy = 0; yy < lastReal.Height; yy++)
                            for (int xx = 0; xx < lastReal.Width; xx++)
                                expanded[xx, yy] = lastReal[xx, yy];

                        lastReal.Dispose();
                        lastReal = expanded;
                    }

                    int w = Math.Min(lastReal.Width, fr.Width);
                    int h = Math.Min(lastReal.Height, fr.Height);
                    for (int y = 0; y < h; y++)
                    {
                        for (int x = 0; x < w; x++)
                        {
                            var plast = lastReal[x, y];
                            var pcur = fr[x, y];
                            // Compare current pixel to the previous non-magenta pixel stored in lastReal.
                            if (plast.R == pcur.R && plast.G == pcur.G && plast.B == pcur.B && plast.A == pcur.A)
                            {
                                // unchanged -> mark magenta sentinel
                                fr[x, y] = new Rgba32(255, 0, 255, 255);
                            }
                            else
                            {
                                // changed -> update lastReal to this new non-magenta pixel
                                lastReal[x, y] = pcur;
                            }
                        }
                    }

                    // If current frame is larger than lastReal in either dimension, copy the extra non-overlapping pixels into lastReal
                    if (fr.Width > lastReal.Width || fr.Height > lastReal.Height)
                    {
                        int copyW = fr.Width;
                        int copyH = fr.Height;
                        var expanded = new SixLabors.ImageSharp.Image<Rgba32>(copyW, copyH);
                        // initialize expanded with transparent
                        for (int yy = 0; yy < copyH; yy++)
                            for (int xx = 0; xx < copyW; xx++)
                                expanded[xx, yy] = new Rgba32(0, 0, 0, 0);

                        // copy existing lastReal
                        for (int yy = 0; yy < lastReal.Height; yy++)
                            for (int xx = 0; xx < lastReal.Width; xx++)
                                expanded[xx, yy] = lastReal[xx, yy];

                        // copy current frame's non-magenta pixels into expanded
                        for (int yy = 0; yy < fr.Height; yy++)
                            for (int xx = 0; xx < fr.Width; xx++)
                            {
                                var p = fr[xx, yy];
                                // if current pixel is not the magenta sentinel, store it
                                if (!(p.R == 255 && p.G == 0 && p.B == 255 && p.A == 255))
                                {
                                    expanded[xx, yy] = p;
                                }
                            }

                        lastReal.Dispose();
                        lastReal = expanded;
                    }
                }

                var fname = item.ID + i.ToString("D6") + ".png";
                var full = Path.Combine(outDirectory, fname);
                await fr.SaveAsPngAsync(full);
                createdFiles.Add(full);
                results.Add((full, delayMs));

                var progress = (double)(i + 1) / frames.Count * 100.0;
                await Dispatcher.UIThread.InvokeAsync(() => ImageProgressBar.Value = progress);
            }

            // Make random frames grid for forPalette.png
            var rnd = new Random();
            int sampleCount = Math.Min(256, frames.Count);
            var sampleIndices = Enumerable.Range(0, frames.Count).OrderBy(x => rnd.Next()).Take(sampleCount).ToList();
            int gridCols = (int)Math.Ceiling(Math.Sqrt(sampleCount));
            int gridRows = (int)Math.Ceiling((double)sampleCount / gridCols);
            int cellW = frames[0].Width;
            int cellH = frames[0].Height;
            int gridW = cellW * gridCols;
            int gridH = cellH * gridRows;
            // scale to at most 2600x2600
            double scale = Math.Min(1.0, 2600.0 / Math.Max(gridW, gridH));
            int finalW = (int)(gridW * scale);
            int finalH = (int)(gridH * scale);

            var palette = new SixLabors.ImageSharp.Image<Rgba32>(gridW, gridH);
            // fill black
            for (int yy = 0; yy < gridH; yy++)
                for (int xx = 0; xx < gridW; xx++)
                    palette[xx, yy] = new Rgba32(0, 0, 0, 255);

            for (int idx = 0; idx < sampleIndices.Count; idx++)
            {
                int col = idx % gridCols;
                int row = idx / gridCols;
                var tile = frames[sampleIndices[idx]];
                palette.Mutate(ctx => ctx.DrawImage(tile, new SixLabors.ImageSharp.Point(col * cellW, row * cellH), 1f));
            }

            if (scale < 1.0)
            {
                palette.Mutate(ctx => ctx.Resize(finalW, finalH));
            }
            var palPath = Path.Combine(outDirectory, "forPalette.png");
            await palette.SaveAsPngAsync(palPath);
            createdFiles.Add(palPath);
            // Note: palette is not a frame, delay is 0
            results.Add((palPath, 0));

            // dispose frames
            foreach (var f in frames) f.Dispose();
            lastReal?.Dispose();
            palette.Dispose();

            return results;
        }

        // Empty convertImg function per requirement
        private async Task convertImg(List<string> savedFiles, string nameID, ImageProcessingSettings settings)
        {
            // Read UI settings on the UI thread because this method may be called from a background thread

            AppendLog($"convertImg called with {savedFiles.Count} files for ID={nameID} (stub). Settings: resize={settings.ResizeMaintain}, colors={settings.ColorCount}, dither={settings.DitherLevel:F1}");

            string yamlLinesList = "";
            string yamlPalettes = "";
            string yamlOutputsPal = "";
            string yamlOutputsImg = "\n\noutputs:";
            string yamlConverts = "\n\nconverts:";

            bool bIsPicture16BPP = settings.ColorCount == 65536;
            int numOfPaletteColors = settings.ColorCount;

            // get max image dimensions in squares
            string strImgDimensions = getImgMaxDimensions(savedFiles, nameID);
            // name of image truncated/padded to 8 chars
            string filename8 = settings.ImageName.Length >= 8 ? settings.ImageName.Substring(0, 8) : settings.ImageName.PadRight(8, ' ');


            if (!bIsPicture16BPP)
            {
                // 1,2,4, & 8 BPP use palettes
                yamlPalettes += (
                    "\npalettes:" +
                    "\n  - name: my_palette" +
                    "\n    max-entries: " + numOfPaletteColors +
                    "\n    fixed-entries:" +
                    "\n      - color: { index: 1,   r: 0,   g: 0,   b: 0}" +
                    "\n      - color: { index: 2, r: 255, g: 255, b: 255}" +
                    "\n    quality: 10" +
                    "\n    images:" +
                    "\n      - forPalette.png");

                int iBpp;
                switch (settings.ColorCount)
                {
                    case 2: iBpp = 1; break;
                    case 4: iBpp = 2; break;
                    case 16: iBpp = 4; break;
                    case 256: iBpp = 8; break;
                    default: iBpp = 8; break;
                }

                yamlOutputsPal += ("\n  - type: appvar" +
                            "\n    name: HP" + nameID + "0000" + //0000 used as placeholders, they don't signify anything
                            "\n    source-format: c" +
                            /*  e.g. HDPALV11 08 PuppySma PS 004003
                                palette version: HDPALV11
                                bpp: 08
                                Image name: PuppySma
                                Image ID: PS
                                Max size in squares: 004003
                            */
                            "\n    header-string: HDPALV110" + iBpp.ToString() + filename8 + nameID + strImgDimensions +
                            "\n    archived: true" +
                            "\n    palettes:" +
                            "\n      - my_palette"
                            );

                foreach (string filepath in savedFiles)
                {
                    if (filepath.Contains("forPalette.png"))
                        continue;

                    string filename = Path.GetFileNameWithoutExtension(filepath); // e.g. AB000003
                    string saveName = filename + ".png";
                    double ditherSetting = settings.DitherLevel;
                    //1,2,4, & 8 bpp
                    string ditherLine = ditherSetting == 0.0 ? "" : "\n    dither: " + ditherSetting.ToString();
                    yamlConverts += (
                        "\n  - name: " + filename +
                        "\n    palette: my_palette" +
                        "\n    images:" +
                        "\n      - " + saveName +
                        "\n    compress: zx0" +
                        ditherLine +
                        "\n    bpp: " + iBpp.ToString());
                    yamlOutputsImg += (
                        "\n  - type: appvar" +
                        "\n    name: " + filename +
                        "\n    source-format: c" +
                        "\n    header-string: " + " HDPICCV4" + filename8 +
                        "\n    archived: true" +
                        "\n    converts:" +
                        "\n      - " + filename);
                }
            }
            else
            {
                //16 bpp palette appvar only used for metadata
                yamlOutputsPal += ("\n  - type: appvar" +
                                "\n    name: HP" + nameID + "0000" + //0000 used as placeholders, they don't signify anything
                                "\n    source-format: c" +
                                /*  e.g. HDPALV11 16 PuppySma PS 004003
                                    palette version: HDPALV11
                                    bpp: 16
                                    Image name: PuppySma
                                    Image ID: PS
                                    Max size in squares: 004003
                                */
                                "\n    header-string: HDPALV1116" + filename8 + nameID + strImgDimensions +
                                "\n    archived: true"
                                );

                //Only the top leftmost image is notated with an A.
                //This makes it quick and easy to count the number of complete images on the calc
                String strVersion = "A";
                foreach (string filepath in savedFiles)
                {
                    if (filepath.Contains("forPalette.png"))
                        continue;

                    string filename = Path.GetFileNameWithoutExtension(filepath); // e.g. AB000003
                    string saveName = filename + ".png";
                    string strHorizVertSquares = filename.Substring(2, 6);// gets 000003

                    yamlConverts += (
                        "\n  - name: " + filename +
                        "\n    style: direct" +
                        "\n    color-format: rgb565" +
                        "\n    flip-x: true" +
                        "\n    rotate: 90" +
                        "\n    images:" +
                        "\n      - " + saveName +
                        "\n    compress: zx0");

                    yamlOutputsImg += (
                        "\n  - type: appvar" +
                        "\n    name: " + filename +
                        "\n    source-format: c" +
                        /* e.g. HDPIC16 A PuppySma PS 004003
                            picture version: HDPIC16
                            IsFirstImage? A = true, B = false
                            Image name: PuppySma
                            Image ID: PS
                            Max size in squares: 004003
                        */
                        "\n    header-string: " + " HDPIC16" + strVersion + filename8 + nameID + strHorizVertSquares +
                        "\n    archived: true" +
                        "\n    converts:" +
                        "\n      - " + filename);

                    // All other images notated with B
                    strVersion = "B";
                }
            }
            //todo: fix outputspal not working
            //Combine all YAML sections and write to convimg.yaml
            yamlLinesList = yamlPalettes + (yamlConverts) + (yamlOutputsImg) + (yamlOutputsPal);
            File.WriteAllText("convimg.yaml", yamlLinesList);
            Thread.Sleep(250); // ensure file write completes before starting convimg

            // Start convimg process based on OS
            LaunchConvimg();

            //Verify expected number of .8xv files created. 16bpp doesn't export a palette
            if (Directory.GetFiles(Directory.GetCurrentDirectory(), "*.8xv").Length < (bIsPicture16BPP ? savedFiles.Count - 1 : savedFiles.Count))
            {
                IOException e = new IOException("Convimg crash. Try restarting HD Picture Viewer Converter or save the picture as a separate .png file.");
                throw e;
            }
        }

        // convertGif now receives per-frame paths and delays (milliseconds)
        private async Task convertGif(System.Collections.Generic.List<(string Path, int DelayMs)> framesData, string nameID, ImageProcessingSettings settings)
        {
            AppendLog($"convertGif called with {framesData.Count} entries for ID={nameID} (stub).");

            string yamlPalette = "\npalettes:" +
                            "\n  - name: my_palette" +
                            "\n    fixed-entries:" +
                            "\n      - color: { index: 0, r: 255, g: 0,   b: 255}" +
                            "\n      - color: { index: 1, r: 0,   g: 0,   b: 0  }" +
                            "\n      - color: { index: 2, r: 255, g: 255, b: 255}" +
                            "\n    quality: 1" +
                            "\n    images:" +
                            "\n      - forPalette.png";

            string yamlConvert = "\n\nconverts:";
            string yamlOutputsImg = "\n\noutputs:";
            string yamlOutputsPal = "";

            string lastFilename = Path.GetFileNameWithoutExtension(framesData.Last().Path);

            foreach (var frame in framesData)
            {
                // skip palette image entry when generating per-frame converts/outputs
                if (Path.GetFileName(frame.Path).Equals("forPalette.png", StringComparison.OrdinalIgnoreCase))
                    continue;

                string filename = Path.GetFileNameWithoutExtension(frame.Path);
                string saveName = Path.GetFileName(frame.Path);
                // header-string uses 4-digit ms value (up to 9999)
                string headerString = frame.DelayMs.ToString("D4");

                yamlConvert +=
                    "\n  - name: " + filename +
                    "\n    palette: my_palette" +
                    "\n    images:" +
                    "\n      - " + saveName +
                    "\n    transparent-index: 0" +
                    "\n    compress: zx0";

                yamlOutputsImg +=
                    "\n  - type: appvar" +
                    "\n    name: " + filename +
                    "\n    source-format: c" +
                    "\n    header-string: " + headerString +
                    "\n    archived: true" +
                    "\n    converts:" +
                    "\n      - " + filename;
            }

            string filename8 = settings.ImageName.Length >= 8 ? settings.ImageName.Substring(0, 8) : settings.ImageName.PadRight(8, ' ');


            yamlOutputsPal += "\n  - type: appvar" +
                            "\n    name: HP" + nameID + "0000" + //0000 used as placeholders, they don't signify anything
                            "\n    source-format: c" +
                            /*HDGIFV01 Poppy___ PA012345 
                             *Version: HDGIFV01 
                             *Image name: Poppy___
                             * Image ID: PA
                             * Frame number: 012345 (-2 accounts for 0 index and excluding palette
                             */
                            "\n    header-string: HDGIFV01" + filename8 + nameID + (framesData.Count - 2).ToString("D6") +
                            "\n    archived: true" +
                            "\n    palettes:" +
                            "\n      - my_palette";

            var yamlCombined = yamlPalette + (yamlConvert) + (yamlOutputsImg) + (yamlOutputsPal);
            File.WriteAllText("convimg.yaml", yamlCombined);
            Thread.Sleep(250);

            LaunchConvimg();

            // Verify expected number of .8xv files created (exclude the palette entry)
            int expected = framesData.Count(f => !Path.GetFileName(f.Path).Equals("forPalette.png", StringComparison.OrdinalIgnoreCase));
            if (Directory.GetFiles(Directory.GetCurrentDirectory(), "*.8xv").Length < expected)
            {
                IOException e = new IOException("Convimg crash. Try restarting HD Picture Viewer Converter or save the picture as a separate .png file.");
                throw e;
            }

            await Task.CompletedTask;
        }

        private void LaunchConvimg()
        {
            Process convimgRunning;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                convimgRunning = Process.Start("convimg.exe");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                convimgRunning = Process.Start("convimg.osx");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                convimgRunning = Process.Start("convimg.linux");
            }
            else
            {
                AppendLog("Unsupported OS platform for convimg.");
                return;
            }

            convimgRunning.WaitForExit();
        }

        private void AppendLog(string message)
        {
            Dispatcher.UIThread.Post(() =>
            {
                ImageLog.Text += DateTime.Now.ToString("HH:mm:ss") + " - " + message + Environment.NewLine;
                // optionally scroll - TextBox will show latest
            });
        }

        private string getImgMaxDimensions(List<string> savedFiles, string nameID)
        {
            int iMaxImgDimensions = 0;
            int prevMaxImgDimensions = 0;
            string strImgDimensions = "";
            foreach (string filepath in savedFiles)
            {
                if (filepath.Contains("forPalette.png"))
                    continue;
                string strDim = Path.GetFileNameWithoutExtension(filepath).Substring(2, 6);
                if (Int32.TryParse(strDim, out iMaxImgDimensions))
                {
                    if (iMaxImgDimensions > prevMaxImgDimensions)
                        strImgDimensions = strDim;
                }
                prevMaxImgDimensions = iMaxImgDimensions;
            }
            return strImgDimensions;
        }
    }

    public class QueueItem : INotifyPropertyChanged
    {
        private static readonly Random _rnd = new();
        private static readonly Regex _validId = new("^[A-Z][A-Z0-9]$");

        public string FilePath { get; }
        public string FileName => Path.GetFileName(FilePath);

        private string _id;
        public string ID
        {
            get => _id;
            set
            {
                var v = (value ?? string.Empty).ToUpper();
                if (v.Length == 2 && _validId.IsMatch(v))
                {
                    _id = v;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
                }
                // ignore invalid values
            }
        }

        public QueueItem(string path)
        {
            FilePath = path;
            _id = GenerateRandomId();
        }

        private static string GenerateRandomId()
        {
            char c1 = (char)('A' + _rnd.Next(0, 26));
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char c2 = chars[_rnd.Next(chars.Length)];
            return new string(new[] { c1, c2 });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    // new DTO
    public record ImageProcessingSettings(
        bool ResizeMaintain,
        bool ResizeStretch,
        bool ResizeDoNot,
        int ColorCount,
        double DitherLevel,
        int ChromaBits,
        string ImageName
    );
}