using System.Net;
using System;

namespace _20250517_19
{
    /*
     У вас есть список URL-адресов, которые нужно загрузить параллельно, используя несколько потоков на C#. 
    В программе должна быть возможность отменить операцию, если она занимает слишком много времени или если пользователь решит ее отменить. 
    Напишите программу, которая загружает файлы, используя TPL и CancellationToken.

     */
    internal class Program
    {
        static async Task Main(string[] args)
        {
            List<string> urls = new List<string>
            {
                "https://letsenhance.io/static/73136da51c245e80edc6ccfe44888a99/1015f/MainBefore.jpg",
                "https://static.vecteezy.com/system/resources/thumbnails/036/324/708/small/ai-generated-picture-of-a-tiger-walking-in-the-forest-photo.jpg",
                "https://img.freepik.com/free-photo/closeup-scarlet-macaw-from-side-view-scarlet-macaw-closeup-head_488145-3540.jpg?semt=ais_hybrid&w=740"
            };
            //WebClient client = new WebClient();
            //client.DownloadFile(url, $"{new DomainCutter().CleanUrl(url)}.txt");

            using CancellationTokenSource cts = new CancellationTokenSource();

            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("User wants to cancel...");
                cts.Cancel();
                e.Cancel = true;
            };

            try
            {
                await DownloadImageAsync(urls, cts.Token);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("Cancelled");
            }

            Console.ReadLine();
        }

        static async Task DownloadImageAsync(List<string> urls, CancellationToken token)
        {
            List<Task> downloadTasks = new List<Task>();

            foreach (string url in urls)
            {
                downloadTasks.Add(Task.Run(() => {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine($"Cancelled download for {url}");
                        token.ThrowIfCancellationRequested();
                    }
                    try
                    {
                        using WebClient client = new WebClient();
                        string fileName = $"{new DomainCutter().CleanUrl(url)}.txt";
                        client.DownloadFile(url, fileName);
                        Console.WriteLine($"Downloaded: {url}");
                    }
                    catch (OperationCanceledException ex)
                    {
                        Console.WriteLine($"{url} Download cancelled");
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine($"{url} Exception details: {ex.Message}");
                    }
                }, token));

                await Task.WhenAll(downloadTasks);
            }
        }
    }

    public class DomainCutter
    {
        public string CleanUrl(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                return uri.Host.Replace(".", "_") + uri.AbsolutePath.Replace("/", "_").Trim('_');
            }
            catch
            {
                return "invalid_url";
            }
        }

    }
}
