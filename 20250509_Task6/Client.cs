using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20250509_Task6
{
    public class Client
    {
        public string Name { get; }
        public Account Account { get; }

        private static readonly Random rand = new Random();
        private static readonly object randLock = new object();
        private bool running = true;

        public Client(string name)
        {
            Name = name;
            Account = new Account(rand.Next(100, 501)); // Initial balance 100–500
        }

        public void Stop()
        {
            running = false;
        }

        public void Simulate()
        {
            while (running)
            {
                int action;
                int amount;
                lock (randLock)
                {
                    action = rand.Next(0, 2); // 0: deposit, 1: withdraw
                    amount = rand.Next(10, 101); // Amount 10–100
                }

                if (action == 0)
                {
                    Console.WriteLine($"[{Name}] Depositing {amount}");
                    Account.Deposit(amount);
                }
                else
                {
                    Console.WriteLine($"[{Name}] Withdrawing {amount}");
                    Account.Withdraw(amount);
                }

                // Wait 1 to 3 seconds
                int delay;
                lock (randLock)
                {
                    delay = rand.Next(1000, 3001);
                }
                Thread.Sleep(delay);
            }

            Console.WriteLine($"[{Name}] Client simulation ended.");
        }
    }
}
