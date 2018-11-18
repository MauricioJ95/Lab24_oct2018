using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab24_Oct2018.Models; //Step 0. NOTHING WILL FUNCTION IF YOU DON'T HAVE THIS!!!!!

namespace Lab24_Oct2018.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ItemAdmin()
        {
            //1. Create the ORM. Better practice to move code from the controller to a different class.
            CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
            //2. Load the Item table into a ViewBag varibale.
            ViewBag.Items = ORM.Items.ToList(); //This runs a command to go and return data
            //3. Return the view.
            return View();
        }
        public ActionResult ItemDetails(string name)
        {
            //0. Validation and exception handling
            if (name == null || name == "")
                return View("Index");
            //1. ORM 
            CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
            //2. Pull the specific item
            Item found = ORM.Items.Where(i => i.Name == name).ToList()[0]; 
            //3. Put the item in a ViewBag 
            if (found != null)
            {
                ViewBag.Item = found;
                //4. Return the view
                return View();
            }
            else
            {
                return View("Index");
            }
            
        }
        public ActionResult SaveItemChanges(Item NewItem)
        {
            //1. Create ORM
            CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
            //2. Get the old item
            Item oldItem = ORM.Items.Find(NewItem.ID);
            //3. Make the changes
            oldItem.Name = NewItem.Name;
            oldItem.Description = NewItem.Description;
            oldItem.Price = NewItem.Price;
            oldItem.Quantity = NewItem.Quantity;
            //4. Push to the DB
            ORM.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();
            //5. Return to the list of items
            return RedirectToAction("ItemAdmin");


        }
    }
}