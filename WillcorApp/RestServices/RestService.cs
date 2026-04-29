using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
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
                if (todaysList != null)
                {
                    return todaysList;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to retrieve today's runs: {ex.Message}", "OK");
            }

            return new PickupRunDto();
        }

        public async Task<List<Client>> GetClients()
        {
            try
            {
                var response = await _client.GetStringAsync("api/Clients");
                var clients = JsonConvert.DeserializeObject<List<Client>>(response);
                if (clients != null)
                {
                    return clients;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to retrieve clients: {ex.Message}", "OK");
            }

            return new List<Client>();
        }

        public async Task AddExtraPickup(int runID, AddPickup addPickup)
        {
            try
            {
                var response = await _client.PostAsJsonAsync($"api/PickupRuns/{runID}/extra-pickup", addPickup);
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    await Shell.Current.DisplayAlert("Error", $"Failed to add extra pickup: {response.ReasonPhrase} - {errorContent}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to add extra pickup: {ex.Message}", "OK");
            }
        }

        public async Task MarkPickupComplete(int itemId, UpdatePickupRunItemDto updatePickUp)
        {
            try
            {
                var response = await _client.PutAsJsonAsync($"api/PickupRuns/item/{itemId}", updatePickUp);

                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", $"Unable to mark pickup complete: {response.ReasonPhrase}" , "OK");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", $"Unable to mark pickup complete: {ex.Message}", "OK");
            }
        }

    }
}
