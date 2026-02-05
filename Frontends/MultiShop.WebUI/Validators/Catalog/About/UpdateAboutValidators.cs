using FluentValidation;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;

namespace MultiShop.WebUI.Validators.Catalog.About
{

    public class UpdateAboutValidators : AbstractValidator<UpdateAboutDto>
    {
        public UpdateAboutValidators()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama alanı boş olamaz!")
                .WithName("Açıklama");
            RuleFor(x => x.Address)
              .NotEmpty().WithMessage("Adres alanı boş olamaz!")
              .WithName("Adres");
            RuleFor(x => x.Phone)
               .NotEmpty().WithMessage("Telefon alanı boş olamaz!")
               .WithName("Telefon");
            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("Email alanı boş olamaz!")
              .WithName("Email")
              .EmailAddress().WithMessage("Geçerli bir email giriniz!");
        }
    }
 
}
