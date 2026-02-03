using FluentValidation;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;

namespace MultiShop.WebUI.Validators
{

    public class CreateBrandValidators : AbstractValidator<CreateBrandDto>
    {
        public CreateBrandValidators()
        {
            RuleFor(x => x.BrandName)
                .NotEmpty().WithMessage("Marka adı boş olamaz!")
                .WithName("MarkaAdı");
            RuleFor(x => x.ImageUrl)
              .NotEmpty().WithMessage("Marka resmi zorunludur!")
              .WithName("MarkaResmi");
        }
    }
 
}
