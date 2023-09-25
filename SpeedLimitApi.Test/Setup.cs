using Microsoft.AspNetCore.Mvc;
using SpeedLimitApi.Controllers;
using SpeedLimitApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedLimitApi.Test
{
    [SetUpFixture]
    public class Setup
    {
        private SpeedLimitController controller = new SpeedLimitController();
        [OneTimeSetUp]
        public void firstSetup()
        {
            controller.PostCarSpeed(new CarSpeed("9587-HT8", 71.0F, DateTime.Now));
            controller.PostCarSpeed(new CarSpeed("9567-HT8", 100.0F, DateTime.Now));
            controller.PostCarSpeed(new CarSpeed("9907-MK8", 15.0F, DateTime.Now));
            controller.PostCarSpeed(new CarSpeed("9887-TH8", 47.0F, DateTime.Now));
        }
    }
}
