using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI
{
    [RequireComponent(typeof(Selectable))]
    public class RequestFocusOnEnable : MonoBehaviour
    {
        void OnEnable()
        {
            GetComponent<Selectable>()?.Select();
        }
    }
}