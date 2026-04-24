using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WillcorApp.Models;

namespace WillcorApp.RestServices
{
    public class RestService
    {
        HttpClient _client;

        public static string BaseAddress = "https://willcorapi-a9cybbh0e4dkhnhe.canadacentral-01.azurewebsites.net/";

        public RestService() 
        {
            _client = new HttpClient() { BaseAddress = new Uri(BaseAddress) };
        }

        public async Task<PickupRunDto> GetTodayRuns()
        {
            try
            {
                var response = await _client.GetStringAsync("api/PickupRuns/today");
                var todaysList = JsonConvert.DeserializeObject<PickupRunDto>(response);
                return todaysList;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to retrieve today's runs: {ex.Message}", "OK");
            }

            return new PickupRunDto();
        }

    }
}
