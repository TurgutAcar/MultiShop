using FluentValidation;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;

namespace MultiShop.WebUI.Validators.Catalog.OfferDiscount
{
    public class CreateOfferDiscountValidators:AbstractValidator<CreateOfferDiscountDto>
    {
        public CreateOfferDiscountValidators()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Başlık alanı boş olamaz!")
               .WithName("Başlık");
            RuleFor(x => x.SubTitle)
              .NotEmpty().WithMessage("Alt başlık alanı boş olamaz!")
              .WithName("Alt Başlık");
            RuleFor(x => x.ImageUrl)
              .NotEmpty().WithMessage("Resim alanı boş olamaz!")
              .WithName("Resim");
            RuleFor(x => x.ButtonTitle)
              .NotEmpty().WithMessage("Buton başlık alanı boş olamaz!")
              .WithName("Buton Başlık");

        }
    }
}
