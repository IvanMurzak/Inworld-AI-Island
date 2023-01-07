using _Project.Network;
using DG.Tweening;
using Inworld;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace _Project.Character
{
    public class CharacterWeatherChanger : MonoBehaviour
    {
        const string KeywordWeather = "weather";

        #region Inspector Variables
        [SerializeField] protected VideoPlayer videoSun, videoCloud, videoRain, videoStorm;    
        [SerializeField] protected TextMeshProUGUI txtWeatherDescription;
        [SerializeField] protected InworldCharacter m_InworldCharacter;
        #endregion

        #region Private Variables
        CanvasGroup canvasSun, canvasCloud, canvasRain, canvasStorm;
        #endregion

        private void Awake()
        {
            canvasSun = videoSun.GetComponent<CanvasGroup>();
            canvasCloud = videoCloud.GetComponent<CanvasGroup>();
            canvasRain = videoRain.GetComponent<CanvasGroup>();
            canvasStorm = videoStorm.GetComponent<CanvasGroup>();

            HideWeatherVideos(force: true);
            txtWeatherDescription.SetText("");
        }

        private void OnEnable()
        {
            if (m_InworldCharacter)
            {
                m_InworldCharacter.onEndPlayerTalking += OnEndListening;
            }
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
            AnimateVideoIfNeeded(videoSun, canvasSun, false, force);
            AnimateVideoIfNeeded(videoCloud, canvasCloud, false, force);
            AnimateVideoIfNeeded(videoRain, canvasRain, false, force);
            AnimateVideoIfNeeded(videoStorm, canvasStorm, false, force);
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
                Request.Geocode(text, data =>
                {
                    SetRandomWeather();
                    txtWeatherDescription.SetText($"Random weather in '{data?.match?.FirstOrDefault()?.location}'");
                });
            }
        }
        void SetWeatherVideo(WeatherType weatherType, bool force = false)
        {
            AnimateVideoIfNeeded(videoSun, canvasSun, weatherType == WeatherType.Sun, force);
            AnimateVideoIfNeeded(videoCloud, canvasCloud, weatherType == WeatherType.Cloud, force);
            AnimateVideoIfNeeded(videoRain, canvasRain, weatherType == WeatherType.Rain, force);
            AnimateVideoIfNeeded(videoStorm, canvasStorm, weatherType == WeatherType.Storm, force);
        }
        void AnimateVideoIfNeeded(VideoPlayer video, CanvasGroup canvas, bool visible, bool force = false)
        {
            if (video.gameObject.activeSelf == visible)
                return;

            if (force)
            {
                canvas.alpha = visible ? 1 : 0;
                video.gameObject.SetActive(visible);
                return;
            }

            if (visible) video.gameObject.SetActive(true);

            DOTween.Kill(canvas.GetInstanceID());
            canvas.DOFade(visible ? 1 : 0, 0.5f)
                .SetId(canvas.GetInstanceID())
                .OnComplete(() =>
                {
                    if (!visible) video.gameObject.SetActive(false);
                })
                .OnKill(() =>
                {
                    if (!visible) video.gameObject.SetActive(false);
                });
        }

        enum WeatherType
        {
            Sun, Cloud, Rain, Storm
        }
    }
}