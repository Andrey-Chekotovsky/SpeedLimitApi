using SpeedLimitApi.Models;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text.Json;

namespace SpeedLimitApi.Repositories
{
    public class CarSpeedRepository
    {
        public CarSpeedRepository() {
        }
        private string CreatePath(DateTime dateTime)
        {
            DateOnly date = DateOnly.FromDateTime(dateTime);
            return CreatePath(date);
        }
        private string CreatePath(DateOnly date)
        {
            return Path.Combine(Environment.CurrentDirectory, @"Data\", date.ToString("D") + ".json");
        }
        public void WriteCarSpeed(CarSpeed carSpeed)
        {
            string path = CreatePath(carSpeed.RegisteredAt);
            var str = JsonSerializer.Serialize(carSpeed);
            if (File.Exists(path))
            {
                str = "," + str;
            }
            else
            {
                File.Create(path).Close();
            }
            using (StreamWriter stream = new StreamWriter(path, true))
            {
                stream.Write(str);
            }
        }
        public void WriteCarSpeedWithOneDate(List<CarSpeed> carSpeeds)
        {
            string path = CreatePath(carSpeeds[0].RegisteredAt);
            var str = JsonSerializer.Serialize(carSpeeds);
            str = str.Remove(0, 1);
            Console.WriteLine(str[0]);
            str = str.Remove(str.Length - 1, 1);
            if (File.Exists(path))
            {
                str = "," + str;
            }
            else
            {
                File.Create(path).Close();
            }
            using (StreamWriter stream = new StreamWriter(path, true))
            {
                stream.Write(str);
            }
        }
        public IEnumerable<CarSpeed> GetCarSpeeds(DateOnly date, float lowestSpeed) 
        {
            string path = CreatePath(date);
            var carSpeeds = new List<CarSpeed>();
            using (StreamReader stream = new StreamReader(path))
            {
                string str = "[" + stream.ReadToEnd() + "]";
                carSpeeds = JsonSerializer.Deserialize<List<CarSpeed>>(str);
            }
            var rez = from carSpeed in carSpeeds where carSpeed.Speed > lowestSpeed select carSpeed;
            return rez;
        }
        public (CarSpeed min, CarSpeed max) GetMinMaxCarSpeed(DateOnly date)
        {
            string path = CreatePath(date);
            var carSpeeds = new List<CarSpeed>();
            using (StreamReader stream = new StreamReader(path))
            {
                string str = "[" + stream.ReadToEnd() + "]";
                carSpeeds = JsonSerializer.Deserialize<List<CarSpeed>>(str);
            }
            carSpeeds.Sort();

            return (carSpeeds[0], carSpeeds[carSpeeds.Count - 1]);
        }
        public void DeleteFile(DateOnly date)
        {
            string path = CreatePath(date);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
