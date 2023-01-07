using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace _Project
{
    public class CameraPositionChanger : MonoBehaviour
    {
        [SerializeField] float startDelay = 1;
        [SerializeField] CinemachineVirtualCamera cam1, cam2, cam3;

        async UniTask Start()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(startDelay));
            DeactivateAll();
            cam3.gameObject.SetActive(true);
        }
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
                Application.Quit();

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
}