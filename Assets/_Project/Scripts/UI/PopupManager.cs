using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace _Project.UI
{
    public class PopupManager : MonoBehaviour
    {
        public static PopupManager Instance { get; private set; }

        [SerializeField] PopupError popupErrorRef;

        LinkedList<PopupError> popupErrorInstances = new();

        void Awake()
        {
            if (Instance != null)
            {
                throw new Exception("Second isntace of PopupManager should not exist");
            }
            Instance = this;
        }
        void OnEnable() => Application.logMessageReceived += HandleException;
        void OnDisable() => Application.logMessageReceived += HandleException;
        void OnDestroy() => Instance = null;
        void HandleException(string logString, string stackTrace, LogType type)
        {
            if (type == LogType.Exception)
            {
                CreateError($"Message:\n<size=20>{logString}</size>\n\nStacktrace:\n<size=16>{stackTrace}</size>");
            }
        }
        public void CreateError(string text)
        {
            Debug.LogWarning($"CreateError popup: {text}");

            var instance = GameObject.Instantiate(popupErrorRef, transform).GetComponent<PopupError>();
            popupErrorInstances.AddLast(instance);
            instance.SetText(text);
            instance.gameObject.SetActive(true);
            instance.FadeIn();

            Observable.Timer(TimeSpan.FromSeconds(instance.Duration)).First()
                .Subscribe(x => RemoveError(instance))
                .AddTo(instance)
                .AddTo(this);
        }
        void RemoveError(PopupError instance)
        {
            instance.FadeOut(() => Destroy(instance.gameObject));
            popupErrorInstances.Remove(instance);
        }
    }
}