using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineSimulator
{
    public class Simulator
    {

        // Multithreading is not used in this project,
        // thus spending time on making a thread-safe solution would be unnecessary
        #region Singleton
        private static Simulator instance = null;
       
        // Private constructor to prevent instances to be made
        private Simulator() { }
       
        public static Simulator Instance
        {
            get
            {
                if (instance == null) instance = new Simulator();
                return instance;
            }
        }
        #endregion

        private User user;
        private IVending vendingMachine;
        private bool shouldRun = true;

        private void OnStart()
        {
            // Set up for user to start using the vending machine
            user = new User();
            vendingMachine = new VendingMachine();
        }

        public void Run()
        {
            OnStart();


            Console.WriteLine($"You are standing infront of a vending machine, you have {user.Money}SEK on you");


            // While the simulation is running, take actions from the console and execute them as appropriate
            while (shouldRun)
            {
                PrintPossibleActions();
                TakeAction(Console.ReadLine());
            }
        }

        private void TakeAction(string input)
        {
            // Turn input to int index
            int actionIndex = 0;
            int.TryParse(input, out actionIndex);

            Console.Clear();


            switch (actionIndex)
            {
                case 1: // list products in vending machine
                    {
                        vendingMachine.ShowAll();
                        break;

                    }
                case 2: // list products in users inventory
                    {
                        user.ListInventory();
                        break;
                    }
                case 3: // check money in machine
                    {
                        vendingMachine.ShowMoneyAvailable();
                        break;
                    }
                case 4: // insert money
                    {
                        user.AttemptInsert(vendingMachine);
                        break;
                    }
                case 5: // attempt purchase
                    {
                        user.AttemptPurchase(vendingMachine);
                        break;
                    }
                case 6: // end transaction
                    {
                        user.RequestChage(vendingMachine);
                        break;
                    }
                case 7: // collect products
                    {
                        user.CollectProducts(vendingMachine);
                        break;
                    }
                case 8: // collect change
                    {
                        user.CollectChange(vendingMachine);
                        break;
                    }
                case 9: // examine an item
                    {
                        user.ExamineItemInInventory();
                        break;
                    }
                case 10: // use an item
                    {
                        user.UseItemInInventory();
                        break;
                    }
                case -1: // close application
                    {
                        shouldRun = false;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Action unrecognized");
                        break;
                    }
            }


            Console.WriteLine("\n Press any key to continue");
            Console.ReadKey(); 
            Console.Clear();

        }

        private void PrintPossibleActions()
        {
            Console.WriteLine("1.  Check what is on offer in the vending machine");
            Console.WriteLine("2.  Check what is in your inventory");
            Console.WriteLine("3.  Check how much money is inserted in the machine");
            Console.WriteLine("");
            Console.WriteLine("4.  Attempt to insert money in the machine");
            Console.WriteLine("5.  Attempt to make a purchase");
            Console.WriteLine("");
            Console.WriteLine("6.  Ask for your change back");
            Console.WriteLine("7.  Collect purchased products"); 
            Console.WriteLine("8.  Collect returned money");
            Console.WriteLine("");
            Console.WriteLine("9.  Examine an item in your inventory"); 
            Console.WriteLine("10. Use an item in your inventory"); 
            Console.WriteLine("");

            Console.WriteLine("-1. Close application");
        }
    }
}
