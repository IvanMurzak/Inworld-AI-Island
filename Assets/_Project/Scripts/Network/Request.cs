using System;
using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;
using _Project.Network.Data;
using _Project.UI;

namespace _Project.Network
{
    public static class Request
    {
        public static void Geocode(string text, Action<GeocodeResponse> onReceived = null)
        {
            RestClient.Get(new RequestHelper()
            {
                Uri = "https://geocode.xyz/",
                ContentType = "application/json",
                Params = new()
                {
                    { "scantext", text },
                    { "geoit", "json" }
                }
            }).Then(response =>
            {
                Debug.Log("Geocode: Status " + response.StatusCode.ToString() + " Ok");
                var data = JsonConvert.DeserializeObject<GeocodeResponse>(response.Text);
                onReceived?.Invoke(data);
            }).Catch(e => PopupManager.Instance.CreateError(e.Message));
        }
        public static void WeatherLocation(string latt, string longt, Action<ApiWeatherLocationResponse> onReceived = null)
        {
            RestClient.Get(new RequestHelper()
            {
                Uri = $"https://api.weather.gov/points/{latt},{longt}"
            }).Then(response =>
            {
                Debug.Log("Weather Location: Status " + response.StatusCode.ToString() + " Ok");
                var data = JsonConvert.DeserializeObject<ApiWeatherLocationResponse>(response.Text);
                onReceived?.Invoke(data);
            }).Catch(e => PopupManager.Instance.CreateError(e.Message));
        }
        public static void WeatherForecast(string url, Action<ApiWeatherForecastResponse> onReceived = null)
        {
            RestClient.Get(new RequestHelper()
            {
                Uri = url
            }).Then(response =>
            {
                Debug.Log("Weather Forecast: Status " + response.StatusCode.ToString() + " Ok");
                var data = JsonConvert.DeserializeObject<ApiWeatherForecastResponse>(response.Text);
                onReceived?.Invoke(data);
            }).Catch(e => PopupManager.Instance.CreateError(e.Message));
        }
    }
}