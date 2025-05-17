using System.IO.Compression;

namespace _20250517_Task24
{
    /*
     Создайте инструмент для сжатия файлов, который сжимает несколько файлов в каталоге. Вы хотите сжимать каждый файл асинхронно, 
    чтобы повысить производительность инструмента.
     */
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Define the path to the directory with files to compress
            string sourceDirectory = "C:\\Users\\mvikh\\OneDrive\\Desktop\\test"; //folder name

            if (!Directory.Exists(sourceDirectory))
            {
                Console.WriteLine($"Directory '{sourceDirectory}' does not exist.");
                return;
            }

            // Get all files in the directory (excluding already compressed ones)
            string[] files = Directory.GetFiles(sourceDirectory, "*.*", SearchOption.TopDirectoryOnly);

            // Create a list of compression tasks
            List<Task> compressionTasks = new List<Task>();

            foreach (var file in files)
            {
                // Skip already compressed files
                if (file.EndsWith(".gz")) continue;

                // Compress each file asynchronously
                compressionTasks.Add(CompressFileAsync(file));
            }

            // Await all compression tasks in parallel
            await Task.WhenAll(compressionTasks);

            Console.WriteLine("All files compressed.");
        }

        // Compresses a single file asynchronously using GZip.
        static async Task CompressFileAsync(string filePath)
        {
            string compressedFilePath = filePath + ".gz";

            // Open source file for reading
            using FileStream sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);

            // Open destination file for writing
            using FileStream destinationStream = new FileStream(compressedFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);

            // Create GZipStream for compression
            using GZipStream compressionStream = new GZipStream(destinationStream, CompressionMode.Compress);

            // Copy and compress data asynchronously
            await sourceStream.CopyToAsync(compressionStream);

            Console.WriteLine($"Compressed: {Path.GetFileName(filePath)} -> {Path.GetFileName(compressedFilePath)}");
        }
    }
}
