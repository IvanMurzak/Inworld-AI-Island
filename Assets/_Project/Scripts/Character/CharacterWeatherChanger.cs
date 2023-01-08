using _Project.Network;
using Inworld;
using Inworld.Sample;
using LeTai.TrueShadow;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace _Project.Character
{
    public class CharacterWeatherChanger : MonoBehaviour
    {
        const string KeywordWeather = "weather";

        #region Inspector Variables
        [SerializeField] protected WeatherVideoPlayer videoSun, videoCloud, videoRain, videoStorm;    
        [SerializeField] protected TextMeshProUGUI txtWeatherDescription;
        [SerializeField] protected InworldCharacter inworldCharacter;
        [SerializeField] protected InworldPlayer inworldPlayer;
        [SerializeField] protected TrueShadow shadow;
        [SerializeField] protected bool TestWeatherInSeattle = true;
        #endregion

        private void Awake()
        {
            HideWeatherVideos(force: true);
            SetText("");
        }

        private void OnEnable()
        {
            if (inworldCharacter)
            {
                inworldCharacter.onEndPlayerTalking += OnEndListening;
                inworldPlayer.onTextSend += OnEndListening;
            }
            if (TestWeatherInSeattle)
            {
                RequestWeather("Weather in Seattle, my favorite place");
            }
        }
        private void OnDisable()
        {
            if (inworldCharacter)
            {
                inworldCharacter.onEndPlayerTalking -= OnEndListening;
                inworldPlayer.onTextSend -= OnEndListening;
            }
        }
        void HideWeatherVideos(bool force = false)
        {
            videoSun.SetVisibility(false, force);
            videoCloud.SetVisibility(false, force);
            videoRain.SetVisibility(false, force);
            videoStorm.SetVisibility(false, force);
        }

        void SetRandomWeather() // just for testing
        {
            var types = Enum.GetValues(typeof(WeatherType)) as WeatherType[];
            var randomIndex = UnityEngine.Random.Range(0, types.Length);
            SetWeatherVideo(types[randomIndex]);
        }

        void OnEndListening(string text)
        {
            Debug.Log($"<b><color=blue>CharacterWeatherChanger.OnEndListening</color></b> {text}");

            if (text.ToLower().Contains(KeywordWeather))
            {
                RequestWeather(text);
            }
        }
        void RequestWeather(string text)
        {
            Request.Geocode(text, geoData =>
            {
                if ((geoData?.match?.Length ?? 0) > 0)
                {
                    var match = geoData.match.First();
                    Request.WeatherLocation(match.latt, match.longt, locationData =>
                    {
                        Request.WeatherForecast(locationData.properties.forecast, weatherData =>
                        {
                            if ((weatherData?.properties?.periods?.Length ?? 0) > 0)
                            {
                                var period = weatherData.properties.periods.First();
                                SetText($"{period.name}, {period.temperature}{period.temperatureUnit}, {period.shortForecast}, wind speed {period.windSpeed} in '{match.location}'");
                                var weatherType = ParseWeatherType(period.shortForecast);
                                SetWeatherVideo(weatherType);
                            }
                        });
                    });
                }
            });
        }
        void SetWeatherVideo(WeatherType weatherType, bool force = false)
        {
            Debug.Log($"<b>SetWeatherVideo</b> {weatherType}");
            videoSun.SetVisibility(weatherType == WeatherType.Sun, force);
            videoCloud.SetVisibility(weatherType == WeatherType.Cloud, force);
            videoRain.SetVisibility(weatherType == WeatherType.Rain, force);
            videoStorm.SetVisibility(weatherType == WeatherType.Storm, force);
        }
        void SetText(string text)
        {
            txtWeatherDescription.SetText(text);
            shadow.CustomHash = text.GetHashCode();
        }
        WeatherType ParseWeatherType(string text)
        {
            text = text.ToLower();

            if (text.Contains("rain")) return WeatherType.Rain;
            if (text.Contains("cloud")) return WeatherType.Cloud;
            if (text.Contains("storm")) return WeatherType.Storm;
            if (text.Contains("sun")) return WeatherType.Sun;

            return WeatherType.Sun;
        }
        enum WeatherType
        {
            Sun, Cloud, Rain, Storm
        }
    }
}