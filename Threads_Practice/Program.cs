namespace Threads_Practice
{
    // Создать метод, выводящий на консоль каждую секунду, текущее время, в отдельном потоке.
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(Method);
            t.Start();
            Console.ReadKey();
        }

        static void Method()
        {
            while (true)
            {
                Console.WriteLine($"Current time: {DateTime.Now}");
                Thread.Sleep(1000);
            }
        }
    }
}
