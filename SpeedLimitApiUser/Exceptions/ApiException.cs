﻿namespace SpeedLimitApiUser.Exceprions
{
    [Serializable]
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) { }

    }
}
