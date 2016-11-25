using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Singleton<T>:Singleton
    {
        static T instance;

        public static T Instance
        {
            get { return instance; }
            set 
            {
                instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }

    public class Singleton
    {
        static readonly IDictionary<Type, object> allSingletions;
        static Singleton()
        {
            allSingletions = new Dictionary<Type, object>();
        }

        public static IDictionary<Type, object> AllSingletons
        {
            get { return allSingletions; }
        }
    }
}
