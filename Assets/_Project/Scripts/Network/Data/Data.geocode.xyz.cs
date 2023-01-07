using System;

namespace _Project.Network.Data
{
    [Serializable]
    public class GeocodeResponse
    {
        public string longt { get; set; }
        public string matches { get; set; }
        public Match[] match { get; set; }
        public string latt { get; set; }
    }

    [Serializable]
    public class Match
    {
        public string longt { get; set; }
        public string location { get; set; }
        public string matchtype { get; set; }
        public string confidence { get; set; }
        public string MentionIndices { get; set; }
        public string latt { get; set; }
    }
}
