using FluentValidation;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;

namespace MultiShop.WebUI.Validators
{

    public class CreateFeatureSliderValidators : AbstractValidator<CreateFeatureSliderDto>
    {
        public CreateFeatureSliderValidators()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık alanı boş olamaz!")
                .WithName("Başlık");
            RuleFor(x => x.Description)
              .NotEmpty().WithMessage("Açıklama alanı boş olamaz!")
              .WithName("Açıklama");
            RuleFor(x => x.ImageUrl)
              .NotEmpty().WithMessage("Resim alanı boş olamaz!")
              .WithName("Resim");

        }
    }
 
}
