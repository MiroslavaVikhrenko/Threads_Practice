namespace _20250509_Task6
{
    /*
     Напишите программу, которая создает несколько потоков, каждый из которых моделирует 
    работу клиента в банке. Каждый поток должен периодически совершать случайные операции 
    (пополнение или снятие средств) на своем счете. Программа должна работать в течение 
    заданного времени и выводить на экран текущее состояние счетов клиентов.

1) Создайте класс Account, который будет представлять банковский счет.
2) Создайте класс Client, который будет представлять клиента и содержать логику выполнения операций.
3) Используйте класс Thread для моделирования работы клиентов. Каждый клиент должен периодически 
    (каждые 1-3 секунды) совершать случайные операции (пополнение или снятие средств).
4) Программа должна завершиться через 30 секунд.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            int simulationTimeSeconds = 30;
            int clientCount = 5;

            List<Client> clients = new List<Client>();
            List<Thread> threads = new List<Thread>();

            // Create and start client threads
            for (int i = 1; i <= clientCount; i++)
            {
                var client = new Client($"Client_{i}");
                clients.Add(client);

                Thread thread = new Thread(client.Simulate);
                threads.Add(thread);
                thread.Start();
            }

            // Let the simulation run
            Thread.Sleep(simulationTimeSeconds * 1000);

            // Stop all clients
            foreach (var client in clients)
            {
                client.Stop();
            }

            // Wait for all threads to complete
            foreach (var thread in threads)
            {
                thread.Join();
            }

            // Display final balances
            Console.WriteLine("\n--- Final Account Balances ---");
            foreach (var client in clients)
            {
                Console.WriteLine($"{client.Name}: {client.Account.GetBalance()}");
            }

            Console.WriteLine("\nSimulation completed.");
        }
    }
}
