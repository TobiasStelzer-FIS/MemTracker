using Memindh;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class SelectedUnitList : EntityAggregation
    {
        private Global global;

        public SelectedUnitList(Global global) : base()
        {
            this.global = global;
        }

        public void Update()
        {
            if (!IsPreconditionMet())
            {
                throw new Exception("Precondition is not met.");
            }

            Items.Clear();

            for (int i = 0; i < global.SelectedUnitCount; i++)
            {
                MemoryMapping memMapping = new MemoryMapping();
                memMapping.Module = this.MemMapping.Module;
                memMapping.StartAddress = this.MemMapping.StartAddress;
                int[] preOffsets = new int[this.MemMapping.PreOffsets.Length+1];
                for (int j = 0; j < this.MemMapping.PreOffsets.Length; j++)
                {
                    preOffsets[j] = this.MemMapping.PreOffsets[j];
                }
                preOffsets[this.MemMapping.PreOffsets.Length] = 0;
                memMapping.PreOffsets = preOffsets;
                memMapping.Offset = i * 4;

                Unit unit = new Unit(global);
                unit.MemMapping = memMapping;

                this.Add(unit);
            }
        }

        public bool IsPreconditionMet()
        {
            return true;
        }

        public Unit this[int index]
        {
            get
            {
                Update();
                return base[index] as Unit;
            }
        }
    }
}
