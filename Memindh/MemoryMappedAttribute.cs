using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memindh
{
    public class MemoryMappedAttribute : Attribute
    {
        private int offset = 0;
        private int[] preOffsets = null;
        private string module = null;
        private int startAddress = 0;

        public int Offset { get { return offset; } }
        public int[] PreOffsets { get; set; }
        public string Module { get; set; }
        public int StartAddress { get; set; }

        // Constructor
        public MemoryMappedAttribute(int offset)
        {
            this.offset = offset;
        }
    }
}
