namespace _20250510_Task11
{
    /*
     Используя Семафор, реализовать очередь на вход в кабинет, в кабинет одновременно может 
    быть только 3 человека. Всего желающих – 10. Заходить в кабинет могут, в каком угодно порядке.
     */
    internal class Program
    {
        // Semaphore allows up to 3 threads (people) at a time
        static Semaphore semaphore = new Semaphore(3, 3);
        static void Main(string[] args)
        {
            Console.WriteLine("Simulation started: 10 people want to enter the office (3 allowed at once)\n");

            for (int i = 1; i <= 10; i++)
            {
                int personId = i;
                Thread thread = new Thread(() => EnterOffice(personId));
                thread.Start();
               
                Thread.Sleep(100);
            }
        }
        static void EnterOffice(int personId)
        {
            Console.WriteLine($"Person {personId} is waiting to enter...");

            semaphore.WaitOne(); // Wait for a free slot (enter office)
            Console.WriteLine($"Person {personId} has entered the office.");

            // Simulate some time spent inside
            Thread.Sleep(new Random().Next(1000, 3000));

            Console.WriteLine($"Person {personId} is leaving the office.");
            semaphore.Release(); // Leave the office (free a slot)
        }
    }
}
