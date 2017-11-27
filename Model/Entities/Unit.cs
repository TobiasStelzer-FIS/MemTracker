using Memindh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Unit : Entity
    {
        private int hp;

        [MemoryMapped(0x6C)]
        public int Hp
        {
            get
            {
                return hp;
            }
            set
            {
                hp = value;
                NotifyPropertyChanged();
            }
        }

        public Unit(RootEntity rootEntity) : base(rootEntity)
        {
        }

        public void Update()
        {
            try
            {
                UpdatePrimitive("Hp");
                //Hp = memHandler.Read(MemMapping.Module, MemMapping.StartAddress, MemMapping.PreOffsets, MemMapping.Offset);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
