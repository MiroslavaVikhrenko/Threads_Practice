namespace _20250516_Task13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create tasks with different ranges
            Task task1 = Task.Run(() => PrintRange(1, 10));
            Task task2 = Task.Run(() => PrintRange(11, 20));
            Task task3 = Task.Run(() => PrintRange(21, 30));

            // Wait for all tasks to finish
            Task.WaitAll(task1, task2, task3);

            Console.WriteLine("All tasks completed.");
            Console.ReadLine(); 
        }

        static void PrintRange(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                Console.WriteLine($"[{Task.CurrentId}] {i}");
                Task.Delay(100).Wait(); 
            }
        }
    }
}
