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
        Task CreateAsync();
        int Update(List<AssecoStuff> data);
        Task<int> UpdateAsync(List<AssecoStuff> data);
        List<AssecoStuff> Read();
        Task<List<AssecoStuff>> ReadAsync();
        int Delete(List<AssecoStuff> data);
    }
}
