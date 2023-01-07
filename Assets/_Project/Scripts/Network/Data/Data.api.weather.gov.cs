using System;

namespace _Project.Network.Data
{
    [Serializable]
    public class ApiWeatherLocationResponse
    {
        public string id { get; set; }
        public string type { get; set; }
        public Properties properties { get; set; }
    }

    [Serializable]
    public class Properties
    {
        public string id { get; set; }
        public string type { get; set; }
        public string cwa { get; set; }
        public string forecastOffice { get; set; }
        public string gridId { get; set; }
        public int gridX { get; set; }
        public int gridY { get; set; }
        public string forecast { get; set; }
        public string forecastHourly { get; set; }
        public string forecastGridData { get; set; }
        public string observationStations { get; set; }
        public RelativeLocation relativeLocation { get; set; }
        public string forecastZone { get; set; }
        public string county { get; set; }
        public string fireWeatherZone { get; set; }
        public string timeZone { get; set; }
        public string radarStation { get; set; }
    }

    [Serializable]
    public class RelativeLocation
    {
        public string type { get; set; }
        public Properties1 properties { get; set; }
    }

    [Serializable]
    public class Properties1
    {
        public string city { get; set; }
        public string state { get; set; }
        public Unit distance { get; set; }
        public Unit bearing { get; set; }
    }

    [Serializable]
    public class Unit
    {
        public string unitCode { get; set; }
        public float value { get; set; }
    }

    [Serializable]
    public class Period
    {
        public int number { get; set; }
        public string name { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public bool isDaytime { get; set; }
        public int temperature { get; set; }
        public string temperatureUnit { get; set; }
        public string temperatureTrend { get; set; }
        public string windSpeed { get; set; }
        public string windDirection { get; set; }
        public string icon { get; set; }
        public string shortForecast { get; set; }
        public string detailedForecast { get; set; }
    }

    // ---------------------------------------------

    [Serializable]
    public class ApiWeatherForecastResponse
    {
        public string type { get; set; }
        public ForecaseProperty properties { get; set; }
    }

    [Serializable]
    public class ForecaseProperty
    {
        public string units { get; set; }
        public string forecastGenerator { get; set; }
        public Unit elevation { get; set; }
        public Period[] periods { get; set; }
    }
}
