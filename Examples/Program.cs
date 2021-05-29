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
                    .HasPassed("tiden har passerat")
                    .Hours("om nr timmer", "nr")
                    .Days("om nrdays dagar", "nrdays", 1, 14)
                    .Weeks("om nr veckor", "nr", 15, 352)
                    .Special("om nr år","nr",opt => { return opt / 365; },PrettifyOnTimeStamp.Days,opt => { return opt.ToString(); })
                    .NoMatch("no match")
                    .Print();

                Console.WriteLine(s);
                Console.ReadLine();
            }
        }
    }
}
