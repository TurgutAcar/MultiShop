using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events.EventInterface
{
    public interface IOrderCompletedEvent
    {
        Guid CorrelationId { get; }
    }
}
