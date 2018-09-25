using System;
using Guggenheim.MeteredTaxi.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guggenheim.MeteredTaxi.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void WeekDayAfternoonsArePeakHours()
        {
            //Friday the 8th of October, 2010, 5:30 PM
            Assert.IsTrue(Meter.IsPeakHour(new DateTime(2010,10,08,17,30,0)));
        }
        [TestMethod]
        public void WeekDaysArentPeakHours()
        {
            //Friday the 8th of October, 2010, 3:00 PM
            Assert.IsFalse(Meter.IsPeakHour(new DateTime(2010, 10, 08, 15, 00, 0)));
        }
        [TestMethod]
        public void WeekEndAfternoonsArentPeakHours()
        {
            //Saturday the 9th of October, 2010, 5:30 PM
            Assert.IsFalse(Meter.IsPeakHour(new DateTime(2010, 10, 09, 17, 30, 0)));
        }
        [TestMethod]
        public void WeekNightsArentPeakHours()
        {
            //Friday the 8th of October, 2010, 8:00 PM
            Assert.IsFalse(Meter.IsPeakHour(new DateTime(2010, 10, 08, 20, 00, 0)));
        }
        [TestMethod]
        public void NightsAreAfterPeakHours()
        {
            //Friday the 8th of October, 2010, 8:00 PM
            Assert.IsTrue(Meter.IsNight(new DateTime(2010, 10, 08, 20, 00, 0)));
        }
        [TestMethod]
        public void NightsAreEarlyMornings()
        {
            //Friday the 8th of October, 2010, 1:00 AM
            Assert.IsTrue(Meter.IsNight(new DateTime(2010, 10, 08, 1, 00, 0)));
        }
        [TestMethod]
        public void NightsAreOnWeekends()
        {
            //Saturday the 9th of October, 2010, 1:00 AM
            Assert.IsTrue(Meter.IsNight(new DateTime(2010, 10, 09, 1, 00, 0)));
        }
        [TestMethod,ExpectedException(typeof(ArgumentException))]
        public void CantTravelNegativeTime()
        {
            Meter.CalculateFareByTime(-1);
        }
        [TestMethod]
        public void CanTravelNoTime()
        {
            //Never Travels at more than 6 mph
            Assert.AreEqual(0,Meter.CalculateFareByTime(0));
        }
        [TestMethod]
        public void TravelFiveMinutes()
        {
            //Travels at more than 6 mph for 5 mins
            Assert.AreEqual(1.75M, Meter.CalculateFareByTime(5));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CantTravelNegativeDistance()
        {
            Meter.CalculateFareByDistance(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AllDistancesMeasuredInFifths()
        {
            Meter.CalculateFareByDistance(0.25M);
        }
        [TestMethod]
        public void CanTravelNoDistance()
        {
            //Never Travels at less than 6 mph
            Assert.AreEqual(0, Meter.CalculateFareByDistance(0));
        }
        [TestMethod]
        public void TravelTwoMiles()
        {
            //Travels at less than 6 mph for 2 miles
            Assert.AreEqual(3.50M, Meter.CalculateFareByDistance(2));
        }
        [TestMethod]
        public void TravelTwoAndAFifthMiles()
        {
            //Travels at less than 6 mph for 2 and 1/5 miles
            Assert.AreEqual(3.85M, Meter.CalculateFareByDistance(2.2M));
        }
        [TestMethod]
        public void ProvidedExampleTest()
        {
            //Travels at less than 6 mph for 2 and 1/5 miles
            Assert.AreEqual(9.75M, Meter.CalculateFare(5,2, new DateTime(2010, 10, 08, 17, 30, 0)));
        }
    }
}
