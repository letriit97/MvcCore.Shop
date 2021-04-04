using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MvcCore.Data.Entities;
using MvcCore.ViewModels.BaseCommons;
using MvcCore.ViewModels.Request;
using MvcCore.ViewModels.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MvcCore.Application.Systems.Users
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly RoleManager<AppRoles> _roleManager;
        private readonly IConfiguration _config;

        public UserServices(UserManager<AppUsers> userManager,
                            SignInManager<AppUsers> signInManager,
                            RoleManager<AppRoles> roleManager,
                            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<ApiResponse<string>> Authencate(LoginRequest request)
        {
            //Tìm kiếm user
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                return new ApiError<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);

            if (!result.Succeeded)
                return new ApiError<string>("Mật khẩu chưa đúng. Vui lòng Kiếm tra lại!");

            var roles = _userManager.GetRolesAsync(user);
            var claims = new[]
            {
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.GivenName, user.FirstName),
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(ClaimTypes.Role, string.Join(";",roles)),
           };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new ApiResponse<string>()
            {
                Result = stringToken,
                Staus = true,
            };
        }

        public async Task<ApiResponse<bool>> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiError<bool>("User không tồn tại");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccess<bool>("Xoá item thành công");
            }
            return new ApiError<bool>("Xoá không thành công");
        }

        public async Task<ApiResponse<UserViewModel>> GetByID(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiError<UserViewModel>("User không tồn tại");
            }

            var userVM = new UserViewModel()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.PhoneNumber,
                DOB = user.Dob
            };

            return new ApiSuccess<UserViewModel>(userVM);
        }

        public async Task<ApiResponse<PageResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;

            if (!string.IsNullOrEmpty(request.KeyWord))
            {
                //Search
                query = query.Where(x => x.UserName.Contains(request.KeyWord) || x.PhoneNumber.Contains(request.KeyWord) || x.Email.Contains(request.KeyWord));
            }

            //3.Paging
            int totalItems = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new UserViewModel()
                {
                    ID = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Mobile = x.PhoneNumber
                }).ToListAsync();

            //4.Select and Project
            var pageResult = new PageResult<UserViewModel>()
            {
                TotalItems = totalItems,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };

            return new ApiSuccess<PageResult<UserViewModel>>(pageResult);
        }

        public async Task<ApiResponse<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            //Validate
            if (user != null)
            {
                return new ApiError<bool>("Tài khoản đã tồn tại");
            }

            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiError<bool>("Email đã tồn tại");
            }

            user = new AppUsers()
            {
                Dob = request.DOB,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.Mobile
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new ApiSuccess<bool>("Thêm mới thành công"); ;
            }
            return new ApiError<bool>("Email đã tồn tại"); ;
        }

        public async Task<ApiResponse<bool>> Update(Guid id, UpdateRequest request)
        {
            //Validate Email
            if (await _userManager.Users.AnyAsync((x => x.Email == request.Email && x.Id != id)))
            {
                return new ApiError<bool>("Email đã tồn tại");
            }

            var user = await _userManager.FindByIdAsync(id.ToString());

            user.Dob = request.DOB;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.Mobile;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new ApiSuccess<bool>("cập nhật thành công"); ;
            }
            return new ApiError<bool>("cập nhật không thành công"); ;
        }
    }
}