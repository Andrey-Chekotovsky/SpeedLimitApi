using RestSharp;
using SpeedLimitApi.Models;
using SpeedLimitApiUser.Exceprions;
using SpeedLimitApiUser.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpeedLimitApiUser
{
    internal class ResponceHandler
    {
        static readonly public RestClient client = new RestClient("https://localhost:7237/SpeedLimit/");
        static private Exception DefineException(string str)
        {
           
            try
            {
                return JsonSerializer.Deserialize<ApiException>(str);
            }
            catch (JsonException ex)
            {
                return new  UnknownApiException(str);
            }
        }
        static private List<CarSpeed> ParceInpet(string? str)
        {
            try
            {
                str = new string((from c in str
                                  where c != '\\'
                                  select c
               ).ToArray());
            }
            catch (ArgumentNullException ex)
            {
                throw new ApiConnectionException("Cannot connect to api");
            }
            str = str.Remove(0, 1);
            str = str.Remove(str.Length - 1, 1);
            try
            {
                return JsonSerializer.Deserialize<List<CarSpeed>>(str);
            }
            catch (JsonException ex)
            {
                throw DefineException(str);
            }
        }
        static async public Task IntrudersResponce()
        {
            DateOnly date = UserInputHandler.EnterDate();
            Console.WriteLine("Enter speed");
            float speed = UserInputHandler.EnterWithLimits(0F, 1000F);
            var request = new RestRequest("getSpeedLimitIntruders?date=" + date.ToString() + "&speed=" + speed);
            var response = await client.ExecuteGetAsync(request);
            var list = ParceInpet(response.Content);
            list.ForEach(car => Console.WriteLine(car));
        }
        static async public Task MinMaxResponce()
        {
            DateOnly date = UserInputHandler.EnterDate();
            var request = new RestRequest("getMinMax?date=" + date.ToString());
            var response = await client.ExecuteGetAsync(request);
            var list = ParceInpet(response.Content);
            list.ForEach(car => Console.WriteLine(car));
        }
    }
    
}
