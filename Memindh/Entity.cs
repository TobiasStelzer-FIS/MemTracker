using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Memindh
{
    public abstract class Entity : INotifyPropertyChanged
    {
        public MemoryMapping MemMapping { get; set; }
        public RootEntity RootEntity { get; set; }

        public Entity()
        {
        }

        public Entity(RootEntity rootEntity)
        {
            RootEntity = rootEntity;
        }

        public void UpdatePrimitive(string propertyName)
        {
            MemoryMappedAttribute propMemMapping = ExtractMemoryMappedAttribute(propertyName);
            int finalAddress = GetFinalAddress() + propMemMapping.Offset;
            int value = this.RootEntity.MemHandler.ReadInt32(new IntPtr(finalAddress));
            ExtractPropertyInfo(propertyName).SetMethod.Invoke(this, new object[] { value });
        }

        public int GetFinalAddress()
        {
            if (MemMapping.FinalAddress < 0)
            {
                int[] preOffsets = new int[MemMapping.PreOffsets.Length];
                for (int i = 0; i < MemMapping.PreOffsets.Length; i++)
                {
                    preOffsets[i] = MemMapping.PreOffsets[i];
                }
                MemHandler memHandler = this.RootEntity.MemHandler;
                MemMapping.FinalAddress = (int)memHandler.CalculateFinalAddress(MemMapping.Module, MemMapping.StartAddress, preOffsets, MemMapping.Offset);
            }
            return MemMapping.FinalAddress;
        }

        public MemoryMappedAttribute ExtractMemoryMappedAttribute(string propertyName)
        {
            PropertyInfo property = ExtractPropertyInfo(propertyName);

            if (property == null)
            {
                // Property not found
                return null;
            }

            foreach (object attribute in property.GetCustomAttributes(true))
            {
                MemoryMappedAttribute memoryMappedAttribute = attribute as MemoryMappedAttribute;
                if (memoryMappedAttribute != null)
                {
                    // Attribute of property was found
                    return memoryMappedAttribute;
                }
            }

            // Attribute not found
            return null;
        }

        public PropertyInfo ExtractPropertyInfo(string propertyName)
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == propertyName)
                {
                    return property;
                }
            }
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
