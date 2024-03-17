using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.Object;
using System;
using System.IO;
using Newtonsoft.Json;

public class ReadJson : MonoBehaviour
{
    // Variable inicial del JSON
    public Steps s;
    public int numCarAgents;
    public int numLights = 4;
    public int width;
    public int height;
    public int numGenerations;
    //public List<List<int>> carMovements;
    public List<List<string>> trafficLightsColors = new List<List<string>>();
    public List<List<int>> carsDX = new List<List<int>>();
    public List<List<int>> carsDY = new List<List<int>>();
    public List<List<int>> LapsList = new List<List<int>>();
    public List<List<Turns>> TurnList = new List<List<Turns>>();
    public TextAsset jsonFile;

    // Inicio
    void Awake()
    {
        s = LoadJson();

        // * Acceder a datos del modelo *

        // Numero de carros
        numCarAgents = s.modelData.carAgents;

        // Ancho y alto de grid
        width = s.modelData.width;

        height = s.modelData.height;

        // Numero de generaciones
        numGenerations = s.modelData.numGenerations;

        // Creación de listas de cambios de color de semáforos
        for (int i = 0; i < numLights; i++)
        {
            List<string> semColors = new List<string>();
            for (int k = 0; k < s.steps.Count; k++)
            {
                semColors.Add(s.steps[k].traficLights[i].colour);
            }
            trafficLightsColors.Add(semColors);
        }

        // Creación de listas de datos de carros
        for (int i = 0; i < numCarAgents; i++)
        {
            List<int> dx = new List<int>();
            List<int> dy = new List<int>();
            List<int> laps = new List<int>();
            List<Turns> turns = new List<Turns>();
            int dxAux = s.steps[0].cars[i].dx;
            int dyAux = s.steps[0].cars[i].dy;
            string t;
            for (int k = 0; k < s.steps.Count; k++)
            {
                dx.Add(s.steps[k].cars[i].dx);
                dy.Add(s.steps[k].cars[i].dy);
                laps.Add(s.steps[k].cars[i].laps);

                // Detectar vueltas y guardar datos en lista de clase "Turns"
                if ((dxAux != s.steps[k].cars[i].dx) || (dyAux != s.steps[k].cars[i].dy))
                {

                    if (s.steps[k].cars[i].dx == 1 && s.steps[k].cars[i].dy == 0)
                    {
                        t = "s1";
                        Turns T = new Turns();
                        T.sec = t;
                        T.st = k;
                        T.Lap = s.steps[k].cars[i].laps;
                        turns.Add(T);
                        dxAux = 1;
                        dyAux = 0;
                    }

                    else if (s.steps[k].cars[i].dx == 0 && s.steps[k].cars[i].dy == -1)
                    {
                        t = "s2";
                        Turns T = new Turns();
                        T.sec = t;
                        T.st = k;
                        T.Lap = s.steps[k].cars[i].laps;
                        turns.Add(T);
                        dxAux = 0;
                        dyAux = -1;
                    }

                    else if (s.steps[k].cars[i].dx == 0 && s.steps[k].cars[i].dy == 1)
                    {
                        t = "s3";
                        Turns T = new Turns();
                        T.sec = t;
                        T.st = k;
                        T.Lap = s.steps[k].cars[i].laps;
                        turns.Add(T);
                        dxAux = 0;
                        dyAux = 1;
                    }

                    else if (s.steps[k].cars[i].dx == -1 && s.steps[k].cars[i].dy == 0)
                    {
                        t = "s4";
                        Turns T = new Turns();
                        T.sec = t;
                        T.st = k;
                        T.Lap = s.steps[k].cars[i].laps;
                        turns.Add(T);
                        dxAux = -1;
                        dyAux = 0;
                    }
                }
            }
            carsDX.Add(dx); // Diferenciales
            carsDY.Add(dy);
            LapsList.Add(laps); // Cantidad de vueltas
            TurnList.Add(turns); // Vueltas en total de la ejecución
        }

    }

    // Método para leer el JSON de forma LOCAL (Carpeta 'JSONs' en la carpeta 'Assets' [SE DEBE CREAR MANUALMENTE])
    public Steps LoadJson()
    {
        if (jsonFile != null)
        {
            string json = jsonFile.text;
            Steps file = JsonConvert.DeserializeObject<Steps>(json);
            return file;
        }
        else
        {
            print("No se ha asignado ningún archivo JSON.");
            return null;
        }
    }
}

// Clases para leer el JSON
[System.Serializable]
public class TrafficLights
{
    public string colour;
    public int id;
};

[System.Serializable]
public class Cars
{
    public int dx;
    public int dy;
    public int id;
    public int laps;
};

[System.Serializable]
public class Step
{
    public List<Cars> cars;
    public List<TrafficLights> traficLights;
};

[System.Serializable]
public class ModelData
{
    public int width;
    public int height;
    public int carAgents;
    public int numGenerations;
};

[System.Serializable]
public class Steps
{
    public ModelData modelData;
    public List<Step> steps;
};