using FluentValidation;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;

namespace MultiShop.WebUI.Validators.Catalog.Feature
{

    public class CreateFeatureValidators : AbstractValidator<CreateFeatureDto>
    {
        public CreateFeatureValidators()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık alanı boş olamaz!")
                .WithName("Başlık");
            RuleFor(x => x.Icon)
              .NotEmpty().WithMessage("İkon alanı boş olamaz!")
              .WithName("İkon");
           
        }
    }
 
}
