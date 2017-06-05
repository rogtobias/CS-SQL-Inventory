using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inventory
{
  public class InventoryTest : IDisposable
  {
    public InventoryTest()
    {
       DBConfiguration.ConnectionString  = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=inventory_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Inventory.GetAll().Count;
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfBeersAreTheSame()
    {
      //Arrange, Act
      Inventory firstInventory = new Inventory("Bud");
      Inventory secondInventory = new Inventory("Bud");

      //Assert
      Assert.Equal(firstInventory, secondInventory);
    }
    [Fact]
    public void Test_Save_SaveToDatabase()
    {
      //Arrange
      Inventory testInventory = new Inventory("Coors");

      //Act
      testInventory.Save();
      List<Inventory> result = Inventory.GetAll();
      List<Inventory> testList = new List<Inventory>{testInventory};

      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Inventory testInventory = new Inventory("Rise Up Red");

      //Act
      testInventory.Save();
      Inventory savedInventory = Inventory.GetAll()[0];
      int result = savedInventory.GetId();
      int testId = testInventory.GetId();

      //Assert
      Assert.Equal(testId, result);
    }
    public void Dispose()
    {
      Inventory.DeleteAll();
    }
  }
}




















// [Fact]
// public void Inventory_InventoryConstructor_IsSaving()
// {
//
//   Inventory newInventory = new Inventory("Bud", "garbage", 12);
//
//   Assert.Equal(newInventory.Inventory("Bud", "garbage", 12), result);
// }
