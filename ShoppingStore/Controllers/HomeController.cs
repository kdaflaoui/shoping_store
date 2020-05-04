using ShoppingStore.DAL;
using ShoppingStore.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingStore.Controllers
{
    public class HomeController : Controller
    {
        private dbShoppingStoreEntities _dbContext = new dbShoppingStoreEntities();
        private HomeIndexViewModel _model = new HomeIndexViewModel();

        public ActionResult Index(string search, int? page)
        {
            ViewBag.Count = 0;
            if (Session["Card"] != null)
            {
                List<ItemCard> card = (List<ItemCard>)Session["Card"];
                ViewBag.Count = card.Count;
            }
            return View(_model.createModel(search, 4, page));
        }

        public ActionResult AddToCard(int productID)
        {
            var product = _dbContext.Tbl_Product.Find(productID);
            
            if (Session["Card"] == null)
            {
                List<ItemCard> cards = new List<ItemCard>();
                cards.Add(new ItemCard
                {
                    Product = product,
                    Quantity = 1
                });
                Session["Card"] = cards;
            }
            else
            {
                int previousQtity = 0;  
                List<ItemCard> card = (List<ItemCard>)Session["Card"];
                foreach (var item in card)
                {
                    if (item.Product.ProductId == productID)
                    {
                        previousQtity = item.Quantity;
                        card.Remove(item);
                        break;
                    }

                }
                card.Add(new ItemCard
                {
                    Product = product,
                    Quantity = previousQtity + 1
                });
                Session["Card"] = card;
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCard(int productID)
        {
            List<ItemCard> card = (List<ItemCard>)Session["Card"];
            if (Session["Card"] != null)
            {
                foreach (var item in card)
                {
                    if (item.Product.ProductId == productID)
                    {
                        if(item.Quantity > 1)
                        {
                            item.Quantity  = item.Quantity - 1;
                        }
                        else
                        {
                            card.Remove(item);
                            break;
                        }
                    }

                }
                Session["Card"] = card;
            }
            if(card.Count == 0)
            {
                Session["Card"] = null;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult CheckoutDetails()
        {
            return View();
        }


    }
}