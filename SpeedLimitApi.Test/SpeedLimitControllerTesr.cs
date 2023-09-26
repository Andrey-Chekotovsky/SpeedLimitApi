using SpeedLimitApi.Controllers;
using SpeedLimitApi.Exceprions;
using SpeedLimitApi.Models;
using SpeedLimitApi.Repositories;
using System.Text.Json;
using Xunit;

namespace SpeedLimitApi.Test
{
    public class SpeedLimitControllerTesr
    {
        private SpeedLimitController controller = new SpeedLimitController();
        private CarSpeedRepository carSpeedRepository = new CarSpeedRepository();
        private List<CarSpeed> carSpeeds = new List<CarSpeed>{ new CarSpeed("9587-HT8", 71.0F, DateTime.Now),
            new CarSpeed("9567-HT8", 100.0F, DateTime.Now), new CarSpeed("9907-MK8", 15.0F, DateTime.Now),
            new CarSpeed("9887-TH8", 47.0F, DateTime.Now) };

        [Fact]
        public void SpeedLimitController_SendDate_ReturnMinAndMaxSpeed()
        {
            carSpeedRepository.DeleteFile(DateOnly.FromDateTime(DateTime.Now));
            controller.PostCarSpeed(carSpeeds[0]);
            controller.PostCarSpeed(carSpeeds[1]);
            controller.PostCarSpeed(carSpeeds[2]);
            controller.PostCarSpeed(carSpeeds[3]);
            string rez = controller.GetMinAndMax(DateOnly.FromDateTime(DateTime.Now).ToString());
            var list = JsonSerializer.Deserialize<List<CarSpeed>>(rez);
            Assert.True(carSpeeds[2].Equals(list[0]));
        }
        [Fact]
        public void SpeedLimitController_SendDateAndSpeed_ReturnSpeedLimitIntruders()
        {
            string rez = controller.getSpeedLimitIntruders(DateOnly.FromDateTime(DateTime.Now).ToString(), 90.0F);
            var list = JsonSerializer.Deserialize<List<CarSpeed>>(rez);
            if (carSpeeds[1].Equals(list[0])) {
                Console.WriteLine("true");
            }
            if (carSpeeds[1].GetHashCode() == list[0].GetHashCode())
            {
                Console.WriteLine("true");
            }
            Assert.True(carSpeeds[1].Equals(list[0]));
        }
        [Fact]
        public void SpeedLimitController_SendDateAndWrongSpeed_ReturnSpeedLimitIntruders()
        {
            string rez = controller.getSpeedLimitIntruders("qwertr", -90.0F);
            var exception = JsonSerializer.Deserialize<InvalidDataException>(rez);
            Assert.True(new InvalidDataException().GetType() == exception.GetType());
        }
        [Fact]
        public void SpeedLimitController_SendWrongDateAndSpeed_ReturnSpeedLimitIntruders()
        {
            string rez = controller.getSpeedLimitIntruders("qwertr", -90.0F);
            var exception = JsonSerializer.Deserialize<NoDateException>(rez);
            Assert.True(new NoDateException("").GetType() == exception.GetType());
        }
    }
}