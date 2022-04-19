using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineSimulator
{
    public abstract class Product
    {
        protected string name;
        protected int value;

        #region GetSstters
        public string Name { get => name; }
        public int Value { get => value; }

        #endregion

        public abstract void Use();
        public virtual void Examine()
        {
            Console.WriteLine($"The {name} cost {value}SEK");
        }
    }
}
