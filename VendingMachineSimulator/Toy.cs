using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineSimulator
{
    public class Toy : Product
    {
        private bool broken;
        private string playDescription;
        private string playBrokenDescription;

        public Toy(string name, int price, string playDescription, string playBrokenDescription)
        {
            this.name = name;
            this.value = price;
            this.playDescription = playDescription;
            this.playBrokenDescription = playBrokenDescription;

            // The toy is broken 25% of the time
            this.broken = (new Random().Next(0, 4) == 0);
        }

        public override void Use()
        {
            Console.WriteLine("");
            Console.Write($"You take out the {name}, ");
            if (broken)
            {
                Console.Write(playBrokenDescription);
            }
            else
            {
                Console.Write(playDescription);
            }
            Console.WriteLine("");
            
        }

        public override void Examine()
        {
            base.Examine();
            if (broken)
            {
                Console.WriteLine("It does not seem to be in the best shape");
            }
            else
            {
                Console.WriteLine("Yep, this is what you paid for alright");
            }
        }
    }
}
