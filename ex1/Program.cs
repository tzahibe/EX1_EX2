using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.Collections.Concurrent;

namespace ex1
{
    public class Program
    {
        static readonly int CASHIER_NUM = 5;
        static readonly object[] cashiers = new object[CASHIER_NUM];
        static ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        static Random rnd = new Random();


        static void ProduceCustomers()
        {
            int count = 0;
            while(true)
            {
                string threadName = "Customer " + (count + 1);
                Console.WriteLine(threadName + " is Enter to the Queue!");
                queue.Enqueue(threadName);
                Thread.Sleep(1000);
                count++;
            }
        }

        static void Main(string[] args)
        {
            Thread producingThread = new Thread(ProduceCustomers);
            producingThread.Start();

            for (int k = 0; k < CASHIER_NUM; k++)
            {
                cashiers[k] = new object();
            }

            for (int i = 0; i < CASHIER_NUM; i++)
            {
                int cashierNum = i;
                Thread cashierThread = new Thread(() => FindFreeCashier(cashierNum));
                cashierThread.Start();
            }


            //int customerCount = 0;

            //while (CUSTOMERS > customerCount)
            //{
            //    string threadName = "Customer " + (customerCount + 1);
            //    ThreadStart start = new ThreadStart(() => FindFreeCashier(threadName));
            //    Thread thr = new Thread(start);
            //    thr.Name = threadName;
            //    Monitor.Enter(queue);
            //    queue.Enqueue(thr);
            //    Console.WriteLine(threadName + " is waiting in the Queue");
            //    Monitor.Exit(queue);
            //    thr.Start();
            //    Thread.Sleep(1000);

            //    customerCount++;
            //}

            //Console.ReadKey();
        }
        public static void FindFreeCashier(int cashieNum)
        {
            string customerName = string.Empty;

            while (true)
            {
                if (queue.TryDequeue(out customerName))
                {
                    Console.WriteLine("Queue of: " + customerName);
                    Checkout_Cashier(customerName, cashieNum);
                }
            }
        }

        public static void Checkout_Cashier(string customerName, int cashieNum)
        {
            int randomNum = rnd.Next(1, 5);
            Console.WriteLine(customerName + " is Getting service in cashir number " + (cashieNum + 1));
            Console.WriteLine("Service " + (cashieNum + 1) + " will be busy for the next " + randomNum * 1000 + " seconds!");
            Thread.Sleep(randomNum * 1000);
            Console.WriteLine(customerName + " is Done in Service " + (cashieNum + 1) + "!");
        }
    }
}
