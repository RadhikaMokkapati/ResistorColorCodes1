using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ResistorColorCodes.Models;

namespace ResistorColorCodes.Provider
{
    public class ResistorColorCodesProvidor : IResistorColorCodes
    {
        public Models.ResistorColorCodes resistorcolorCodes = null;
        public ResistorColorCodesProvidor()
        {
            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.Replace(".UnitTests\\bin", "")) + ".Provider\\Content\\ColorCodes.xml";
            XmlSerializer deserializer = new XmlSerializer(typeof(Models.ResistorColorCodes));
            using (TextReader textReader = new StreamReader(_filePath))
            {
                resistorcolorCodes = (Models.ResistorColorCodes)deserializer.Deserialize(textReader);
            }
        }

        public Int64 CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
        {
            Int64 maxresistance = 0;

            try
            {
                // Validate ColorCodes
                ValidateInput(bandAColor, bandBColor, bandCColor, bandDColor, resistorcolorCodes.ColorCodes);

                //Get Color Codes
                ColorCode bandAColorChar = GetColorCode(bandAColor, resistorcolorCodes.ColorCodes);
                ColorCode bandBColorChar = GetColorCode(bandBColor, resistorcolorCodes.ColorCodes);
                ColorCode bandCColorChar = GetColorCode(bandCColor, resistorcolorCodes.ColorCodes);
                ColorCode bandDColorChar = GetColorCode(bandDColor, resistorcolorCodes.ColorCodes);

                Int64 AverageResistance = Convert.ToInt64(bandAColorChar.SignificantFigues + bandBColorChar.SignificantFigues) * Convert.ToInt64(bandCColorChar.Multiplier);

                double TolernaceCalc = (Convert.ToDouble(bandDColorChar.Tolerance) / 100) * AverageResistance;

                maxresistance = AverageResistance + Convert.ToInt64(TolernaceCalc);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return maxresistance;
        }

        public void ValidateInput(string bandAColor, string bandBColor, string bandCColor, string bandDColor, ColorCode[] resistorcolorCodes)
        {
            //Validate BandAcolor
            if (string.IsNullOrEmpty(bandAColor) || string.IsNullOrEmpty(bandBColor) || string.IsNullOrEmpty(bandCColor) || string.IsNullOrEmpty(bandDColor))
            {
                throw new Exception("Please Enter BandColors");
            }

            //Validate BandA color is valid and has significunt nubers
            ColorCode colorACode = resistorcolorCodes.FirstOrDefault(color => color.Color == bandAColor);
            if (colorACode == null || colorACode.SignificantFigues == string.Empty)
                throw new Exception("Please Enter Valid BandColors");

            //Validate BandB color is valid and has significunt nubers
            ColorCode colorBCode = resistorcolorCodes.FirstOrDefault(color => color.Color == bandBColor);
            if (colorBCode == null || colorBCode.SignificantFigues == string.Empty)
                throw new Exception("Please Enter Valid BandColors");

            //Validate BandC color is valid and has significunt nubers
            ColorCode colorCCode = resistorcolorCodes.FirstOrDefault(color => color.Color == bandCColor);
            if (colorCCode == null || colorCCode.Multiplier == string.Empty)
                throw new Exception("Please Enter Valid BandColors");

            //Validate BandD color is valid and has significunt nubers
            ColorCode colorDCode = resistorcolorCodes.FirstOrDefault(color => color.Color == bandDColor);
            if (colorDCode == null || colorDCode.Tolerance == string.Empty)
                throw new Exception("Please Enter Valid BandColors");
        }

        private static ColorCode GetColorCode(string bandColor, ColorCode[] resistorcolorCodes)
        {
            ColorCode colorCode = resistorcolorCodes.FirstOrDefault(color => color.Color == bandColor);
            return colorCode;
        }


    }
}
