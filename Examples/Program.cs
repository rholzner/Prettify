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
            var rnd =  new Random();
            var startDate = DateTime.UtcNow;
            while(true)
            {
                var daysToAdd = rnd.Next(0, 30);
                Thread.Sleep(100);
                var monthToAdd = rnd.Next(0, 12);
                Thread.Sleep(100);
                var minToAdd = rnd.Next(1, 60);
                Thread.Sleep(100);
                var secToAdd = rnd.Next(1, 60);
                
                Thread.Sleep(100);
                var year = rnd.Next(0,1);

                var endDate = startDate.AddMonths(monthToAdd).AddDays(daysToAdd).AddMinutes(minToAdd).AddSeconds(secToAdd).AddYears(year);
                Console.WriteLine($"start: {startDate}");
                Console.WriteLine($"end: {endDate}");

                var timeDif = endDate - startDate;

                var s = timeDif.Prettify()
                    .HasPassed(text: "time has passed")
                    .Hours(text: "in nr hours", placeholder: "nr")
                    .Days(text: "in nrdays days", placeholder: "nrdays",low: 1,top: 14)
                    .Weeks(text: "in nr weeks", placeholder: "nr", low: 15, top: 352)
                    .Special(text: "in nr years", placeholder: "nr",opt => { return opt / 365; },PrettifyOnTimeStamp.Days,opt => { return opt.ToString(); })
                    .NoMatch(text: "no match")
                    .Print();

                Console.WriteLine(s);
                Console.ReadLine();
            }
        }
    }
}
