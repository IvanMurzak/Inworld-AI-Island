using Cinemachine;
using UnityEngine;

public class CameraPositionChanger : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam1, cam2, cam3;

    private void Start()
    {
        DeactivateAll();
        cam3.gameObject.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            DeactivateAll();
            cam1.gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            DeactivateAll();
            cam2.gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            DeactivateAll();
            cam3.gameObject.SetActive(true);
        }
    }
    void DeactivateAll()
    {
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(false);
        cam3.gameObject.SetActive(false);
    }
}
