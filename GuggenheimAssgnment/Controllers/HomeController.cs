using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guggenheim.MeteredTaxi.Core;

namespace GuggenheimAssgnment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost ]
        public decimal CalculateFare(int minutesTravelled,decimal milesTravlled,string startTime)
        {
            var CalculatedFare = Meter.CalculateFare(minutesTravelled, milesTravlled, DateTime.Parse(startTime));
            return CalculatedFare;
        }
    }
}