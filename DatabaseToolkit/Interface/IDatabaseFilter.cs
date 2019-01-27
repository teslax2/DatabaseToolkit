using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseToolkit.Inteface
{
    public interface IDatabaseFilter
    {
        int Mark { get; set; }
        double Ratio { get; set; }
        DateTime StartTime { get; set; }
        string Note { get; set; }
        string Description { get; set; }
    }
}
