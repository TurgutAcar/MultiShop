using FluentValidation;
using MultiShop.DtoLayer.IdentityDtos.RegisterDtos;

namespace MultiShop.WebUI.Validators
{

    public class CreateRegisterValidators : AbstractValidator<CreateRegisterDto>
    {
        public CreateRegisterValidators()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz")
                .MinimumLength(3).WithMessage("En az 3 karakter olmalı")
                .WithName("KullanıcıAdı");
            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("Email zorunludur")
              .EmailAddress().WithMessage("Geçerli bir email giriniz")
              .WithName("Email");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad boş olamaz")
                .WithName("Adı");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyadı boş olamaz")
                .WithName("Soyadı");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz")
                .MinimumLength(3).WithMessage("En az 6 karakter olmalı")
                .WithName("Sifre");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Şifre tekrarı boş olamaz")
                .MinimumLength(3).WithMessage("En az 6 karakter olmalı")
                .WithName("SifreTekrar");

        }
    }
 
}
