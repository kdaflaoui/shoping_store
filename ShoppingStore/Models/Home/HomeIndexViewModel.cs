using PagedList;
using ShoppingStore.DAL;
using ShoppingStore.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShoppingStore.Models.Home
{
    public class HomeIndexViewModel
    {
        private GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        private dbShoppingStoreEntities _dbContext = new dbShoppingStoreEntities();

        public IEnumerable<Tbl_Product> listProduct { get; set; }

        public HomeIndexViewModel createModel(string search, int size, int? page)
        {
            IEnumerable<Tbl_Product> products = null;
            if (search == null)
            {
                search = "";

                products = _dbContext
                    .Tbl_Product
                    .Where(p => p.ProductName.Contains(search))
                    .Include(p => p.Tbl_Category)
                    .ToList();
            }
            else
            {
                products = _dbContext
                .Tbl_Product
                .Where(p => p.ProductName.Contains(search))
                .Include(p => p.Tbl_Category)
                .ToList();
            }
            return new HomeIndexViewModel
            {
                listProduct = products
            };
        }
    }
}