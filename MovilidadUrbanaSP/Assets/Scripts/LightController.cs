using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject greenLight1;
    public GameObject yellowLight1;
    public GameObject redLight1;
    public GameObject greenLight2;
    public GameObject yellowLight2;
    public GameObject redLight2;
    public List<List<string>> TLColors = new List<List<string>>();
    public Steps stepsAux;
    public int TLid;
    public int indexID, numCars, numStep;
    public string status;
    public bool end = false;
    public float delay, delayAux;
    public float delayAdj;
    // Start is called before the first frame update
    void Start()
    {
        TLColors = GameObject.Find("JsonREADER").GetComponent<ReadJson>().trafficLightsColors;
        numCars = GameObject.Find("JsonREADER").GetComponent<ReadJson>().numCarAgents;
        TLid += numCars;
        //stepsAux = GameObject.Find("JsonREADER").GetComponent<ReadJson>().s;
        if (TLid == numCars)
        {
            indexID = 0;
            delayAdj = GameObject.Find("JsonREADER").GetComponent<ReadJson>().height;
            delay = (9.84615f * 0.5f) / delayAdj;
            //delay = (0.5f * 5f) / 8;
        }
        else if (TLid == numCars + 1)
        {
            indexID = 1;
            delayAdj = GameObject.Find("JsonREADER").GetComponent<ReadJson>().width;
            delay = (9.84615f * 0.5f) / delayAdj;
            //delay = (0.5f * 5f) / 8;
        }
        else if (TLid == numCars + 2)
        {
            indexID = 2;
            delayAdj = GameObject.Find("JsonREADER").GetComponent<ReadJson>().width;
            delay = (9.84615f * 0.5f) / delayAdj;
            //delay = (0.5f * 5f) / 8;
        }
        else if (TLid == numCars + 3)
        {
            indexID = 3;
            delayAdj = GameObject.Find("JsonREADER").GetComponent<ReadJson>().height;
            delay = (9.84615f * 0.5f) / delayAdj;
            //delay = (0.5f * 5f) / 8;
        }
        StartCoroutine(changeLights());
        //yellowLight1.SetActive(false);
        //yellowLight2.SetActive(false);
    }

    IEnumerator changeLights()
    {
        for (int i = 0; i < TLColors[0].Count; i++)
        {
            //UnityEngine.Debug.Log(TLColors[indexID][i]);
            //UnityEngine.Debug.Log(TLid);
            //StartCoroutine(loopDelay());
            status = TLColors[indexID][i];
            delayAux = delay;
            numStep = i;
            //UnityEngine.Debug.Log(i);
            if (status == "green")
            {
                turnOnGreenLight();
                //delayAux = delay / 4;
            }

            else if (status == "yellow")
            {
                turnOnYellowLight();
            }

            else if (status == "red")
            {
                turnOnRedLight();
                //delayAux = delay / 4;
            }

            if (i + 1 == TLColors[0].Count)
            {
                end = true;
            }
            yield return new WaitForSeconds(delay);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        


        /*if (Input.GetKeyDown(KeyCode.G))
        {
            turnOnGreenLight();
        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            turnOnRedLight();
        }

        else if (Input.GetKeyDown(KeyCode.Y))
        {
            turnOnYellowLight();
        }*/
    }

    public void turnOnGreenLight()
    {
        greenLight1.SetActive(false);
        greenLight2.SetActive(false);
        yellowLight1.SetActive(true);
        yellowLight2.SetActive(true);
        redLight1.SetActive(true);
        redLight2.SetActive(true);
    }

    public void turnOnRedLight()
    {
        greenLight1.SetActive(true);
        greenLight2.SetActive(true);
        yellowLight1.SetActive(true);
        yellowLight2.SetActive(true);
        redLight1.SetActive(false);
        redLight2.SetActive(false);
    }

    public void turnOnYellowLight()
    {
        greenLight1.SetActive(true);
        greenLight2.SetActive(true);
        yellowLight1.SetActive(false);
        yellowLight2.SetActive(false);
        redLight1.SetActive(true);
        redLight2.SetActive(true);
    }
}
