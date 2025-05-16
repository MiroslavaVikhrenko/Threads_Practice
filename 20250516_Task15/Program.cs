namespace _20250516_Task15
{
    /*
     Создайте  приложение, которое ищет в некотором массиве: 

■ Минимум; 
■ Максимум; 
■ Среднее; 
■ Сумму. 

Используйте массив Task для решения поставленной задачи.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 12, 5, 9, 3, 22, 18, 7 };

            int min = 0, max = 0, sum = 0;
            double average = 0;

            Task[] tasks = new Task[4];

            tasks[0] = new Task(() =>
            {
                min = numbers.Min();
                Console.WriteLine($"Minimum: {min}");
            });

            tasks[1] = new Task(() =>
            {
                max = numbers.Max();
                Console.WriteLine($"Maximum: {max}");
            });

            tasks[2] = new Task(() =>
            {
                sum = numbers.Sum();
                Console.WriteLine($"Sum: {sum}");
            });

            tasks[3] = new Task(() =>
            {
                average = numbers.Average();
                Console.WriteLine($"Average: {average:F2}");
            });

            // Start each task manually
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
