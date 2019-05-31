using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab21.Models;

namespace Lab21.Controllers
{
    public class ShopController : Controller
    {
        private ShopDbEntities ORM = new ShopDbEntities();

        // GET: Shop
        public ActionResult DisplayShop()
        {
            ViewBag.ShopList = ORM.Items.ToList();
            return View();
        }

        public ActionResult ItemForm()
        {
            return View();
        }

        public ActionResult SaveNewItem(Item newItem)
        {
            //Checks if all required info is entered
            if (ModelState.IsValid)
            {
                //Adds the product and saves to DB
                ORM.Items.Add(newItem);
                ORM.SaveChanges();
                //Returning to action to display updated list
                return RedirectToAction("DisplayShop");
            }
            else
            {
                ViewBag.ErrorMessage = "Something did not go right.";
                return RedirectToAction("DisplayShop");
            }



        }

        public ActionResult Registration(User user)
        {
            return View(user);
        }

        public ActionResult NewUser(NewUserInfo newUser)
        {
            ViewBag.Result = newUser;

            return View();
        }

        public ActionResult SaveNewUser(User newUser)
        {
            //Checks if all required info is entered
            if (ModelState.IsValid)
            {
                //Adds the product and saves to DB
                ORM.Users.Add(newUser);
                ORM.SaveChanges();
                //Returning to action to display updated list
                return RedirectToAction("NewUser",newUser);
            }
            else
            {
                ViewBag.ErrorMessage = "Something did not go right.";
                return RedirectToAction("Registration");
            }



        }

        public ActionResult SaveChanges(Item updatedItem)
        {
            Item originalItem = ORM.Items.Find(updatedItem.Id);

            if (originalItem != null)
            {
                originalItem.Name = updatedItem.Name;
                originalItem.Description = updatedItem.Description;
                originalItem.Quantity = updatedItem.Quantity;

                originalItem.Price = updatedItem.Price;

                ORM.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("UpdateProduct", updatedItem.Id);
            }

        }
    }
}