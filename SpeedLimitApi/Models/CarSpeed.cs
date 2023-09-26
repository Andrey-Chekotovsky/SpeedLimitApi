using Microsoft.Diagnostics.Tracing.Parsers.Clr;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json.Serialization;

namespace SpeedLimitApi.Models
{
    [Serializable]
    public class CarSpeed : IComparable<CarSpeed>
    {
        [JsonPropertyName("speed")]
        private float speed;
        [JsonPropertyName("carNumber")]
        private char[] carNumber = new char[8];
        [JsonPropertyName("registeredAt")]
        private DateTime registeredAt;
        [NonSerialized]
        public static readonly int Size = 205; 
        public CarSpeed()
        {
}
        public CarSpeed(float speed)
        {
            this.speed = speed;
            this.registeredAt = DateTime.Now;
            this.carNumber = "0000-OO0".ToCharArray();
        }

        public CarSpeed(char[] carNumber, float speed, DateTime registeredAt)
        {
            this.carNumber = carNumber;
            this.speed = speed;
            this.registeredAt = registeredAt;
        }

        public char[] CarNumber { get { return carNumber; } set { carNumber = value; } }
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
                   this.speed == speed.speed;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(carNumber, registeredAt);
        }

        public override string? ToString()
        {
            return this.CarNumber + " " + registeredAt.ToString() + " " + speed;
        }
    }
}
