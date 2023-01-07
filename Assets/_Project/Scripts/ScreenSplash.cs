using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project
{
    public class ScreenSplash : MonoBehaviour
    {
        private const float OverlayOffsetPosition = 5000;

        [SerializeField] string[] scenesToLoad;
        [SerializeField] RectTransform rectTransition;
        [SerializeField] CanvasGroup canvasContent;
        [SerializeField] float fadeInDuration = 1;
        [SerializeField] float fadeOutDuration = 1;
        [SerializeField] TextMeshProUGUI txtGuideLine1;
        [SerializeField] TextMeshProUGUI txtGuideLine2;
        [SerializeField] TextMeshProUGUI txtGuideLine3;

        async UniTask Start()
        {
            // Letting Unity to initialize everything and give ability
            // to user to see smooth start animations
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

            var loadingTasks = await LoadScenes();
            OverlayFadeIn(fadeInDuration, () =>
            {
                canvasContent.gameObject.SetActive(false);

                ShowTextGuide(async () =>
                {
                    await ActivateScenes(loadingTasks);

                // Activate last loaded scene
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(scenesToLoad.Last()));

                // --------------------------- [v v v] Unloading current scene [v v v]
                OverlayFadeOut(fadeOutDuration, () => SceneManager.UnloadSceneAsync(gameObject.scene));
                });
            });
        }

        async UniTask<IEnumerable<AsyncOperation>> LoadScenes()
        {
            var loadingTasks = scenesToLoad
                .Select(x =>
                {
                    var loadTask = SceneManager.LoadSceneAsync(x, LoadSceneMode.Additive);
                    loadTask.allowSceneActivation = false;
                    return loadTask;
                })
                .ToList();

            foreach (var loadingTask in loadingTasks)
                await UniTask.WaitUntil(() => loadingTask.progress >= 0.89f);

            return loadingTasks;
        }
        async UniTask ActivateScenes(IEnumerable<AsyncOperation> loadingTasks)
        {
            foreach (var loadingTask in loadingTasks)
            {
                loadingTask.allowSceneActivation = true;
                await loadingTask;
            }
        }
        Tween OverlayFadeIn(float duration, TweenCallback onComplete = null)
        {
            Debug.Log("<b>ScreenSplash.OverlayFadeIn</b> started");
            return rectTransition.DOMoveX(0, duration)
                .From(OverlayOffsetPosition)
                .SetEase(Ease.OutSine)
                .SetRelative()
                .OnComplete(() =>
                {
                    Debug.Log("<b>ScreenSplash.OverlayFadeIn</b> completed");
                    onComplete?.Invoke();
                });
        }
        Tween OverlayFadeOut(float duration, TweenCallback onComplete = null)
        {
            Debug.Log("<b>ScreenSplash.OverlayFadeOut</b> started");
            return rectTransition.DOMoveX(-OverlayOffsetPosition, duration)
                .SetEase(Ease.InSine)
                .SetRelative()
                .OnComplete(() =>
                 {
                     Debug.Log("<b>ScreenSplash.OverlayFadeOut</b> completed");
                     onComplete?.Invoke();
                 });
        }
        Tween ShowTextGuide(TweenCallback onComplete = null)
        {
            return DOTween.Sequence()
                .Append(txtGuideLine1.DOFade(1, .5f))
                .Append(txtGuideLine2.DOFade(1, .5f).SetDelay(1))
                .Append(txtGuideLine3.DOFade(1, .5f).SetDelay(1))
                .Append(txtGuideLine1.DOFade(0, .25f).SetDelay(1.5f))
                .Append(txtGuideLine2.DOFade(0, .25f).SetDelay(0.1f))
                .Append(txtGuideLine3.DOFade(0, .25f).SetDelay(0.1f))
                .OnComplete(() => onComplete?.Invoke());
        }
    }
}