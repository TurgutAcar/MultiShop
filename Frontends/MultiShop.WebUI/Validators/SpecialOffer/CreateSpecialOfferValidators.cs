using FluentValidation;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;

namespace MultiShop.WebUI.Validators
{

    public class CreateSpecialOfferValidators : AbstractValidator<CreateSpecialOfferDto>
    {
        public CreateSpecialOfferValidators()
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

        }
    }
 
}
