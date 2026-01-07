using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Catalog.OutboxWorker.Infrastructure.Settings
{
    public class OutboxOptions
    {
        public int BatchSize { get; set; }
        public int PollIntervalSeconds { get; set; }
        public int LockDurationSeconds { get; set; }
    }
}
