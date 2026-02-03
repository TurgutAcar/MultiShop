using FluentValidation;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;

namespace MultiShop.WebUI.Validators
{

    public class CreateCategoryValidators : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidators()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Kategori adı boş olamaz!")
                .WithName("KategoriAdı");
            RuleFor(x => x.ImageUrl)
              .NotEmpty().WithMessage("Kategori resmi zorunludur!")
              .WithName("KategoriResmi");
        }
    }
 
}
