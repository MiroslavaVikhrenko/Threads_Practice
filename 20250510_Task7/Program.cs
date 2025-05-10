using System.Net;

namespace _20250510_Task7
{
    /*
     Создать приложение, которое скачивает большой файл из Интернета. 
    Вы хотите, чтобы процесс скачивания происходил в отдельном потоке, 
    чтобы пользователь мог продолжать работать в приложении, пока файл скачивается.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "";
            string fileName = "4.png";

            Thread downloadThread = new Thread(() => DownloadFile(path, fileName));
            downloadThread.Start();

            while (downloadThread.IsAlive)
            {
                Console.WriteLine("Downloading...");
            }

            Console.WriteLine("Main thread completed.");
        }

        static void DownloadFile(string path, string filename)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(path, filename);
                    Console.WriteLine("Download completed successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Download failed: {ex.Message}");
            }
        }
    }
}
