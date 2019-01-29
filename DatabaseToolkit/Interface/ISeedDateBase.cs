using System.Data.Entity;
using System.Threading.Tasks;

namespace DatabaseToolkit.Inteface
{
    public interface ISeedDataBase
    {
        int Seed();
        Task<int> SeedAsync();
    }
}
