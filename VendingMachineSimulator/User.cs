using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineSimulator
{
    public class User
    {
        public User()
        {
            // Users start with a random amount of money and no items in their inventory
            money = new Random().Next(20, 1001); 
            inventory = new List<Product>();
        }

        private int money;
        List<Product> inventory;

        #region GetSetters
        public int Money { get => money; }
        #endregion

        public void ListInventory()
        {
            //Can't list what isn't there
            if(inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty");
                return;
            }

            // List all items in the users inventory
            for (int i = 0; i < inventory.Count; i++)
            {
                Console.WriteLine($"{i}. {inventory[i].Name}");
            }
        }

        public void AttemptInsert(IVending machine)
        {
            Console.WriteLine($"You have {money}SEK, what denomination of a bill/coin do you want to enter?");

            // Keep asking for an amount until the user accepts an agreeable amount
            bool insertAccepted = false;
            while (insertAccepted == false)
            {
                int insertAmount = -1;
                int.TryParse(Console.ReadLine(), out insertAmount);

                // Insert nothing
                if (insertAmount == 0)
                {
                    Console.WriteLine($"You entered no money, you still have {money}SEK");
                    break;
                }

                if (insertAmount > money)
                {
                    Console.WriteLine($"You don't have that much money, you have {money}SEK");
                    continue;
                }

                // If the inserted amount was accepted, subtract the amount and inform the user
                if (machine.InsertMoney(insertAmount) == true)
                {
                    insertAccepted = true;

                    money -= insertAmount;
                    Console.WriteLine($"You inserted {insertAmount}SEK, you now have {money}SEK");
                    machine.ShowMoneyAvailable();
                }
                else
                {
                    Console.WriteLine($"There is no such bill/coin in the current year ({DateTime.Now.AddYears(new Random().Next(30, 100)).Year})");
                }
            }

        }

        public void AttemptPurchase(IVending machine)
        {
            // List items for purchase, have the user enter an index and if there's enough money in the machine buy it

            Console.WriteLine("What would you like to buy? (use index #)");
            int moneyAvailable = machine.ShowMoneyAvailable();
            List<Product> products = machine.ShowAll();

            int userIndexInput = -1;

            while(int.TryParse(Console.ReadLine(), out userIndexInput) == false)
            {
                Console.WriteLine("Input not recognized as a whole number");
            }

            if (userIndexInput >= products.Count || userIndexInput < 0)
            {
                Console.WriteLine("There is no product with that index");
            }
            else
            {
                if (moneyAvailable < products[userIndexInput].Value)
                {
                    Console.WriteLine("There's not enough money available");
                }
                else
                {
                    Console.WriteLine($"The machine rumbles and the [{products[userIndexInput].Name}] falls into the output compartment");
                    machine.Purchase(products[userIndexInput].Name);
                }
            }
        }
        public void RequestChage(IVending machine)
        {
            // If there is any money aviable for use in the machine, put it in the output compartment

            if (machine.ShowMoneyAvailable() <= 0)
            {
                Console.WriteLine("No money has is available in the machine");
            }
            else
            {
                Console.WriteLine("There is a clinking and buzzing from the machine as it ejects your change into a compartment next to the control panel");
                machine.EndTransaction();
            }
        }

        public void CollectProducts(IVending machine)
        {
            // If any items were collected, inform the user
            List<Product> newProducts = machine.CollectProducts();
            inventory.AddRange(newProducts);

            if (newProducts.Count > 0)
            {
                for (int i = 0; i < newProducts.Count; i++)
                {
                    Console.WriteLine($"You collect the [{newProducts[i].Name}] from the output compartment and put it in your inventory.");
                }
            }
        }
        public void CollectChange(IVending machine)
        {
            // If any money was collected inform the user and update their waller
            List<int> change = machine.CollectMoney();

            if (change.Count > 0)
            {
                for (int i = 0; i < change.Count; i++)
                {
                    if (change[i] <= 10)
                    {
                        Console.WriteLine($"You add a {change[i]}SEK coin to your wallet.");
                    }
                    else
                    {
                        Console.WriteLine($"You add a {change[i]}SEK bill to your wallet.");
                    }
                    money += change[i];
                }

                Console.WriteLine($"You now have {money}SEK in your wallet");
            }
            else
            {
                Console.WriteLine("There was no change available.");
            }
        }

        public void ExamineItemInInventory()
        {
            // Can't examine something that isn't there
            if (inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty");
                return;
            }

            // List items in inventory
            Console.WriteLine("Which item do you wish to inspect? (use index #)");
            ListInventory();

            int userIndexInput = -1;

            while (int.TryParse(Console.ReadLine(), out userIndexInput) == false)
            {
                Console.WriteLine("Input not recognized as a whole number");
            }

            if (userIndexInput >= inventory.Count || userIndexInput < 0)
            {
                Console.WriteLine("There is no item  with that index");
            }
            else
            {
                inventory[userIndexInput].Examine();
            }

        }

        public void UseItemInInventory()
        {
            // Can't use something that isn't there
            if (inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty");
                return;
            }

            // List items in inventory
            Console.WriteLine("Which item do you wish to use? (use index #)");
            ListInventory();

            int userIndexInput = -1;

            while (int.TryParse(Console.ReadLine(), out userIndexInput) == false)
            {
                Console.WriteLine("Input not recognized as a whole number");
            }

            if (userIndexInput >= inventory.Count || userIndexInput < 0)
            {
                Console.WriteLine("There is no item  with that index");
            }
            else
            {
                inventory[userIndexInput].Use();
            }


        }
    }
}
