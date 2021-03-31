using MvcCore.ViewModels.BaseCommons;
using MvcCore.ViewModels.Request;
using MvcCore.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MvcCore.Application.Catalog.Products
{
    public interface IProductServices
    {
        #region Product

        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<bool> UpdatePrice(int productID, decimal newPrice);
        Task<bool> UpdateStoct(int productID, int addedQuantity);
        Task<ProductViewModel> GetByID(int productID, int languageID);
        Task<int> Delete(int productID);
        Task<PageResult<ProductViewModel>> GetAllPaging(GetProductAdminPagingRequest request);
        #endregion

        #region ProductImage
        Task<int> AddImages(int productID, ProductImageCreateRequest productImage);
        Task<int> UpdateImages(int imageID, ProductImageUpdateRequest productImage);
        Task<int> RemoveImages(int imageID);

        Task<ProductImageViewModel> GetImageByID(int imageID);
        Task<List<ProductImageViewModel>> GetListImages(int productID);
        Task<PageResult<ProductViewModel>> GetAllbyCategoryID(int languageID, GetProductPagingRequest request);

        #endregion
    }
}
