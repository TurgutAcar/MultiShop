using FluentValidation;

using MultiShop.DtoLayer.CatalogDtos.ProductDtos;

namespace MultiShop.WebUI.Validators
{

    public class UpdateProductValidators : AbstractValidator<CreateProductDto>
    {
        public UpdateProductValidators()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Ürün adı alanı boş olamaz!")
                .WithName("Başlık");
            RuleFor(x => x.ProductDescription)
              .NotEmpty().WithMessage("Açıklama alanı boş olamaz!")
              .WithName("Açıklama");
            RuleFor(x => x.ProductImageUrl)
              .NotEmpty().WithMessage("Resim alanı boş olamaz!")
              .WithName("Resim");
              RuleFor(x => x.ProductPrice)
              .NotEmpty().WithMessage("Fiyat alanı boş olamaz!")
              .WithName("Resim");

        }
    }
 
}
