using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memindh
{
    public class EntityAggregation : ObservableCollection<Entity>
    {
        private MemoryMapping memMapping;

        public MemoryMapping MemMapping { get; set; }

        public EntityAggregation() : base()
        {
        }
    }
}
