
    using System;  
    using System.Threading;  
    using System.Diagnostics;
    
    namespace locks  
    {  
    class Department  
        {  
            private Object thisLock = new Object();  
            int salary;  
            Random r = new Random();  
            public Department(int initial)  
            {  
                salary = initial;  
            }  
            int Withdraw(int amount)  
            {  
                // This condition never is true unless the lock statement  
                // is commented out.  
                if (salary < 0)  
                {  
                    throw new Exception("Negative Balance");  
                }  
                // Comment out the next line to see the effect of leaving out   
                // the lock keyword.  
                lock (thisLock)  
                {  
                    if (salary >= amount)  
                    {  
                        Console.WriteLine("salary before Withdrawal :  " + salary);  
                        Console.WriteLine("Amount to Withdraw        : -" + amount);  
                        salary = salary - amount;  
                        Console.WriteLine("salary after Withdrawal  :  " + salary);  
                        return amount;  
                    }  
                    else  
                    {  
                        return 0; // transaction rejected  
                    }  
                }  
            }  
            public void DoTransactions()  
            {  
                for (int i = 0; i < 100; i++)  
                {  
      
                    Withdraw(r.Next(1, 100));  
                }  
            }  
        }  
    class Process  
        {  
            static void Main()  
            {  
                Thread[] threads = new Thread[10];  
                Department dep = new Department(1000);   
               for (int i = 0; i < 10; i++)  
                {  
                   Thread t = new Thread(new ThreadStart(dep.DoTransactions));  
                    threads[i] = t;  
                }  
                for (int i = 0; i < 10; i++)  
                {  
                    threads[i].Start();  
                }  
                Console.Read();  
            }  
        }  
    }  
