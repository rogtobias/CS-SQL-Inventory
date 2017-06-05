using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Inventory
{
  public class Inventory
  {
    private string _beerName;
    private int _id;
    private static List<Inventory> _instances = new List<Inventory>();

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

    public override bool Equals(System.Object otherInventory)
    {
      if (!(otherInventory is Inventory))
      {
        return false;
      }
      else
      {
        Inventory newInventory = (Inventory) otherInventory;
        bool idEquality = (this.GetId() == newInventory.GetId());
        bool descriptionEquality = (this.GetBeerName() == newInventory.GetBeerName());
        return (idEquality && descriptionEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO inventory (beerName) OUTPUT INSERTED.id VALUES (@InventoryBeerName);", conn);

      SqlParameter descriptionParameter = new SqlParameter();
      descriptionParameter.ParameterName = "@InventoryBeerName";
      descriptionParameter.Value = this.GetBeerName();
      cmd.Parameters.Add(descriptionParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM Inventory;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
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

    public static Inventory Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM inventory WHERE id = @InventoryId", conn);
      SqlParameter inventoryIdParameter = new SqlParameter();
      inventoryIdParameter.ParameterName = "@InventoryId";
      inventoryIdParameter.Value = id.ToString();
      cmd.Parameters.Add(inventoryIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundInventoryId = 0;
      string foundInventoryDescription = null;
      while(rdr.Read())
      {
        foundInventoryId = rdr.GetInt32(0);
        foundInventoryDescription = rdr.GetString(1);
      }
      Inventory foundInventory = new Inventory(foundInventoryDescription, foundInventoryId);

      if (rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }

     return foundInventory;
    }
  }
}
