using System;
using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;
using _Project.Network.Data;

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
            });
        }
    }
}