using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20250509_Task6
{
    public class Account
    {
        private object balanceLock = new object();
        public int Balance { get; private set; }

        public Account(int initialBalance = 0)
        {
            Balance = initialBalance;
        }

        public void Deposit(int amount)
        {
            lock (balanceLock)
            {
                Balance += amount;
                Console.WriteLine($"[Deposit] +{amount} => Balance: {Balance}");
            }
        }

        public void Withdraw(int amount)
        {
            lock (balanceLock)
            {
                if (Balance >= amount)
                {
                    Balance -= amount;
                    Console.WriteLine($"[Withdraw] -{amount} => Balance: {Balance}");
                }
                else
                {
                    Console.WriteLine($"[Withdraw] Failed (-{amount}) => Insufficient funds (Balance: {Balance})");
                }
            }
        }

        public int GetBalance()
        {
            lock (balanceLock)
            {
                return Balance;
            }
        }
    }
}
