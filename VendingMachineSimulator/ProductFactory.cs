using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineSimulator
{
    public abstract class ProductFactory
    {
        public static List<Product> GetProducts()
        {
            List<Product> output = new List<Product>();
            
            // Add some drinks
            output.Add(MakeADrink("Bebsi Max", 20, true));
            output.Add(MakeADrink("Luka Citron", 15, true));
            output.Add(MakeADrink("Peppar, M.D.", 18, true));
            output.Add(MakeADrink("Mindre (apelsin)", 16, false));

            // Add some snacks
            output.Add(MakeASnack("Slappbrunch", 22, "chocolate covered wafer", "melted wafer covered in chocolate"));
            output.Add(MakeASnack("Punchrulle", 12, "chocolate based pastery surrounded by marzipan and dipped on both ends in melted chocolate", "green thing that's as hard as a plastic vacuum cleaner and tastes like the contents of one too"));
            output.Add(MakeASnack("BMW Grillchips", 13, "crispy chips with a mild barbeque flavor", "what used to be a potato, although now it more looks like saw dust"));

            // Add some toys
            output.Add(MakeAToy("Jojo", 35, "it slides up and down without problem, you manage a few tricks before you put it away", "it has been shattared and looks like the slices of an orange... that's impressive but it's unusable"));
            output.Add(MakeAToy("Bear Plushie", 30, "it's very soft and holding it makes you feel just a little better about everything", "a seam has burst and it's arm is loose, it's still good for hugging and somehow you feel even more attached to it than you think you would if it wasn't broken - you'll remember to be gentle with the little guy"));
            
            return output;
        }

        private static Toy MakeAToy(string name, int price, string playDescription, string brokenPlayDescription) 
        {
            return new Toy(name, price, playDescription, brokenPlayDescription);
        }
        private static Drink MakeADrink(string name, int price, bool carbonated)
        {
            return new Drink(name, price, carbonated);
        }
        private static Snack MakeASnack(string name, int price, string edibleDescription, string inedibleDescription)
        {
            return new Snack(name, price, edibleDescription, inedibleDescription);
        }
    }
}
