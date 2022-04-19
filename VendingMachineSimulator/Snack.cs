using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineSimulator
{
    public class Snack : Product
    {
        private bool expired;
        private string edibleDescription;
        private string inedibleDescription;

        public Snack(string name, int price, string edibleDescription, string inedibleDescription)
        {
            this.name = name;
            this.value = price;
            this.edibleDescription = edibleDescription;
            this.inedibleDescription = inedibleDescription;

            // The snack is expired 25% of the time
            this.expired = (new Random().Next(0, 4) == 0);
        }

        public override void Use()
        {
            if (expired)
            {
                Console.WriteLine("You eat some of the " + inedibleDescription);
            }
            else
            {
                Console.WriteLine("You eat the some of the " + edibleDescription);
            }
        }

        public override void Examine()
        {
            base.Examine();
            if (expired)
            {
                Console.WriteLine("It seems this expired 30 minutes ago");
            }
            else 
            {
                Console.WriteLine($"It seems this doesn't expire until {DateTime.Today.AddDays(3000)}");
            }
        }
    }
}
