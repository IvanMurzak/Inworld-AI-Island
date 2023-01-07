using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] PopupError popupErrorRef;

    LinkedList<PopupError> popupErrorInstances = new ();

    void OnEnable() => Application.logMessageReceived += HandleException;
    void OnDisable() => Application.logMessageReceived += HandleException;

    void HandleException(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Exception)
        {
            CreateError($"Message:\n<size=20>{logString}</size>\n\nStacktrace:\n<size=16>{stackTrace}</size>");
        }
    }
    void CreateError(string text)
    {
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
