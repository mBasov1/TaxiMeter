using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guggenheim.MeteredTaxi.Core
{
    public class Meter
    {
        /// <summary>
        /// Peak hour Weekday Surcharge of $1.00 Monday - Friday after 4:00 PM & before 8:00 PM
        /// </summary>
        public static readonly List<DayOfWeek> PEAK_HOUR_DAYS = new List<DayOfWeek>()
        { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
        public static readonly TimeSpan PEAK_HOURS_START_TIME = new TimeSpan(16, 0, 0);
        public static readonly TimeSpan PEAK_HOURS_END_TIME = new TimeSpan(20, 0, 0);
        /// <summary>
        /// Night surcharge of $.50 after 8:00 PM & before 6:00 AM
        /// </summary>
        public static readonly TimeSpan NIGHT_END_TIME = new TimeSpan(6, 0, 0);

        public const decimal ENTRY_FARE = 3;
        public const decimal UNIT_FARE = 0.35M;
        public const decimal PEAK_HOURS_SURCHARGE = 1M;
        public const decimal NIGHT_SURCHARGE = 0.5M;
        public const decimal NYS_SURCHARGE = 0.5M;

        public static decimal CalculateFare(int MinutesTravelled,decimal MilesTravelled,DateTime StartTimeOfTrip)
        {
            Decimal TotalFare = 0;
            //Add the entry fare and state surcharge. The customer always pays these. 
            TotalFare += ENTRY_FARE;
            TotalFare += NYS_SURCHARGE;
            TotalFare += CalculateFareByDistance(MilesTravelled);
            TotalFare += CalculateFareByTime(MinutesTravelled);
            if (IsPeakHour(StartTimeOfTrip))
                TotalFare += PEAK_HOURS_SURCHARGE;
            if(IsNight(StartTimeOfTrip))
                TotalFare += NIGHT_SURCHARGE;
            return TotalFare;
        }
        public static decimal CalculateFareByDistance(decimal MilesTravelled)
        {
            if (MilesTravelled < 0)
            {
                throw new ArgumentException("Can't travel less than 6 MPH for a negative time.");
            }
            else
            //All examples/problems will have distance in units of 1/5 of a mile 
            if ((MilesTravelled % 0.2M) != 0)
            {
                throw new ArgumentException("Distance travelled less than 6 MPH must be given in units of 1/5 of a mile.");
            }
            else
            {
                return (MilesTravelled/0.2M)* UNIT_FARE;
            }
        }
        public static decimal CalculateFareByTime(int MinutesTravelled)
        {
            if (MinutesTravelled < 0)
            {
                throw new ArgumentException("Can't idle or travel more than 6 MPH for a negative time.");
            }
            else
            {
                return MinutesTravelled * UNIT_FARE;
            }
        }
        public static bool IsPeakHour(DateTime StartTimeOfTrip)
        {
            //Peak hours are Weekdays
            if (PEAK_HOUR_DAYS.Contains(StartTimeOfTrip.Date.DayOfWeek))
            {
            //After the work day ends, but before nightfall
                if(PEAK_HOURS_START_TIME <= StartTimeOfTrip.TimeOfDay && StartTimeOfTrip.TimeOfDay < PEAK_HOURS_END_TIME)
                return true;
            }
            return false;
        }
        public static bool IsNight(DateTime StartTimeOfTrip)
        {
            //After the Peak hours ends, or early in the morning.
            if (PEAK_HOURS_END_TIME <= StartTimeOfTrip.TimeOfDay || StartTimeOfTrip.TimeOfDay < NIGHT_END_TIME)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
