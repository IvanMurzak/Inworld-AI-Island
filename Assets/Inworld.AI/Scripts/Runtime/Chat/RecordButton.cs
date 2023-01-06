/*************************************************************************************************
* Copyright 2022 Theai, Inc. (DBA Inworld)
*
* Use of this source code is governed by the Inworld.ai Software Development Kit License Agreement
* that can be found in the LICENSE.md file or at https://www.inworld.ai/sdk-license
*************************************************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;
namespace Inworld.Sample.UI
{
    /// <summary>
    ///     This class is used for the Record Button in the global chat panel.
    /// </summary>
    public class RecordButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsRecording { get; private set; }
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("<b>RecordButton.OnPointerDown</b>");
            IsRecording = true;
            InworldController.Instance.StartAudioCapture(InworldController.Instance.CurrentCharacter.ID);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("<b>RecordButton.OnPointerUp</b>");
            IsRecording = false;
            InworldController.Instance.EndAudioCapture(InworldController.Instance.CurrentCharacter.ID);
        }
    }
}
