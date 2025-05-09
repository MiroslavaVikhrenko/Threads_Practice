namespace _20250509_Task2
{

    /*
     Напишите программу, в которой в главном потоке целочисленная переменная через 
    определенные промежутки получает случайное значение. Два дочерних потока периодически 
    (через определенные промежутки времени) проверяют значение переменной. 
    Первый поток проверяет, является ли значение переменной нечетным, 
    а второй поток проверяет, делится ли значение переменной на 3. 
    Если проверка успешная, то соответствующий поток выводит в консольное окно сообщение.
     */
    internal class Program
    {
        static int sharedValue = 0;
        static readonly object lockObject = new object();
        static bool running = true;

        static void Main(string[] args)
        {
            Random rand = new Random();

            // Start thread to check if value is odd
            Thread oddThread = new Thread(() =>
            {
                while (running)
                {
                    int value;
                    lock (lockObject)
                    {
                        value = sharedValue;
                    }

                    if (value % 2 != 0)
                    {
                        Console.WriteLine($"[OddThread] {value} is odd.");
                    }

                    Thread.Sleep(700); // Check every 700 ms
                }
            });

            // Start thread to check if value is divisible by 3
            Thread divisibleByThreeThread = new Thread(() =>
            {
                while (running)
                {
                    int value;
                    lock (lockObject)
                    {
                        value = sharedValue;
                    }

                    if (value % 3 == 0)
                    {
                        Console.WriteLine($"[DivByThreeThread] {value} is divisible by 3.");
                    }

                    Thread.Sleep(1000); // Check every 1000 ms
                }
            });

            // Start both threads
            oddThread.Start();
            divisibleByThreeThread.Start();

            // Main thread assigns new random values
            for (int i = 0; i < 20; i++) // Run 20 iterations
            {
                lock (lockObject)
                {
                    sharedValue = rand.Next(1, 101); // Random number between 1 and 100
                    Console.WriteLine($"[MainThread] New value assigned: {sharedValue}");
                }

                Thread.Sleep(500); // Assign new value every 500 ms
            }

            // Stop threads after loop
            running = false;

            // Wait for threads to finish
            oddThread.Join();
            divisibleByThreeThread.Join();

            Console.WriteLine("Program completed.");
        }
    }
}
