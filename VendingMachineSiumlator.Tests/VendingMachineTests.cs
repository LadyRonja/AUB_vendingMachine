using System;
using Xunit;
using VendingMachineSimulator;
using System.Collections.Generic;

namespace VendingMachineSiumlator.Tests
{
    public class VendingMachineTests
    {
        [Fact]
        public void Does_Not_Add_Faulty_Denominations()
        {
            // Arrange
            IVending vm = new VendingMachine();
            int illegalDenomination = 3;
            int startMoney = vm.ShowMoneyAvailable();

            // Act
            bool approval = vm.InsertMoney(illegalDenomination);
            int moneyAdded = vm.ShowMoneyAvailable() - startMoney;

            // Assert
            Assert.Equal(0, moneyAdded);
        }
        
        [Fact]
        public void Adds_Legal_Inserts_To_Available_Money()
        {
            // Arrange
            IVending vm = new VendingMachine();
            int legalInsert = 20;
            int startMoney = vm.ShowMoneyAvailable();

            // Act
            bool approval = vm.InsertMoney(legalInsert);
            int moneyAdded = vm.ShowMoneyAvailable() - startMoney;

            // Assert
            Assert.Equal(legalInsert, moneyAdded);

        }

        [Fact]
        public void Does_Not_Confirm_Illegal_Denominations()
        {
            // Arrange
            IVending vm = new VendingMachine();
            int illegalDenomination = 3;

            // Act
            bool approval = vm.InsertMoney(illegalDenomination);

            // Assert
            Assert.False(approval);
        }

        [Fact]
        public void Does_Confirm_Legal_Denominations()
        {
            // Arrange
            IVending vm = new VendingMachine();
            int legalDenomination = 10;

            // Act
            bool approval = vm.InsertMoney(legalDenomination);

            // Assert
            Assert.True(approval);
        }

        [Fact]
        public void Buying_An_Item_Puts_It_In_The_Output_Compartment()
        {
            // Arrange
            IVending vm = new VendingMachine();
            int money = 1000;
            string availableProductName = vm.ShowAll()[0].Name;

            // Insert a lot of money, buy and collect the first product listed in the machine
            vm.InsertMoney(money);
            vm.Purchase(availableProductName);
            List<Product> sut = vm.CollectProducts();

            // Assert that the product collected is the same as the one requested
            Assert.Equal(availableProductName, sut[0].Name);
        }

        [Fact]
        public void Cannot_Buy_With_Insufficient_Funds()
        {
            // Arrange
            IVending vm = new VendingMachine();
            string availableProductName = vm.ShowAll()[0].Name;

            // Insert no money but try to purchase a product
            vm.Purchase(availableProductName);
            List<Product> sut = vm.CollectProducts();

            // Assert
            Assert.Empty(sut);
        }

        [Fact]
        public void Multiple_Items_Can_Be_Bought_And_Collected_At_Once()
        {
            // Arrange
            IVending vm = new VendingMachine();
            int money = 1000;
            string firstProduct = vm.ShowAll()[0].Name;
            string secondProduct = vm.ShowAll()[1].Name;
            int expectedAmount = 2;

            // Insert a lot of money, then buy two available products
            vm.InsertMoney(money);
            vm.Purchase(firstProduct);
            vm.Purchase(secondProduct);
            List<Product> collectedProducts = vm.CollectProducts();
            int sut = collectedProducts.Count;

            // Assert that two products have been collected
            Assert.Equal(expectedAmount, sut);
        }

        [Fact]
        public void Machine_Is_Intialised_With_Products()
        {
            // Ensure that when a machine is created it has some products in it already
            IVending vm = new VendingMachine();
            var sut = vm.ShowAll();


            // Assert
            Assert.NotEmpty(sut);
        }

        [Fact]
        public void Ending_The_Transaction_Puts_Leftover_Funds_In_The_Cash_Output_Compartment()
        {
            // Arrange
            IVending vm = new VendingMachine();
            int money = 1000;
            string availableProductName = "Punchrulle"; // Should cost 12

            // Expected refund should be 1988 (1x1000, 1x500x 4x100, 1x50, 1x20, 1x10, 1x5, 3x1)
            List<int> expected = new List<int>() 
            { 
            1000,
            500,
            100, 100, 100, 100,
            50,
            20,
            10,
            5,
            1,1,1
            };

            // Insert 2000, purchase an item for 12 and get the refund
            vm.InsertMoney(money);
            vm.InsertMoney(money);
            vm.Purchase(availableProductName);
            vm.EndTransaction();
            List<int> sut = vm.CollectMoney();

            // Assert that the product collected is the same as the one requested
            Assert.Equal(expected, sut);
        }

        [Fact]
        public void Can_Check_Money_In_Machine()
        {
            // Arrange
            IVending vm = new VendingMachine();
            int money = 100;
            int startInMachine = vm.ShowMoneyAvailable();
            int expected = startInMachine + money;

            // Act
            vm.InsertMoney(money);
            int sut = vm.ShowMoneyAvailable();

            // Assert
            Assert.Equal(expected, sut);
        }
    }
}
