using DatabaseToolkit.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseToolkit.Inteface
{
    public interface IDateBaseOperations
    {
        void Create();
        int Update(List<AssecoStuff> data);
        List<AssecoStuff> Read();
        int Delete(List<AssecoStuff> data);
    }
}
