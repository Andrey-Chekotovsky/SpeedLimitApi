using SpeedLimitApi.Controllers;
using SpeedLimitApi.Models;
using SpeedLimitApi.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedLimitApi.Test
{
    internal class BenchmarkTest
    {
        private static SpeedLimitController controller = new SpeedLimitController();
        private static CarSpeedRepository carSpeedRepository = new CarSpeedRepository();
        private static List<CarSpeed> carSpeeds = new List<CarSpeed>{ new CarSpeed("9587-HT8", 71.0F, DateTime.Now),
            new CarSpeed("9567-HT8", 100.0F, DateTime.Now), new CarSpeed("9907-MK8", 15.0F, DateTime.Now),
            new CarSpeed("9887-TH8", 47.0F, DateTime.Now) };
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("Method: PostCarSpeed");
            stopwatch.Start();
            controller.PostCarSpeed(carSpeeds[0]);
            stopwatch.Stop();
            var rezult = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Time: " + rezult);
            Console.WriteLine("---------------------------");
            stopwatch.Restart();
            Console.WriteLine("Adding 400000 objects");
            for (int i = 0; i < 100000; i++)
            {
                controller.PostCarSpeed(carSpeeds[0]);
                controller.PostCarSpeed(carSpeeds[1]);
                controller.PostCarSpeed(carSpeeds[2]);
                controller.PostCarSpeed(carSpeeds[3]);
            }
            stopwatch.Stop();
            rezult = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Time: " + rezult);
            Console.WriteLine("---------------------------");
            Console.WriteLine("Method: GetMinAndMax");
            stopwatch.Restart();
            controller.GetMinAndMax(DateOnly.FromDateTime(DateTime.Now).ToString());
            stopwatch.Stop();
            rezult = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Time: " + rezult);
            Console.WriteLine("---------------------------");
            Console.WriteLine("Method: GetMinAndMax");
            stopwatch.Restart();
            controller.GetMinAndMax(DateOnly.FromDateTime(DateTime.Now).ToString());
            stopwatch.Stop();
            rezult = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Time: " + rezult);
            Console.WriteLine("---------------------------");
            Console.WriteLine("Method: getSpeedLimitIntruders");
            stopwatch.Restart();
            controller.getSpeedLimitIntruders(DateOnly.FromDateTime(DateTime.Now).ToString(), 60.0F);
            stopwatch.Stop();
            rezult = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Time: " + rezult);
        }
    }
}
