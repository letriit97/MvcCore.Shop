using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MvcCore.Application.Commons;
using MvcCore.Data.EF;
using MvcCore.Data.Entities;
using MvcCore.Data.Infrastructure.Interfaces;
using MvcCore.Utilities.Exceptions;
using MvcCore.ViewModels.BaseCommons;
using MvcCore.ViewModels.Request;
using MvcCore.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcCore.Application.Catalog.Products
{
    public class ProductServices : IProductServices
    {
        private readonly ShopOnlineDBContext db;
        private readonly IStoreageServices _IStorageService;


        public ProductServices(ShopOnlineDBContext context, IStoreageServices storeageServices)
        {
            db = context;
            _IStorageService = storeageServices;
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                ViewCount = 0,
                Stock = request.Stock,
                CreateTime = DateTime.Now,
                SeoAlias = request.SeoAlias,

                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name =  request.Title,
                        Description = request.Description,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoAlias,
                        LanguageId = request.LanguageID
                    }
                }
            };

            //Save Images before Save Product.
            if (request.FileUpload != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Captions = "HinhAnh",
                        CreateTime = DateTime.Now,
                        FileSize =request.FileUpload.Length,
                        Url = await this.SaveFile(request.FileUpload),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }

            //Save Dataa

            db.Products.Add(product);
            await db.SaveChangesAsync();
            return product.ID;
        }

        #region Methods
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _IStorageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
        #endregion

        public async Task AddViewCount(int productID)
        {
            var product = await db.Products.FindAsync(productID);
            product.ViewCount += 1;
            await db.SaveChangesAsync();
        }


        public async Task<int> Delete(int productID)
        {
            var product = await db.Products.FindAsync(productID);
            if (product == null)
            {
                throw new ShopOnlineExceptions($"Cannot Find Product: {productID}");
            }

            // Xóa File vật lý
            var image = db.ProductImages.Where(x => x.ProductID == productID);
            foreach (var item in image)
            {
                await _IStorageService.DeleteAsync(item.Url);
            }

            db.Products.Remove(product);

            return await db.SaveChangesAsync();
        }

        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetProductAdminPagingRequest request)
        {
            //1.Join
            var query = from p in db.Products
                        join pt in db.ProductTranslations on p.ID equals pt.ProductID
                        //join pc in db.ProductCategories on p.ID equals pc.ProductID 
                        //join c in db.Categories on pc.ProductCategoryID equals c.ID
                        select new { p, pt };
            //2.Filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));
            }

            //if (request.CategoryID != null && request.CategoryID.Count() != 0)
            //{
            //    query = query.Where(i => request.CategoryID.Contains(i.pc.ProductCategoryID));
            //}

            //3.Paging
            int totalItems = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    ID = x.p.ID,
                    Title = x.p.Title,
                    Name = x.p.Title,
                    CreateTime = x.p.CreateTime,
                    Description = x.p.Description,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,

                    LanguageId = x.pt.LanguageId,
                    Details = x.pt.Details,
                    SeoAlias = x.pt.SeoAlias,
                    SeoTitle = x.pt.SeoTitle,
                    SeoDescription = x.pt.SeoDescription,

                }).ToListAsync();

            //4.Select and Project 
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalItems = totalItems,
                Items = data
            };

            return pageResult;

        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await db.Products.Where(x => x.ID == request.ID).FirstOrDefaultAsync();
            var productTranslation = await db.ProductTranslations.
                Where(x => x.ProductID == request.ID && x.LanguageId == request.LanguageId)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                throw new ShopOnlineExceptions($"Cannnot Find Product with ID: {request.ID}");
            }

            //Save Data
            productTranslation.Name = request.Name;
            productTranslation.Description = request.Description;
            productTranslation.Details = request.Details;
            productTranslation.SeoAlias = request.SeoAlias;
            productTranslation.SeoDescription = request.SeoDescription;
            productTranslation.SeoTitle = request.SeoTitle;

            //Save Images before Save Product.
            if (request.FileUpload != null)
            {
                var image = await db.ProductImages.Where(x => x.IsDefault == true && x.ProductID == request.ID).FirstOrDefaultAsync();

                if (image != null)
                {

                    image.FileSize = request.FileUpload.Length;
                    image.Url = await this.SaveFile(request.FileUpload);
                    db.ProductImages.Update(image);
                }
            }


            return await db.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productID, decimal newPrice)
        {
            var product = await db.Products.Where(x => x.ID == productID).FirstOrDefaultAsync();

            if (product == null)
            {
                throw new ShopOnlineExceptions($"Cannnot Find Product with ID: {productID}");
            }

            product.Price = newPrice;
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStoct(int productID, int addedQuantity)
        {
            var product = await db.Products.Where(x => x.ID == productID).FirstOrDefaultAsync();

            if (product == null)
            {
                throw new ShopOnlineExceptions($"Cannnot Find Product with ID: {productID}");
            }

            product.Stock += addedQuantity;
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<ProductViewModel> GetByID(int productID, int languageID)
        {
            var product = await db.Products.Where(x => x.ID == productID).FirstOrDefaultAsync();
            var productTranslation = await db.ProductTranslations.Where(x => x.ProductID == productID && x.LanguageId == languageID).FirstOrDefaultAsync();


            var productViewModel = new ProductViewModel()
            {
                ID = product.ID,
                Title = product != null ? product.Title : string.Empty,
                Description = productTranslation != null ? productTranslation.Description : string.Empty,
                LanguageId = productTranslation.LanguageId,
                Details = productTranslation != null ? productTranslation.Details : string.Empty,
                Name = productTranslation != null ? productTranslation.Name : string.Empty,
                Price = product.Price,
                OriginalPrice = product.OriginalPrice,
                Stock = product.Stock,
                ViewCount = product.ViewCount,
                CreateTime = product.CreateTime,
                SeoAlias = product.SeoAlias,
            };

            return productViewModel;
        }

        public Task<int> Delete(int productID, int imageID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddImages(int productID, ProductImageCreateRequest request)
        {
            var image = new ProductImage()
            {
                ProductID = productID,
                Captions = request.Captions,
                CreateTime = DateTime.Now,
                IsDefault = request.IsDefault,
                SortOrder = request.SortOrder,
            };

            //Save Images before Save Product.
            if (request.ImageFile != null)
            {
                image.Url = await this.SaveFile(request.ImageFile);
                image.FileSize = request.ImageFile.Length;

            }

            db.ProductImages.Add(image);
            await db.SaveChangesAsync();
            return image.ID;
        }

        public async Task<int> UpdateImages(int imageID, ProductImageUpdateRequest request)
        {

            var image = await db.ProductImages.Where(x => x.ID == imageID).FirstOrDefaultAsync();

            if (image == null)
            {
                //return new ShopOnlineExceptions($"Cannot Find an image with ID {imageID}");
                return -1;
            }



            //Save Images before Save Product.
            if (request.ImageFile != null)
            {
                image.Url = await this.SaveFile(request.ImageFile);
                image.FileSize = request.ImageFile.Length;
            }

            db.ProductImages.Update(image);
            return await db.SaveChangesAsync();
        }
        public async Task<List<ProductImageViewModel>> GetListImages(int productID)
        {
            return await db.ProductImages.Where(x => x.ProductID == productID).Select(x => new ProductImageViewModel()
            {
                Captions = x.Captions,
                CreateTime = x.CreateTime,
                Url = x.Url,
                FileSize = x.FileSize,
                ID = x.ID,
                IsDefault = x.IsDefault,
                SortOrder = x.SortOrder
            }).ToListAsync();
        }

        public async Task<int> RemoveImages(int imageID)
        {
            var pImage = await db.ProductImages.Where(x => x.ID == imageID).FirstOrDefaultAsync();

            if (pImage == null)
                return -1;

            db.ProductImages.Remove(pImage);
            return await db.SaveChangesAsync();

        }

        public async Task<ProductImageViewModel> GetImageByID(int imageID)
        {
            var image = await db.ProductImages.Where(x => x.ID == imageID).FirstOrDefaultAsync();

            if (image == null)
                throw new ShopOnlineExceptions($"Cannot Find an Image with ID {imageID}");

            var result = new ProductImageViewModel()
            {
                Captions = image.Captions,
                CreateTime = image.CreateTime,
                Url = image.Url,
                FileSize = image.FileSize,
                ID = image.ID,
                IsDefault = image.IsDefault,
                SortOrder = image.SortOrder
            };

            return result;

        }

        public async Task<PageResult<ProductViewModel>> GetAllbyCategoryID(int languageID, GetProductPagingRequest request)
        {
            //1.Join
            var query = from p in db.Products
                        join pt in db.ProductTranslations on p.ID equals pt.ProductID
                        join pc in db.ProductCategories on p.ID equals pc.ProductID
                        join c in db.Categories on pc.ProductCategoryID equals c.ID
                        where pt.LanguageId == languageID
                        select new { p, pt, pc };
            //2.Filter


            if (request.CategoryID.Count() > 0)
            {
                query = query.Where(i => request.CategoryID.Contains(i.pc.ProductCategoryID));
            }

            //3.Paging
            int totalItems = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    ID = x.p.ID,
                    Name = x.p.Title,
                    CreateTime = x.p.CreateTime,
                    Description = x.p.Description,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,

                    LanguageId = x.pt.LanguageId,
                    Details = x.pt.Details,
                    SeoAlias = x.pt.SeoAlias,
                    SeoTitle = x.pt.SeoTitle,
                    SeoDescription = x.pt.SeoDescription,

                }).ToListAsync();

            //4.Select and Project 
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalItems = totalItems,
                Items = data
            };

            return pageResult;

        }
    }
}
