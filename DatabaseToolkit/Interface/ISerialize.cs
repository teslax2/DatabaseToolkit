using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseToolkit.Interface
{
    public interface ISerialize<T> where T:class
    {
        bool Save(T data);
        T Load();
    }
}
