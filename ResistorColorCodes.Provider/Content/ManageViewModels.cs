using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ResistorColorCodes.Models
{

    public class ResistorColorCodes
    {
        public ColorCode[] ColorCodes { get; set; }
    }

    public class ColorCode
    {
        public int ColorId { get; set; }

        public string Color { get; set; }

        public string SignificantFigues { get; set; }

        public string Multiplier { get; set; }

        public string Tolerance { get; set; }
    }
}
