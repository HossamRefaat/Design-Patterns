using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePattern.Core
{
    internal interface IPrototype<T>
    {
        public T DeepClone();
        public T ShallowClone();
    }
}
