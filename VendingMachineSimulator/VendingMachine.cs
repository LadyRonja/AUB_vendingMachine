using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineSimulator
{
    public class VendingMachine : IVending
    {
        private readonly int[] allowedDenominations = new int[8] { 1, 5, 10, 20, 50, 100, 500, 1000 };
        private int availableMoney = 0;
        private List<Product> allProducts = new List<Product>();

        private List<Product> outputCompartmetProducts = new List<Product>();
        private List<int> outputCompartmentCash = new List<int>();

        #region GetSetters
        public int AvailableMoney { get => availableMoney; }
        #endregion

        public VendingMachine()
        {
            allProducts = ProductFactory.GetProducts(); // Upon creation stock the inventory
        }

        /// <summary>
        /// Turn all available money left in the machine into change and put it in the otput compartment for money
        /// </summary>
        public void EndTransaction()
        {
            // Assumes that the allowedDenominations is sorted in ascending order

            // While there is still money left int the pool
            // Try to give back the largest possible denomination
            // Do this until there isn't enough money left to give that denomination
            // Then repeat with the second largest denomination and repeat for each possible denomination

            // If 1 isn't a denomination there is a risk this algorithm hangs the program
            while (availableMoney != 0)
            {
                for (int i = 1; i <= allowedDenominations.Length; i++)
                {
                    int currentItteration = (allowedDenominations.Length - i);

                    while (availableMoney >= allowedDenominations[currentItteration])
                    {
                        outputCompartmentCash.Add(allowedDenominations[currentItteration]);
                        availableMoney -= allowedDenominations[currentItteration];
                    }
                }
            }
        }

        /// <summary>
        /// Insert a amount of money, if it's a correct denomination return true and increase the available funds.
        /// </summary>
        public bool InsertMoney(int amount)
        {
            // Ensure the entered amount is legal and if so return true and add it to the availableMoney,
            // if not return false and do not add it
            bool denominationValidated = false;

            for (int i = 0; i < allowedDenominations.Length; i++)
            {
                if (amount == allowedDenominations[i])
                {
                    denominationValidated = true;
                    availableMoney += amount;
                    break;
                }
            }

            return denominationValidated;
        }

        /// <summary>
        /// Puts a given product (identified by the name parameter) into the output compartment if enough funds are available in the machine
        /// </summary>
        public void Purchase(string productName)
        {
            // Check if the product requested exists in the machine
            // Then check if enough money is available
            // If so put the product in the output compartment and subtract it's cost
            for (int i = 0; i < allProducts.Count; i++)
            {
                if (string.Equals(allProducts[i].Name, productName))
                {
                   if (availableMoney >= allProducts[i].Value)
                    {
                        outputCompartmetProducts.Add(allProducts[i]);
                        availableMoney -= allProducts[i].Value;

                        // Remove the product from available purchase
                        allProducts.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Returns all products available for purchase in the machine, also prints their name and value to the console
        /// </summary>
        public List<Product> ShowAll()
        {
            // Print all contents and their prices
            for (int i = 0; i < allProducts.Count; i++)
            {
                Console.WriteLine($"{i}. [{allProducts[i].Name}] at the cost of [{allProducts[i].Value}]SEK");
            }

            // Print if the machine is empty
            if (allProducts.Count == 0)
            {
                Console.WriteLine($"The vending machine is empty");
            }

            return allProducts;
        }

        /// <summary>
        /// Returns all products that have been purchased and then clears the output compartment for products
        /// </summary>
        public List<Product> CollectProducts()
        {
            // Inform the user if there are no products
            if (outputCompartmetProducts.Count == 0)
            {
                Console.WriteLine("There are no products to collect in the compartment.");
            }

            // Return whats in the output compartment and then clear it.
            List<Product> output = new List<Product>();
            output.AddRange(outputCompartmetProducts);
            outputCompartmetProducts.Clear();

            return output;

        }

        /// <summary>
        /// Returns all money that have been put in the output compartment for cash and then clears it out
        /// </summary>
        public List<int> CollectMoney()
        {
            // Return whats in the output compartment and then clear it.
            List<int> output = new List<int>();
            output.AddRange(outputCompartmentCash);
            outputCompartmentCash.Clear();

            return output;
        }

        /// <summary>
        /// Returns the amount of money available in the machine, also prints it
        /// </summary>
        public int ShowMoneyAvailable()
        {
            Console.WriteLine($"There is {availableMoney}SEK at disposal in the machine");
            return availableMoney;
        }
    }
}
