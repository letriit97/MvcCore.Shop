using MvcCore.ViewModels.BaseCommons;
using MvcCore.ViewModels.Request;
using MvcCore.ViewModels.ViewModels;
using System;
using System.Threading.Tasks;

namespace MvcCore.Application.Systems.Users
{
    public interface IUserServices
    {
        Task<ApiResponse<string>> Authencate(LoginRequest request);

        Task<ApiResponse<bool>> Register(RegisterRequest request);

        Task<ApiResponse<bool>> Update(Guid id, UpdateRequest request);

        Task<ApiResponse<bool>> Delete(Guid id);

        Task<ApiResponse<UserViewModel>> GetByID(Guid id);

        Task<ApiResponse<PageResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request);
    }
}