using FluentValidation;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;

namespace MultiShop.WebUI.Validators
{

    public class CreateProductDetailValidators : AbstractValidator<CreateProductDetailDto>
    {
        public CreateProductDetailValidators()
        {
           
            RuleFor(x => x.ProductDescription)
              .NotEmpty().WithMessage("Açıklama alanı boş olamaz!")
              .WithName("Açıklama");
            RuleFor(x => x.ProductInfo)
              .NotEmpty().WithMessage("Ürün bilgisi alanı boş olamaz!")
              .WithName("Ürün Bilgisi");
            

        }
    }
 
}
