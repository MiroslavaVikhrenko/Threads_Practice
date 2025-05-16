namespace _20250516_Task14
{
    /*
     Создайте приложение, использующее класс «Task». Приложение должно отображать текущее время и дату. Запустите задачу три способами: 

■ Через метод Start класса Task; 
■ Через метод Task.Factory.StartNew; 
■ Через метод Task.Run.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Using Task + Start()
            Task task1 = new Task(() => DisplayDateTime("Task.Start()"));
            task1.Start();

            // 2. Using Task.Factory.StartNew()
            Task task2 = Task.Factory.StartNew(() => DisplayDateTime("Task.Factory.StartNew()"));

            // 3. Using Task.Run()
            Task task3 = Task.Run(() => DisplayDateTime("Task.Run()"));

            // Wait for all tasks to complete
            Task.WaitAll(task1, task2, task3);

            Console.WriteLine("All tasks completed.");
            Console.ReadLine();
        }
        // Method to display current date and time
        static void DisplayDateTime(string methodType)
        {
            Console.WriteLine($"[{methodType}] Current Date and Time: {DateTime.Now}");
        }
    }
}
