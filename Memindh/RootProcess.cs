using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Memindh
{
    public abstract class RootProcess
    {

        public void Update()
        {
            PropertyInfo[] properties = (this.GetType()).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                HandleProperty(property);
            }
        }

        private void HandleProperty(PropertyInfo property)
        {
            MemoryMappedAttribute memoryMappedAttribute = GetMemoryMappedAttribute(property);
            if (memoryMappedAttribute == null) return;

            if (property.PropertyType == typeof(Entity))
            {
                property.PropertyType.GetMethod("Load").Invoke(null, null);
            }
            else
            {
                property.SetMethod.Invoke(this, null);
            }
        }

        private MemoryMappedAttribute GetMemoryMappedAttribute(PropertyInfo property)
        {
            foreach (object attribute in property.GetCustomAttributes(true))
            {
                MemoryMappedAttribute memoryMappedAttribute = attribute as MemoryMappedAttribute;
                if (memoryMappedAttribute != null)
                {
                    return memoryMappedAttribute;
                }
            }
            return null;
        }
    }
}
