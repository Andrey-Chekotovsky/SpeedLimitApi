using SpeedLimitApi.Controllers;
using SpeedLimitApi.Models;
using SpeedLimitApi.Repositories;
using System.Text.Json;

namespace SpeedLimitApi.Test
{
    public class SpeedLimitControllerTesr
    {
        private SpeedLimitController controller = new SpeedLimitController();
        private CarSpeedRepository carSpeedRepository = new CarSpeedRepository();
        private List<CarSpeed> carSpeeds = new List<CarSpeed>{ new CarSpeed("9587-HT8", 71.0F, DateTime.Now),
            new CarSpeed("9567-HT8", 100.0F, DateTime.Now), new CarSpeed("9907-MK8", 15.0F, DateTime.Now),
            new CarSpeed("9887-TH8", 47.0F, DateTime.Now) };

        [Test, Order(1)]
        public void SpeedLimitController_SendDate_ReturnMinAndMaxSpeed()
        {
            carSpeedRepository.DeleteFile(DateOnly.FromDateTime(DateTime.Now));
            controller.PostCarSpeed(carSpeeds[0]);
            controller.PostCarSpeed(carSpeeds[1]);
            controller.PostCarSpeed(carSpeeds[2]);
            controller.PostCarSpeed(carSpeeds[3]);
            string rez = controller.GetMinAndMax(DateOnly.FromDateTime(DateTime.Now).ToString());
            var list = JsonSerializer.Deserialize<List<CarSpeed>>(rez);
            Assert.That(new List<CarSpeed>{
                carSpeeds[2], carSpeeds[1]}, Is.EqualTo(list));
        }
        [Test]
        public void SpeedLimitController_SendDateAndSpeed_ReturnSpeedLimitIntruders()
        {
            string rez = controller.getSpeedLimitIntruders(DateOnly.FromDateTime(DateTime.Now).ToString(), 90.0F);
            var list = JsonSerializer.Deserialize<List<CarSpeed>>(rez);
            Assert.That(new List<CarSpeed>{
                carSpeeds[1] }, Is.EqualTo(list));
        }
        
    }
}