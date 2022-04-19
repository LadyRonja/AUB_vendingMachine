using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineSimulator
{
    public interface IVending
    {
        public void Purchase(string productName);
        public List<Product> ShowAll();
        public bool InsertMoney(int amount);
        public void EndTransaction();

        public List<Product> CollectProducts();
        public List<int> CollectMoney();
        public int ShowMoneyAvailable();
    }
}
