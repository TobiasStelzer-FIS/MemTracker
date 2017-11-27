using Memindh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Global : RootEntity
    {
        private SelectedUnitList selectedUnitList;
        private int selectedUnitCount;

        [MemoryMapped(0x0, Module = "game.exe", PreOffsets = new int[] { 0x0 }, StartAddress = 0x640C64)]
        public SelectedUnitList SelectedUnitList
        {
            get
            {
                if (selectedUnitList == null)
                {
                    selectedUnitList = new SelectedUnitList(this);

                    MemoryMapping memMapping = new MemoryMapping(this.ExtractMemoryMappedAttribute("SelectedUnitList"));
                    selectedUnitList.MemMapping = memMapping;
                }
                return selectedUnitList;
            }
        }
        public int SelectedUnitCount
        {
            get
            {
                return selectedUnitCount;
            }
            set
            {
                selectedUnitCount = value;
                NotifyPropertyChanged();
            }
        }

        public Global(MemHandler memHandler) : base(memHandler)
        {
        }

        public void Update()
        {
            try
            {
                SelectedUnitCount = this.MemHandler.Read("game.exe", 0x00640C70, new int[0]);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
