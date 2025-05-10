namespace _20250510_Task8
{
    /*
     Напишите программу, которая создает несколько касс (потоков), и каждый поток будет 
    обрабатывать очередь покупателей. 

Каждый покупатель будет обрабатываться за случайное время (от 1 до 3 секунд). 
    Программа должна выводить на экран, когда покупатель начинает и заканчивает обслуживание 
    на каждой кассе. 

1) Создайте класс Customer, который будет представлять покупателя. 
2) Создайте класс CashRegister, который будет представлять кассу и будет работать в отдельном потоке. 
3) Используйте класс Thread для моделирования работы касс. 
4) Сгенерируйте очередь из 20 покупателей и распределите 
     */
    internal class Program
    {     
        static void Main(string[] args)
        {
            int totalCustomers = 20;
            int registerCount = 3;

            Queue<Customer> customerQueue = new Queue<Customer>();
            for (int i = 1; i <= totalCustomers; i++)
            {
                customerQueue.Enqueue(new Customer(i));
            }

            List<Thread> threads = new List<Thread>();
            for (int i = 1; i <= registerCount; i++)
            {
                CashRegister register = new CashRegister(i, customerQueue);
                Thread thread = new Thread(register.ProcessCustomers);
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("All customers have been served");    
        }
    }
}
