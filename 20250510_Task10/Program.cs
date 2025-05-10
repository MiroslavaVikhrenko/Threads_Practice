namespace _20250510_Task10
{
    /*
     Предположим, у вас есть консольное приложение, имитирующее банк с несколькими банкоматами. 
    Каждый банкомат позволяет клиентам снимать деньги со своих счетов. Однако если два клиента 
    попытаются снять деньги одновременно, существует риск того, что основной баланс в банке, 
    станет отрицательным, в итоге выдача средств клиенту засчитается, но купюр он не получит.

Чтобы предотвратить это, вы можете использовать класс Monitor для синхронизации 
    доступа к переменной баланса счета.
     */
    internal class Program
    {
        static int accountBalance = 500; // Shared account balance
        static object lockObject = new object();
        static void Main(string[] args)
        {
            Console.WriteLine("Initial account balance: $" + accountBalance);
            Console.WriteLine();

            // Simulate 3 ATMs
            Thread atm1 = new Thread(() => Withdraw("ATM 1", 200));
            Thread atm2 = new Thread(() => Withdraw("ATM 2", 300));
            Thread atm3 = new Thread(() => Withdraw("ATM 3", 150));

            // Start all ATMs
            atm1.Start();
            atm2.Start();
            atm3.Start();

            // Wait for all to finish
            atm1.Join();
            atm2.Join();
            atm3.Join();

            Console.WriteLine($"\nFinal account balance: ${accountBalance}");
        }
        static void Withdraw(string atmName, int amount)
        {
            Console.WriteLine($"{atmName}: Attempting to withdraw ${amount}...");

            // Synchronize access to the balance
            bool lockTaken = false;

            /*
             Метод Monitor.Enter принимает два параметра - объект блокировки и значение типа bool, 
            которое указывает на результат блокировки (если он равен true, то блокировка успешно 
            выполнена). Фактически этот метод блокирует объект locker так же, как это делает оператор 
            lock. С помощью А в блоке try...finally с помощью метода Monitor.Exit происходит 
            освобождение объекта locker, если блокировка осуществлена успешно, и он становится 
            доступным для других потоков.
             */
            try
            {
                Monitor.Enter(lockObject, ref lockTaken);

                if (accountBalance >= amount)
                {
                    Console.WriteLine($"{atmName}: Withdrawal approved.");
                    Thread.Sleep(1000); // Simulate delay
                    accountBalance -= amount;
                    Console.WriteLine($"{atmName}: Withdrawal successful. Remaining balance: ${accountBalance}");
                }
                else
                {
                    Console.WriteLine($"{atmName}: Withdrawal denied. Insufficient funds.");
                }
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(lockObject);
                }
            }
        }
    }
}
