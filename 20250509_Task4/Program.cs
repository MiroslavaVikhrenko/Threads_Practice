namespace _20250509_Task4
{
    /*
     Напишите программу, содержащую класс с двумя полями: 
    одно является ссылкой на целочисленный массив, а второе поле является ссылкой 
    на символьный массив. Создайте объект класса, а также запустите на выполнение два 
    дочерних потока. Один дочерний поток должен заполнить символьный массив объекта, 
    а второй дочерний поток должен заполнить целочисленный массив объекта. 
    Способ заполнения предложите самостоятельно (например, случайные символы и числа).
     */
    internal class Program
    {
        static Random rand = new Random(); // Shared random generator
        static readonly object randLock = new object(); // To avoid race condition with Random
        static void Main(string[] args)
        {
            int arraySize = 10;
            DataHolder holder = new DataHolder(arraySize);

            Thread intThread = new Thread(() =>
            {
                for (int i = 0; i < holder.IntArray.Length; i++)
                {
                    lock (randLock)
                    {
                        holder.IntArray[i] = rand.Next(1, 101); // Random number 1–100
                    }
                    Console.WriteLine($"[IntThread] IntArray[{i}] = {holder.IntArray[i]}");
                    Thread.Sleep(50); // Simulate work
                }
            });

            Thread charThread = new Thread(() =>
            {
                for (int i = 0; i < holder.CharArray.Length; i++)
                {
                    lock (randLock)
                    {
                        holder.CharArray[i] = (char)rand.Next('A', 'Z' + 1); // Random uppercase letter
                        // 'A' is the ASCII value 65
                        // 'Z' is the ASCII value 90
                        // rand.Next('A', 'Z' + 1) becomes: rand.Next(65, 91)
                        // This call generates a random number from 65 to 90 inclusive,
                        // which maps to characters 'A' to 'Z'.
                        // So without the + 1, 'Z' (90) would never be included in the result.
                    }
                    Console.WriteLine($"[CharThread] CharArray[{i}] = {holder.CharArray[i]}");
                    Thread.Sleep(50); // Simulate work
                }
            });

            // Start both threads
            intThread.Start();
            charThread.Start();

            // Wait for both to complete
            intThread.Join();
            charThread.Join();

            Console.WriteLine("\nFinal arrays:");
            Console.WriteLine("Integers: " + string.Join(", ", holder.IntArray));
            Console.WriteLine("Characters: " + string.Join(", ", holder.CharArray));
        }
    }
    class DataHolder
    {
        public int[] IntArray;
        public char[] CharArray;

        public DataHolder(int size)
        {
            IntArray = new int[size];
            CharArray = new char[size];
        }
    }
}
