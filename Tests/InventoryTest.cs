using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inventory
{
  public class InventoryTest
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




// public void Dispose()
// {
//   Task.DeleteAll();
// }
