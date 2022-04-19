using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineSimulator
{
    public class Drink : Product
    {
        private bool carbonated;
        private bool shaken;

        public Drink(string name, int price, bool carbonated)
        {
            this.name = name;
            this.value = price;
            this.carbonated = carbonated;


            // The drink is shaken 25% of the time
            this.shaken = (new Random().Next(0, 4) == 0);
        }

        public override void Use()
        {
            if (carbonated && shaken)
            {
                Console.WriteLine("You open the drink and it errupts all over you. Should've tapped on the side first, dumbass.");
            }
            else if (carbonated && !shaken)
            {
                Console.WriteLine("You open the drink with a satisfying fizz, it quenches your first excellently");
            }
            else
            {
                Console.WriteLine("You drink it and it's quite refreshing");
            }
        }

        public override void Examine()
        {
            base.Examine();
            if (carbonated && shaken)
            {
                Console.WriteLine("Something feels off here...");
            }
            else if (!carbonated && shaken)
            {
                Console.WriteLine("It took a bit of a tumbling in the machine, luckily it's not carbonated");
            }
            else
            {
                Console.WriteLine("It looks refereshing");
            }
        }
    }
}
