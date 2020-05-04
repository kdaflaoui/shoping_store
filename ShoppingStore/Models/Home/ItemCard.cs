using ShoppingStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingStore.Models.Home
{
    public class ItemCard
    {
        public Tbl_Product Product { get; set; }
        public int Quantity { get; set; }

    }
}