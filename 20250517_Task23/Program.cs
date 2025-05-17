namespace _20250517_Task23
{
    /*
     У вас есть несколько текстовых файлов, содержащих данные о продажах, и вам нужно обработать их асинхронно. 
    Каждый файл содержит записи, которые необходимо обработать и агрегировать данные по каждой категории (например, по товарам или регионам). 
    Задача заключается в том, чтобы асинхронно читать данные из нескольких файлов, обрабатывать их и выводить итоговый отчет по категориям.
1) Напишите метод, который будет читать данные из текстовых файлов асинхронно.
2) Напишите метод, который будет обрабатывать данные и агрегировать их по категориям (например, по товарам).

Содержимое файлов:
Electronics,150.50
Clothing,75.00
Electronics,200.25
Home,50.00
Clothing,120.75
     */
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Assume we have several text files with sales data
            string[] filePaths = {
                "C:\\Users\\mvikh\\OneDrive\\Desktop\\sales1.txt",
                "C:\\Users\\mvikh\\OneDrive\\Desktop\\sales2.txt"

            };

            // Read and process all files asynchronously
            List<string[]> fileContents = new List<string[]>();

            foreach (var filePath in filePaths)
            {
                string[] lines = await ReadFileAsync(filePath);
                fileContents.Add(lines);
            }

            // Flatten all lines into one list and process them
            List<string> allLines = fileContents.SelectMany(lines => lines).ToList();

            // Aggregate sales by category
            Dictionary<string, double> salesByCategory = AggregateSales(allLines);

            // Print the results
            Console.WriteLine("Sales Report by Category:");
            foreach (var category in salesByCategory)
            {
                Console.WriteLine($"{category.Key}: {category.Value:F2}");
            }
        }

        // Asynchronously reads all lines from a file.
        static async Task<string[]> ReadFileAsync(string filePath)
        {
            // Read all lines from the file asynchronously
            return await File.ReadAllLinesAsync(filePath);
        }

        // Processes lines and aggregates sales by category.
        static Dictionary<string, double> AggregateSales(List<string> lines)
        {
            var result = new Dictionary<string, double>();

            foreach (string line in lines)
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Split line into category and amount
                string[] parts = line.Split(',');

                if (parts.Length != 2)
                    continue; // Invalid line format

                string category = parts[0].Trim();
                if (!double.TryParse(parts[1], out double amount))
                    continue; // Invalid amount

                // Add or update the amount for this category
                if (result.ContainsKey(category))
                    result[category] += amount;
                else
                    result[category] = amount;
            }

            return result;
        }
    }
}
