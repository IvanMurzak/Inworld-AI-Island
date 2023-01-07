using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Project.Network.Data
{
    public class GeoJSON
    {
        public object[] context { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public float[] coordinates { get; set; }
    }

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
        public Relativelocation relativeLocation { get; set; }
        public string forecastZone { get; set; }
        public string county { get; set; }
        public string fireWeatherZone { get; set; }
        public string timeZone { get; set; }
        public string radarStation { get; set; }
    }

    public class Relativelocation
    {
        public string type { get; set; }
        public Geometry1 geometry { get; set; }
        public Properties1 properties { get; set; }
    }

    public class Geometry1
    {
        public string type { get; set; }
        public float[] coordinates { get; set; }
    }

    public class Properties1
    {
        public string city { get; set; }
        public string state { get; set; }
        public Distance distance { get; set; }
        public Bearing bearing { get; set; }
    }

    public class Distance
    {
        public string unitCode { get; set; }
        public float value { get; set; }
    }

    public class Bearing
    {
        public string unitCode { get; set; }
        public int value { get; set; }
    }

}
