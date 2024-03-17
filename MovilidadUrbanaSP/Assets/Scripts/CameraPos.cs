using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraPos : MonoBehaviour
{
    public Camera C1;
    public Camera C2;
    public Camera C3; // Nueva cámara 3
    public Camera C4; // Nueva cámara 4
    public TextMeshProUGUI NoIteraciones;
    public int pasos;

    // Start is called before the first frame update
    void Start()
    {
        C1.enabled = true;
        C2.enabled = false;
        C3.enabled = false; // Inicialmente desactivada
        C4.enabled = false; // Inicialmente desactivada
        pasos = GameObject.Find("Sem1Control").GetComponent<LightController>().numStep;
    }

    // Update is called once per frame
    void Update()
    {
        pasos = GameObject.Find("Sem1Control").GetComponent<LightController>().numStep + 1;
        NoIteraciones.text = "Iteraciones: " + pasos;
    }

    public void selectCam1()
    {
        C1.enabled = true;
        C2.enabled = false;
        C3.enabled = false;
        C4.enabled = false;
    }

    public void selectCam2()
    {
        C1.enabled = false;
        C2.enabled = true;
        C3.enabled = false;
        C4.enabled = false;
    }

    public void selectCam3()
    {
        C1.enabled = false;
        C2.enabled = false;
        C3.enabled = true;
        C4.enabled = false;
    }

    public void selectCam4()
    {
        C1.enabled = false;
        C2.enabled = false;
        C3.enabled = false;
        C4.enabled = true;
    }
}
