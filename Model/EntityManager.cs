using Memindh;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EntityManager
    {
        private static EntityManager instance;

        private Dictionary<string, RootProcess> rootProcesses;


        private EntityManager()
        {
            rootProcesses = new Dictionary<string, RootProcess>();
        }

        public void RegisterRootEntity(string identifier, RootProcess entityType)
        {
            rootProcesses.Add(identifier, entityType);
        }

        public void InitializeMemoryMappings()
        {
            foreach (KeyValuePair<string, RootProcess> kvp in rootProcesses)
            {

            }

            PropertyInfo[] properties = (typeof(Global)).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine("Property: " + property.Name);
                foreach (object attribute in property.GetCustomAttributes(true))
                {
                    MemoryMappedAttribute memoryMappedAttribute = attribute as MemoryMappedAttribute;
                    if (memoryMappedAttribute != null)
                    {
                        // do whatever
                    }
                }
            }
        }

        public EntityManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EntityManager();
                }
                return instance;
            }
        }
    }
}
