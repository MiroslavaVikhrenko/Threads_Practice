namespace _20250516_Task16
{
    /*
     Создайте приложение, в котором:
1) Первый task выводит сумму чисел от 1 до 100; 
2) Второй task записывает случайные 20 чисел в диапазоне от 1 до 100 в файл №1; 
3) Третий task считывает числа из файла №1, преобразует в двоичную систему и записывает в файл №2.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            string file1 = "numbers.txt";
            string file2 = "binary_numbers.txt";

            Task[] tasks = new Task[3];

            // Task 1: Sum from 1 to 100
            tasks[0] = new Task(() =>
            {
                int sum = Enumerable.Range(1, 100).Sum();
                Console.WriteLine($"Sum from 1 to 100: {sum}");
            });

            // Task 2: Generate 20 random numbers and write to file1
            tasks[1] = new Task(() =>
            {
                Random rand = new Random();
                int[] randomNumbers = new int[20];
                for (int i = 0; i < 20; i++)
                {
                    randomNumbers[i] = rand.Next(1, 101);
                }

                File.WriteAllLines(file1, randomNumbers.Select(n => n.ToString()));
                Console.WriteLine($"20 random numbers written to {file1}");
            });

            // Task 3: Read from file1, convert to binary, write to file2
            tasks[2] = new Task(() =>
            {
                // Wait for Task 2 to finish
                tasks[1].Wait();

                if (File.Exists(file1))
                {
                    string[] lines = File.ReadAllLines(file1);
                    string[] binaryLines = lines
                        .Select(line => Convert.ToString(int.Parse(line), 2))
                        .ToArray();

                    File.WriteAllLines(file2, binaryLines);
                    Console.WriteLine($"Binary numbers written to {file2}");
                }
                else
                {
                    Console.WriteLine("File not found. Cannot convert to binary.");
                }
            });

            // Start all tasks
            foreach (var task in tasks)
            {
                task.Start();
            }

            // Wait for all tasks to complete
            Task.WaitAll(tasks);

            Console.ReadLine();
        }
    }
}
