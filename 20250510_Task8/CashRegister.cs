using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20250510_Task8
{
    public class CashRegister
    {
        private Queue<Customer> _customers;
        private int _id;

        public CashRegister(int id, Queue<Customer> customers)
        {
            _id = id;
            _customers = customers;
        }

        public void ProcessCustomers()
        {
            Random rand = new Random();
            while (_customers.Count > 0)
            {
                Customer customer;
                lock (_customers)
                {
                    customer = _customers.Dequeue();
                }

                Console.WriteLine($"[Register {_id}] Starting serving customer {customer.Id}");
                int processingTime = rand.Next(1000, 3001); // 1 to 3 sec

                Thread.Sleep(processingTime);

                Console.WriteLine($"[Register {_id}] Finishing serving customer {customer.Id}");
            }

            Console.WriteLine($"[Register {_id}] No more customers in queue.");
        }
    }
}
