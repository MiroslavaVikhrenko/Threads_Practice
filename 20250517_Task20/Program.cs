namespace _20250517_Task20
{
    /*
    Используя асинхронный метод, считать текст из файла, определить количество символов, результат вывести на экран.
     */
    internal class Program
    {
        static async Task Main()
        {
            string filePath = "C:\\Users\\mvikh\\OneDrive\\Desktop\\exam notes - reflection.docx"; 

            try
            {
                string content = await ReadTextFromFileAsync(filePath);
                int charCount = content.Length;
                Console.WriteLine($"The file contains {charCount} characters.");
            }
            catch ( Exception ex ) 
            {
                Console.WriteLine($"{ex.Message}" );
            }
        }
        static async Task<string> ReadTextFromFileAsync(string path)
        {
            using StreamReader reader = new StreamReader(path);
            string text = await reader.ReadToEndAsync();
            return text;
        }
    }
}
