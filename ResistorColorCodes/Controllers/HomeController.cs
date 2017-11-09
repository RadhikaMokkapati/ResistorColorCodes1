using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using ResistorColorCodes.Provider;

namespace ResistorColorCodes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            GetdropdownValues();
            return View();
        }
        


        [HttpPost]
        public ActionResult Submit(string colorA, string colorB, string colorC, string colorD)
        {
            GetdropdownValues();
            IResistorColorCodes IResistorColorCodes = new ResistorColorCodesProvidor();
            Int64 value = IResistorColorCodes.CalculateOhmValue(colorA, colorB, colorC, colorD);
            ViewBag.value = value;
            return View("Index", value);
        }

        private void GetdropdownValues()
        {

            List<SelectListItem> test = new List<SelectListItem>();
            Models.ResistorColorCodes resistorcolorCodes = null;
            ResistorColorCodesProvidor provider = new ResistorColorCodesProvidor();
            resistorcolorCodes = provider.resistorcolorCodes;

            List<SelectListItem> items = (from code in resistorcolorCodes.ColorCodes
                                          where !string.IsNullOrEmpty(code.SignificantFigues)
                                          select new SelectListItem
                                          {
                                              Text = code.Color,
                                              Value = code.Color.ToString(),
                                              Selected = true
                                          }).ToList();
            ViewBag.ColorA = items;
            ViewBag.ColorB = items;

            items = (from code in resistorcolorCodes.ColorCodes
                     where !string.IsNullOrEmpty(code.Multiplier)
                     select new SelectListItem
                     {
                         Text = code.Color,
                         Value = code.Color.ToString(),
                         Selected = true
                     }).ToList();
            ViewBag.colorC = items;

            items = (from code in resistorcolorCodes.ColorCodes
                     where !string.IsNullOrEmpty(code.Tolerance)
                     select new SelectListItem
                     {
                         Text = code.Color,
                         Value = code.Color.ToString(),
                         Selected = true
                     }).ToList();
            ViewBag.colorD = items;
        }
    }
}