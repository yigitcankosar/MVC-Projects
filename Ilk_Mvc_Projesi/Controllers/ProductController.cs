using Ilk_Mvc_Projesi.Models;
using Ilk_Mvc_Projesi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ilk_Mvc_Projesi.Controllers
{
    public class ProductController : Controller
    {
        private readonly NorthwindContext _dbContext;

        public ProductController(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        private int _pageSize = 10;
        public IActionResult Index(int? page = 1)
        {
            var model = _dbContext.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .OrderBy(x => x.Category.CategoryName)
                .ThenBy(x => x.ProductName)
                .Skip((page.GetValueOrDefault() - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            //ViewBag.Categories = _dbContext.Categories.OrderBy(x => x.CategoryName).ToList();
            //ViewBag.Suppliers = _dbContext.Suppliers.OrderBy(x => x.CompanyName).ToList();

            ViewBag.Page = page.GetValueOrDefault(1);
            ViewBag.Limit = (int)Math.Ceiling(_dbContext.Products.Count() / (double)_pageSize);

            return View(model);
        }

        public IActionResult Detail(int? id)
        {
            var data = _dbContext.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .FirstOrDefault(x => x.ProductId == id);
            if (data == null)
                return RedirectToAction(nameof(Index));

            var model = new ProductViewModel()
            {
                ProductId = data.ProductId,
                CategoryName = data.Category?.CategoryName,
                CategoryId = data.CategoryId,
                CompanyName = data.Supplier?.CompanyName,
                ProductName = data.ProductName,
                UnitPrice = data.UnitPrice,
                SupplierId = data.SupplierId
            };

            return View(model);
        }

        public IActionResult Update(int? id)
        {
            var data = _dbContext.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .FirstOrDefault(x => x.ProductId == id);
            if (data == null)
                return RedirectToAction(nameof(Index));

            var model = new ProductViewModel()
            {
                ProductId = data.ProductId,
                CategoryName = data.Category?.CategoryName,
                CategoryId = data.CategoryId,
                CompanyName = data.Supplier?.CompanyName,
                ProductName = data.ProductName,
                UnitPrice = data.UnitPrice,
                SupplierId = data.SupplierId
            };

            ViewBag.CategoryList = GetCategoryList();
            ViewBag.SupplierList = GetSupplierList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = GetCategoryList();
                ViewBag.SupplierList = GetSupplierList();
                return View(model);
            }

            var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == model.ProductId);
            if (product == null)
                return RedirectToAction(nameof(Index));

            product.ProductName = model.ProductName;
            product.UnitPrice = model.UnitPrice;
            product.CategoryId = model.CategoryId;
            product.SupplierId = model.SupplierId;
            try
            {
                _dbContext.Products.Update(product);
                _dbContext.SaveChanges();
                TempData["Mesaj"] = $"{product.ProductName} isimli ürün başarıyla güncellenmiştir";
                return RedirectToAction(nameof(Detail), new { id = product.ProductId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.CategoryList = GetCategoryList();
                ViewBag.SupplierList = GetSupplierList();
                return View(model);
            }
        }


        private List<SelectListItem> GetCategoryList()
        {
            var categories = _dbContext.Categories.OrderBy(x => x.CategoryName).ToList();
            var categoryList = new List<SelectListItem>
            {
                new SelectListItem("Kategori Yok", null)
            };
            foreach (var category in categories)
            {
                categoryList.Add(new SelectListItem(category.CategoryName, category.CategoryId.ToString()));
            }

            return categoryList;
        }

        private List<SelectListItem> GetSupplierList()
        {
            var suppliers = _dbContext.Suppliers.OrderBy(x => x.CompanyName).ToList();

            var supplierList = new List<SelectListItem>()
            {
                new SelectListItem("Tedarikci Yok", null)
            };
            foreach (var supplier in suppliers)
            {
                supplierList.Add(new SelectListItem(supplier.CompanyName, supplier.SupplierId.ToString()));
            }

            return supplierList;
        }
    }
}