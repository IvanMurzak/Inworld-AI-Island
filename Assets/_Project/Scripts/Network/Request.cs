using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;
using _Project.Network.Data;
using System;

namespace _Project.Network
{
    public static class Request
    {
        public static void Geocode(string text, Action<GeocodeResponse> onReceived = null)
        {
            RestClient.Post("https://geocode.xyz", new RequestHelper()
            {
                Params = new()
                {
                    { "scantext", text },
                    { "geoit", "json" }
                }
            }).Then(response =>
            {
                Debug.Log("Status " + response.StatusCode.ToString() + " Ok");
                var data = JsonConvert.DeserializeObject<GeocodeResponse>(response.Text);
                onReceived?.Invoke(data);
            });
        }
    }
}