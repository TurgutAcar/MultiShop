using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events
{
    public abstract class IntegrationEvent
    {
        public string EventType => GetType().Name;
    }

}
