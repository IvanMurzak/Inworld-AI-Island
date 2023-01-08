using DG.Tweening;
using LeTai.TrueShadow;
using TMPro;
using UnityEngine;

namespace _Project.UI
{
    public class PopupError : MonoBehaviour
    {
        [SerializeField] float duration;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] TrueShadow shadow;

        Tween tween;
        float startYPosition;

        public float Duration => duration;

        void Awake()
        {
            startYPosition = transform.localPosition.y;
            canvasGroup.alpha = 0;
        }

        public void SetText(string text)
        {
            this.text.SetText(text);
            shadow.CustomHash = text.GetHashCode();
        }
        public void FadeIn(TweenCallback onComplete = null)
        {
            tween?.Kill();
            tween = DOTween.Sequence()
                .Join(canvasGroup.DOFade(1, 0.5f))
                .Join(transform.DOLocalMoveY(startYPosition, 0.5f)
                    .From(startYPosition + 100)
                    .SetEase(Ease.OutSine));

            if (onComplete != null)
            {
                tween = tween.OnComplete(onComplete);
            }
        }
        public void FadeOut(TweenCallback onComplete = null)
        {
            tween?.Kill();
            tween = DOTween.Sequence()
                .Join(canvasGroup.DOFade(0, 0.5f))
                .Join(transform.DOLocalMoveY(startYPosition + 100, 0.5f)
                    .SetEase(Ease.InQuad));

            if (onComplete != null)
            {
                tween = tween.OnComplete(onComplete);
            }
        }
    }
}