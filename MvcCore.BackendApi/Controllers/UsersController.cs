using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Application.Systems.Users;
using MvcCore.ViewModels.Request;

namespace MvcCore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultToken = await _userServices.Authencate(request);

            if (string.IsNullOrEmpty(resultToken.Result))
            {
                return BadRequest(resultToken);
            }

            return Ok(resultToken);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userServices.Register(request);

            if (!result.Staus)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // /api/users/id
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit([FromBody] UpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userServices.Update(request.ID, request);

            if (!result.Staus)
                return BadRequest(result);

            return Ok(result);
        }


        // api/users/paging?pageIndex=1&pageSize=10&keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            var product = await _userServices.GetUserPaging(request);
            return Ok(product);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            var user = await _userServices.GetByID(id);
            return Ok(user);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userServices.Delete(id);
            return Ok(user);
        }
    }
}
