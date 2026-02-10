using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
namespace MultiShop.Catalog.Search.Middlewares
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizeByMethodAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            foreach (var action in controller.Actions)
            {
                // Action üzerindeki Http metodunu bulalım
                var httpMethod = action.Selectors
                    .SelectMany(s => s.ActionConstraints.OfType<HttpMethodActionConstraint>())
                    .SelectMany(c => c.HttpMethods)
                    .FirstOrDefault()?.ToUpper();

                if (string.IsNullOrEmpty(httpMethod)) continue;

                if (httpMethod == "GET")
                {
                    action.Filters.Add(new AuthorizeFilter("CatalogSearchPolicy"));
                }
             
            }
        }
    }
}
