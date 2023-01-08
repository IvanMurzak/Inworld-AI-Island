using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class RequestFocusOnEnable : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Selectable>()?.Select();
    }
}
