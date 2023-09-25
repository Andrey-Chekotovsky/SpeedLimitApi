using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SpeedLimitApi.Models
{
    [Serializable]
    public class CarSpeed : IComparable<CarSpeed>
    {
        [JsonPropertyName("carNumber")]
        private string carNumber;
        [JsonPropertyName("speed")]
        private float speed;
        [JsonPropertyName("registeredAt")]
        private DateTime registeredAt;
        public CarSpeed()
        {
}
        public CarSpeed(float speed)
        {
            this.speed = speed;
            this.registeredAt = DateTime.Now;
            this.carNumber = "0000-OO0";
        }

        public CarSpeed(string carNumber, float speed, DateTime registeredAt)
        {
            this.carNumber = carNumber;
            this.speed = speed;
            this.registeredAt = registeredAt;
        }

        public string CarNumber { get { return carNumber; } set { carNumber = value; } }
        public float Speed { get { return speed; } set { speed = value; } }
        public DateTime RegisteredAt { get {  return registeredAt; } set {  registeredAt = value; } }

        public int CompareTo(CarSpeed? other)
        {
            if (other == null) {
                return -1; 
            }
            if (this.speed > other.speed) {
                return 1;
            }
            else {
                return -1; 
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is CarSpeed speed &&
                   carNumber == speed.carNumber &&
                   this.speed == speed.speed &&
                   registeredAt == speed.registeredAt;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(carNumber, speed, registeredAt);
        }
    }
}
