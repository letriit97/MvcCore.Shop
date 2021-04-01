using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Application.Catalog.Products;
using MvcCore.ViewModels.Request;

namespace MvcCore.BackendApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        #region Public

        //[HttpGet("/{languageID}")]
        //public async Task<IActionResult> GetAllPaging(int languageID, [FromQuery] GetProductPagingRequest request)
        //{
        //    var product = await _productServices.GetAllbyCategoryID(languageID, request);
        //    return Ok(product);
        //}

        #endregion Public

        #region Admin

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetProductAdminPagingRequest request)
        {
            var product = await _productServices.GetAllPaging(request);
            return Ok(product);
        }

        [HttpGet("{productID}/{languageID}")]
        public async Task<IActionResult> GetByID(int productID, int languageID)
        {
            var product = await _productServices.GetByID(productID, languageID);
            if (product == null)
                return NotFound();
            //return BadRequest("Cannot fint product");

            return Ok(product);
        }

        [Authorize]
        [HttpPost()]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _productServices.Create(request);
            if (productId == 0)
                return BadRequest();

            var product = await _productServices.GetByID(productId, request.LanguageID);

            return CreatedAtAction(nameof(GetByID), new { id = productId }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _productServices.Update(request);

            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _productServices.Delete(productId);

            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        //UPdate 1 phần của đối tượng
        [HttpPatch("price/{productID}/{newprice}")]
        public async Task<IActionResult> UpdatePrice(int productID, decimal newPrice)
        {
            var isSuccess = await _productServices.UpdatePrice(productID, newPrice);
            if (isSuccess == true)
                return Ok();
            return BadRequest();
        }

        #endregion Admin

        #region ProductImages

        [HttpPost("{productID}/image")]
        public async Task<IActionResult> CreateImage(int productID, [FromForm] ProductImageCreateRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            int imageID = await _productServices.AddImages(productID, request);
            if (imageID == 0)
            {
                return BadRequest();
            }

            var Images = await _productServices.GetImageByID(imageID);

            return CreatedAtAction(nameof(GetImageByID), new { id = imageID }, Images);
        }

        //Update Nhiều
        [HttpPut("/image/{imageID}")]
        public async Task<IActionResult> UpdateImage(int imageID, [FromQuery] ProductImageUpdateRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var pImage = await _productServices.UpdateImages(imageID, request);
            if (pImage == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{productId}/images/{imageID}")]
        public async Task<IActionResult> RemoveImage(int imageID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productServices.RemoveImages(imageID);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpGet("{productID}/images/{imageID}")]
        public async Task<IActionResult> GetImageByID(int productID, int imageID)
        {
            var image = await _productServices.GetImageByID(imageID);
            if (image == null)
                //return NotFound();
                return BadRequest("Cannot fint product");

            return Ok(image);
        }

        #endregion ProductImages
    }
}
