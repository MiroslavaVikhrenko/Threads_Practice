namespace _20250506_Task1
{
    //Написать программу, в которой при запуске начинают одновременно работать 3 метода,
    //выводящие на экран числа в интервале от n  до z, с интервалом на вывод – полсекунды.
    //Значения n и z, передаются в метод.
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(() => PrintNumbers(1, 5));
            t1.Start();
            t1.Join();
            Thread t2 = new Thread(() => PrintNumbers(6, 10));
            t2.Start();
            t2.Join();
            Thread t3 = new Thread(() => PrintNumbers(11, 15));
            t3.Start();
            t3.Join();

        }

        static void PrintNumbers(int n, int z)
        {
            for (int i = n; i <= z; i++)
            {
                Console.WriteLine($"{i}");
                Thread.Sleep(500);
            }
        }
    }
}
