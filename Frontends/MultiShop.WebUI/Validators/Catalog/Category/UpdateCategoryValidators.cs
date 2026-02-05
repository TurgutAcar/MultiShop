using FluentValidation;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;

namespace MultiShop.WebUI.Validators.Catalog.Category
{

    public class UpdateCategoryValidators : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidators()
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
