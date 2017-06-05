using Xunit;
using System;
using System.Collections.Generic;
using InventoryProject.Objects;
using System.Data;
using System.Data.SqlClient;

namespace InvetoryTester
{
  public class Inventory  : IDisposable
  {
    public ToDoTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=todo_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Task.DeleteAll();
    }
  }
}
