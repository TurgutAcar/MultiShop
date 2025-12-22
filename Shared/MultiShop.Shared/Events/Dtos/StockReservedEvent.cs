using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events.Dtos
{
    public sealed class StockReservedEvent : IntegrationEvent
    {
        public int SagaId { get; set; }
        public int OrderId { get; set; }
        public string ProductId { get; set; }
        public int ReservedQuantity { get; set; }
    }
}
