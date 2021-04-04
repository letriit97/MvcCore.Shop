using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MvcCore.ViewModels.Request;

namespace MvcCore.ViewModels.Systems.Users
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        // Class chứa tất cả Validation của Login Request
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username là bắt buộc");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu là bắt buộc").MinimumLength(6).WithMessage("mật khẩu tối thiểu 6 kí tự");
        }
    }
}
