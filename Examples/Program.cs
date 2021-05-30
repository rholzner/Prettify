using System;
using System.Threading;
using Prettify;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var rnd = new Random();
            var startDate = DateTime.UtcNow;
            while (true)
            {
                var daysToAdd = rnd.Next(0, 30);
                Thread.Sleep(100);
                var monthToAdd = rnd.Next(0, 12);
                Thread.Sleep(100);
                var minToAdd = rnd.Next(1, 60);
                Thread.Sleep(100);
                var secToAdd = rnd.Next(1, 60);

                Thread.Sleep(100);
                var year = rnd.Next(0, 1);

                var endDate = startDate.AddMonths(monthToAdd).AddDays(daysToAdd).AddMinutes(minToAdd).AddSeconds(secToAdd).AddYears(year);
                Console.WriteLine($"start: {startDate}");
                Console.WriteLine($"end: {endDate}");

                var timeDif = endDate - startDate;

                var s = timeDif.Prettify()
                    .HasPassed(text: "time has passed")
                    .NoMatch(text: "no match")
                    .Rule("Minutes nr left", "nr", opt => { return Math.Round(opt, MidpointRounding.AwayFromZero); }, opt => { return opt.ToString(); }, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 1, 0, 0, 0), PrettifyOnTimeStamp.Minutes)
                    .Rule("Hours nr left", "nr", opt => { return Math.Round(opt, MidpointRounding.AwayFromZero); }, opt => { return opt.ToString(); }, new TimeSpan(0, 1, 0, 0, 0), new TimeSpan(2, 0, 0, 0, 0), PrettifyOnTimeStamp.Hours)
                    .Rule("Days nr left", "nr", opt => { return Math.Round(opt, MidpointRounding.AwayFromZero); }, opt => { return opt.ToString(); }, new TimeSpan(2, 0, 0, 0, 0), new TimeSpan(60, 0, 0, 0, 0), PrettifyOnTimeStamp.Days)
                    .Rule("Weeks nr left", "nr", opt => { return Math.Round(opt / 7, MidpointRounding.AwayFromZero); }, opt => { return opt.ToString(); }, new TimeSpan(60, 0, 0, 0, 0), new TimeSpan(365, 0, 0, 0, 0), PrettifyOnTimeStamp.Days)
                    .Rule("Years nr left", "nr", opt => { return Math.Round(opt / 365, MidpointRounding.AwayFromZero); }, opt => { return opt.ToString(); }, new TimeSpan(365, 0, 0, 0, 0), new TimeSpan(1500, 0, 0, 0, 0), PrettifyOnTimeStamp.Days)
                    .Print();

                var s1 = endDate.Prettify(startDate)
                    .HasPassed(text: "time has passed")
                    .NoMatch(text: "no match")
                    .Rule("Hours nr left", "nr", opt => { return opt.ToString(); }, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(2, 0, 0, 0, 0), PrettifyOnTimeStamp.Hours)
                    .Rule("Days nr left", "nr", opt => { return opt.ToString(); }, new TimeSpan(2, 0, 0, 0, 0), new TimeSpan(60, 0, 0, 0, 0), PrettifyOnTimeStamp.Days)
                    .Rule("Weeks nr left", "nr", opt => { return Math.Round(opt / 7, MidpointRounding.AwayFromZero); }, opt => { return opt.ToString(); }, new TimeSpan(60, 0, 0, 0, 0), new TimeSpan(365, 0, 0, 0, 0), PrettifyOnTimeStamp.Days)
                    .Rule("Years nr left", "nr", opt => { return Math.Round(opt / 365, MidpointRounding.AwayFromZero); }, opt => { return opt.ToString(); }, new TimeSpan(365, 0, 0, 0, 0), new TimeSpan(1500, 0, 0, 0, 0), PrettifyOnTimeStamp.Days)
                    .Print();


                Console.WriteLine(s);
                Console.WriteLine(s1);
                Console.ReadLine();
            }
        }
    }
}
