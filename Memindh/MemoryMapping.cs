using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memindh
{
    public class MemoryMapping
    {
        public int Offset { get; set; }
        public int[] PreOffsets { get; set; }
        public string Module { get; set; }
        public int StartAddress { get; set; }
        public int FinalAddress { get; set; }

        // Constructor
        public MemoryMapping()
        {
            FinalAddress = -1;
        }

        public MemoryMapping(MemoryMappedAttribute attribute) : this()
        {
            Offset = attribute.Offset;
            PreOffsets = new int[attribute.PreOffsets.Length];
            for (int i = 0; i < PreOffsets.Length; i++)
            {
                PreOffsets[i] = attribute.PreOffsets[i];
            }
            Module = attribute.Module;
            StartAddress = attribute.StartAddress;
        }
    }
}
