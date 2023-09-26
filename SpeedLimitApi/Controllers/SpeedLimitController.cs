using Microsoft.AspNetCore.Mvc;
using SpeedLimitApi.Exceprions;
using SpeedLimitApi.Models;
using SpeedLimitApi.Repositories;
using System.Text.Json;

namespace SpeedLimitApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpeedLimitController : ControllerBase
    {
        private static readonly CarSpeedRepository carSpeedRepository = new CarSpeedRepository();
        private System.Timers.Timer timer = new System.Timers.Timer(10000);
        private static List<CarSpeed> carSpeeds = new List<CarSpeed>();
        public SpeedLimitController()
        {
            timer.Start();
            timer.Elapsed += OnTimedEvent;
        }
        public void addAndSort()
        {

        }
        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine(":)");
            if (carSpeeds.Count != 0)
            {
                carSpeedRepository.WriteCarSpeedWithOneDate(carSpeeds);
                carSpeeds = new List<CarSpeed>();
            }
        }
        [Route("getSpeedLimitIntruders")]
        [HttpGet]
        public string getSpeedLimitIntruders([FromQuery(Name = "date")] string strDate,
            [FromQuery(Name = "speed")] float speed)
        {
            DateOnly date;
            if (carSpeeds.Count != 0)
            {
                carSpeedRepository.WriteCarSpeedWithOneDate(carSpeeds);
                carSpeeds = new List<CarSpeed>();
            }
            try {
                date = DateOnly.Parse(strDate);
            }
            catch (FormatException ex) {
                JsonSerializer.Serialize(new InvalidDataException("Invalid date: " + ex.Message));
            }
            if (speed < 0 || speed > 1000) {
                return JsonSerializer.Serialize(
                    new InvalidDataException("Speed of car couldn't be less than 0 or greater than 1000"));
            }
            return JsonSerializer.Serialize(carSpeedRepository.GetCarSpeeds(date, speed));
        }
        [Route("getMinMax")]
        [HttpGet]
        public string GetMinAndMax([FromQuery(Name = "date")] string strDate)
        {
            DateOnly date;
            if (carSpeeds.Count != 0)
            {
                carSpeedRepository.WriteCarSpeedWithOneDate(carSpeeds);
                carSpeeds = new List<CarSpeed>();
            }
            try
            {
                date = DateOnly.Parse(strDate);
            }
            catch (FormatException ex){
                JsonSerializer.Serialize(new InvalidDataException("Invalid date: " + ex.Message));
            }
            try{
                var rezult = carSpeedRepository.GetMinMaxCarSpeed(date);
                return JsonSerializer.Serialize(new List<CarSpeed> { rezult.min, rezult.max });
            }
            catch (FileNotFoundException ex) {
                return JsonSerializer.Serialize(new NoDateException("There is no data about this date: " + ex.Message));
            }
        }
        [Route("PostCarSpeed")]
        [HttpPost]
        public void PostCarSpeed([FromBody] CarSpeed carSpeed)
        {
            carSpeeds.Add(carSpeed);
        }
    }
}