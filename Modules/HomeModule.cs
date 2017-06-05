using Nancy;
using Inventory;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inventory
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => View["index.cshtml"];

      Post["/new"] = _ => {
        Inventory newInventory = new Inventory(Request.Form["beerName"]);
        newInventory.Save();
        return View["new.cshtml", newInventory];
      };

      Get["/view_all"] = _ => {
        List<Inventory> allInventory = Inventory.GetAll();
        return View["view_all.cshtml", allInventory];
      };

      Get["/view_all/{id}"] = parameters => {
        Inventory selectedInventory = Inventory.Find(parameters.id);
        return View["view_beer.cshtml", selectedInventory];
      };

      Post["/DeleteAll"] = _ => {
        Inventory.DeleteAll();
        return View["index.cshtml"];
      };
    }
  }
}
