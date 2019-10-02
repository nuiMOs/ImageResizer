using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();

            Stopwatch sw = new Stopwatch();

            for (int i = 1; i <= 10; i++)
            {
                // Sync
                imageProcess.Clean(destinationPath);
                sw.Restart();
                imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
                sw.Stop();

                var syncMs = sw.ElapsedMilliseconds;
                Console.WriteLine($"第{i}次 Sync 花費時間: {syncMs} ms");

                // Sync
                //imageProcess.Clean(destinationPath);
                //sw.Restart();
                //imageProcess.ResizeImagesPLNQ(sourcePath, destinationPath, 2.0);
                //sw.Stop();

                //var syncPLINQMs = sw.ElapsedMilliseconds;
                //Console.WriteLine($"第{i}次 Sync with PLINQ 花費時間: {syncPLINQMs} ms");

                // Async
                imageProcess.Clean(destinationPath);
                sw.Restart();
                await imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0);
                sw.Stop();

                var asyncMs = sw.ElapsedMilliseconds;
                Console.WriteLine($"第{i}次 Async 花費時間: {asyncMs} ms");
                Console.WriteLine($"調整比率: {((syncMs - asyncMs) / (double)syncMs * 100).ToString("#.#")} %");

                // Async with PLINQ
                //imageProcess.Clean(destinationPath);
                //sw.Restart();
                //await imageProcess.ResizeImagesPLNQAsync(sourcePath, destinationPath, 2.0);
                //sw.Stop();

                //var asyncPLINQMs = sw.ElapsedMilliseconds;
                //Console.WriteLine($"第{i}次 Async with PLINQ 花費時間: {asyncPLINQMs} ms");
                //Console.WriteLine($"調整比率: {((syncMs - asyncPLINQMs) / (double)syncMs * 100).ToString("#.#")} %");
            }

            Console.ReadKey();
        }
    }
}
