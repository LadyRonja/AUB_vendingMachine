using System;

namespace VendingMachineSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Simulator.Instance.Run();

            Console.WriteLine("End of program reached");
        }
    }
}
