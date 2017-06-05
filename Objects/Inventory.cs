using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Inventory
{
  public class Inventory
  {
    private string _beerName;
    private int _id;

    public Inventory(string beerName, int Id = 0)
    {
      _beerName = beerName;
      _id = Id;
    }
    public string GetBeerName()
    {
      return _beerName;
    }
    public void SetBeerName(string beerName)
    {
      _beerName = beerName;
    }
    public int GetId()
    {
      return _id;
    }
    public static List<Inventory> GetAll()
    {
      List<Inventory> allInventories = new List<Inventory>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM Inventory;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int inventoryId = rdr.GetInt32(0);
        string beerName = rdr.GetString(1);
        Inventory newInventory = new Inventory(beerName, inventoryId);
        allInventories.Add(newInventory);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allInventories;
    }

  }
}
