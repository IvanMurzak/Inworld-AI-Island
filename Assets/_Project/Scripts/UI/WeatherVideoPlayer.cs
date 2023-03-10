using DG.Tweening;
using LeTai.TrueShadow;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace _Project.UI
{
    public class WeatherVideoPlayer : MonoBehaviour
    {
        [SerializeField] VideoPlayer videoPlayer;
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] LayoutElement layoutElement;
        [SerializeField] TrueShadow shadow;

        Tween tween;

        void FadeAnimation(bool visible)
        {
            tween?.Kill();
            layoutElement.ignoreLayout = !visible;
            if (visible)
            {
                gameObject.SetActive(visible);
                shadow.CustomHash = Random.Range(0, 1000);
            }
            tween = canvasGroup.DOFade(visible ? 1 : 0, 0.5f)
                .OnComplete(() =>
                {
                    if (!visible) gameObject.SetActive(false);
                })
                .OnKill(() =>
                {
                    if (!visible) gameObject.SetActive(false);
                });
        }
        void FadeForce(bool visible)
        {
            canvasGroup.alpha = visible ? 1 : 0;
            layoutElement.ignoreLayout = !visible;
            gameObject.SetActive(visible);
        }

        public void SetVisibility(bool visible, bool force = false)
        {
            if (gameObject.activeSelf == visible)
                return;

            if (force) FadeForce(visible);
            else FadeAnimation(visible);
        }
    }
}