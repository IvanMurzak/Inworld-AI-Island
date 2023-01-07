using _Project.Network;
using DG.Tweening;
using Inworld;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace _Project.Character
{
    public class CharacterWeatherChanger : MonoBehaviour
    {
        const string KeywordWeather = "weather";

        #region Inspector Variables
        [SerializeField] protected WeatherVideoPlayer videoSun, videoCloud, videoRain, videoStorm;    
        [SerializeField] protected TextMeshProUGUI txtWeatherDescription;
        [SerializeField] protected InworldCharacter m_InworldCharacter;
        #endregion

        private void Awake()
        {
            HideWeatherVideos(force: true);
            txtWeatherDescription.SetText("");
        }

        private void OnEnable()
        {
            if (m_InworldCharacter)
            {
                m_InworldCharacter.onEndPlayerTalking += OnEndListening;
            }

            RequestWeather("Weather in London, my favorite place");
        }
        private void OnDisable()
        {
            if (m_InworldCharacter)
            {
                m_InworldCharacter.onEndPlayerTalking -= OnEndListening;
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
            Request.Geocode(text, data =>
            {
                if ((data?.match?.Length ?? 0) > 0)
                {
                    SetRandomWeather();
                    txtWeatherDescription.SetText($"Random weather in '{data.match.First().location}'");
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

        enum WeatherType
        {
            Sun, Cloud, Rain, Storm
        }
    }
}