using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Memindh
{
    public class RootEntity : Entity
    {
        public MemHandler MemHandler { get; set; }

        public RootEntity(MemHandler memHandler)
        {
            this.MemHandler = memHandler;
            this.RootEntity = this;
        }
    }
}
