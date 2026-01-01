//using MultiShop.Order.Application.Features.Mediator.Commands;
//using MultiShop.Order.Domain.OrderAggregate;

//namespace MultiShop.Order.Application.Factories
//{
//    public static class OrderingFactory
//    {
//        public static Ordering CreateFromCommand(CreateOrderingCommand command)
//        {
//            var address = new Address
//            {
//                UserId = command.Address.UserId,
//                Name = command.Address.Name,
//                Surname = command.Address.Surname,
//                Email = command.Address.Email,
//                Phone = command.Address.Phone,
//                Country = command.Address.Country,
//                District = command.Address.District,
//                City = command.Address.City,
//                Detail1 = command.Address.Detail1,
//                Detail2 = command.Address.Detail2,
//                Description = command.Address.Description,
//                ZipCode = command.Address.ZipCode
//            };

//            var ordering = new Ordering(
//                command.UserId,
//                command.TotalPrice,
//                command.OrderDate,
//                address
//            );

//            foreach (var item in command.OrderItems)
//            {
//                ordering.AddOrderDetail(
//                    item.ProductId,
//                    item.ProductName,
//                    item.ProductPrice,
//                    item.ProductAmount,
//                    item.ProductTotalPrice
//                );
//            }

//            return ordering;
//        }
//    }

//}
