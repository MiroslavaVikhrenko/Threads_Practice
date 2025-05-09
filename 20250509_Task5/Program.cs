namespace _20250509_Task5
{
    /*
     Напишите программу, которая создает несколько потоков, каждый из которых моделирует 
    датчик температуры в отдельной комнате. Каждый поток должен периодически генерировать 
    и выводить случайные значения температуры для своей комнаты. 
    Программа должна остановить все потоки через заданное время.
     */
    internal class Program
    {
        static bool running = true;
        static readonly object randLock = new object();
        static Random rand = new Random();
        static void Main(string[] args)
        {
            int numberOfRooms = 5;
            int runTimeSeconds = 10;

            List<Thread> sensorThreads = new List<Thread>();

            for (int i = 1; i <= numberOfRooms; i++)
            {
                int roomId = i; // Capture local copy for closure
                Thread sensorThread = new Thread(() => SimulateTemperatureSensor(roomId));
                sensorThreads.Add(sensorThread);
                sensorThread.Start();
            }

            // Let the simulation run for specified time
            Thread.Sleep(runTimeSeconds * 1000);

            // Stop all threads
            running = false;

            // Wait for all threads to finish
            foreach (var thread in sensorThreads)
            {
                thread.Join();
            }

            Console.WriteLine("\nAll sensors stopped.");
        }

        static void SimulateTemperatureSensor(int roomId)
        {
            while (running)
            {
                int temperature;
                lock (randLock)
                {
                    temperature = rand.Next(-10, 36); // Simulate -10°C to 35°C
                }

                Console.WriteLine($"[Room {roomId}] Temperature: {temperature}°C");
                Thread.Sleep(1000); // Simulate sensor reading every 1 second
            }

            Console.WriteLine($"[Room {roomId}] Sensor stopped.");
        }
    }
}
