using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResistorColorCodes.Provider;

namespace ResistorColorCodes.UnitTests
{
    [TestClass]
    public class ResistorColorCodesTestBase
    {
        [TestMethod]
        public void BasicTest()
        {
            IResistorColorCodes IResistorColorCodes = new ResistorColorCodesProvidor();
            Int64 value = IResistorColorCodes.CalculateOhmValue("Yellow", "Violet", "Red", "Gold");
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void ValidateIfXMLDataisLoaded()
        {
            ResistorColorCodesProvidor provider = new ResistorColorCodesProvidor();
            Assert.IsNotNull(provider.resistorcolorCodes);
        }


        [TestMethod]        
        public void ValidateWrongColorCodesTest()
        {
            try
            {
                ResistorColorCodesProvidor provider = new ResistorColorCodesProvidor();
                provider.ValidateInput("Gold", "Violet", "Red", "Gold", provider.resistorcolorCodes.ColorCodes);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Please Enter Valid BandColors", ex.Message);
                //throw ex;
            }            
        }

        [TestMethod]
        public void ValidateSuccessColorCodesTest()
        {
            try
            {
                ResistorColorCodesProvidor provider = new ResistorColorCodesProvidor();
                provider.ValidateInput("Yellow", "Violet", "Red", "Gold", provider.resistorcolorCodes.ColorCodes);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception nopt expected");
            }
        }
    }
}
