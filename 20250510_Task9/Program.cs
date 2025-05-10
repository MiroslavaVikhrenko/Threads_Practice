namespace _20250510_Task9
{
    /*
     Создать массив, заполнить. Используя методы и отдельные потоки: определить среднее 
    арифметическое массива, подсчитать сумму массива. Использовать ключевое слово lock 
    (Первым должна выводиться сумма массива, а потом уже среднее арифметическое).
     */
    internal class Program
    {
        static int[] numbers;
        static int sum;
        static double average;
        static object lockObject = new object();
        static bool isSumCalculated = false;
        static void Main(string[] args)
        {
            FillArray(10); 

            Thread sumThread = new Thread(CalculateSum);
            Thread averageThread = new Thread(CalculateAverage);

            sumThread.Start();
            averageThread.Start();

            sumThread.Join();
            averageThread.Join();

        }
        static void FillArray(int size)
        {
            Random rand = new Random();
            numbers = new int[size];
            Console.WriteLine("Array elements:");
            for (int i = 0; i < size; i++)
            {
                numbers[i] = rand.Next(1, 101); 
                Console.Write(numbers[i] + " ");
            }
            Console.WriteLine("\n");
        }

        static void CalculateSum()
        {
            lock (lockObject)
            {
                sum = 0;
                foreach (int number in numbers)
                {
                    sum += number;
                }
                Console.WriteLine($"Sum of array: {sum}");
                isSumCalculated = true;
                Monitor.Pulse(lockObject); // Notify waiting threads
            }
        }

        static void CalculateAverage()
        {
            lock (lockObject)
            {
                while (!isSumCalculated)
                {
                    Monitor.Wait(lockObject); // Wait until sum is calculated
                }
                average = (double)sum / numbers.Length;
                Console.WriteLine($"Average of array: {average:F2}");
            }
        }

    }
}
