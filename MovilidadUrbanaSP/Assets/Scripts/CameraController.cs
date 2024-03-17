using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<Camera> cameraList = new List<Camera>();
    public GameObject cam1prefab;

    void Start()
    {
        if (cameraList == null)
        {
            UnityEngine.Debug.Log("Agregar camaras");
        }
        else
        {
            cameraControl();
        }
    }

    void cameraControl()
    {
        if (cameraList[0].enabled)
        {
            cam1prefab.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
