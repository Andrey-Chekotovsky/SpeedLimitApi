namespace SpeedLimitApi.Exceprions
{
    [Serializable]
    public class NoDateException : Exception
    {
        public NoDateException(string message) : base(message){ }
        
    }
}
