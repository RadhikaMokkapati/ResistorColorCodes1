using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistorColorCodes.Provider
{
    public interface IResistorColorCodes
    {
        Int64 CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor);
    }
}
