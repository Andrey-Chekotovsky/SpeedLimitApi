
using RestSharp;
using SpeedLimitApi.Models;
using SpeedLimitApiUser;
using SpeedLimitApiUser.Exceprions;
using SpeedLimitApiUser.Exceptions;
using System.Text.Json;

const string getIntruders = "https://localhost:7237/SpeedLimit/getSpeedLimitIntruders";
const string getMInMax = "https://localhost:7237/SpeedLimit/getMinMax";
int switcher = -1;
var client = new RestClient("https://localhost:7237/SpeedLimit/");
while (switcher != 0)
{
    try
    {
        Console.WriteLine("0 - exit, 1 - intruders, 2 - mi and max speed");
        switcher = int.Parse(Console.ReadLine());
        switch (switcher)
        {
            case 0:
                Console.WriteLine("bye");
                break;
            case 1:
                await ResponceHandler.IntrudersResponce();
                break;
            case 2:
                await ResponceHandler.MinMaxResponce();
                break;
            default:
                Console.WriteLine("well...");
                break;
        }
    }
    catch (Exception ex) when (ex is ApiException ||
                               ex is UnknownApiException ||
                               ex is ApiConnectionException)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}





