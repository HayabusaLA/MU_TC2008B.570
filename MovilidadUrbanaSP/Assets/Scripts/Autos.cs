using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class Autos : MonoBehaviour
{
    // Atributos del agente
    public float speed, speedAux, offset;
    public Vector3 startPos;
    public Vector3 endPos;
    public int id, index, dxAux, dyAux, posInList;
    public int sector, lengthTrack;
    public int linePos, carsInSector, carLap;
    public string lightS, rule;
    public bool endLight, turnPrivilege;

    // Listas heredadas del ReadJson
    public List<int> dx = new List<int>();
    public List<int> dy = new List<int>();
    public List<int> laps = new List<int>();
    public List<Turns> turnList = new List<Turns>();
    public List<GameObject> carsInLine = new List<GameObject>();

    void Start()
    {
        index = 0;  // Índice de búsqueda de datos por vueltas
        carLap = 0; // Vueltas del carro
        turnPrivilege = false; // Privilegio en estado "off" como base

        // Establecer datos iniciales según el sector del automóvil
        if (sector == 1)
        {
            lightS = "Sem1Control";
            rule = GameObject.Find("Sem1Control").GetComponent<LightController>().status;
            speed = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().C1velocity;
            carsInSector = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().S1Coches;
            dxAux = 1;
            dyAux = 0;
            lengthTrack = GameObject.Find("JsonREADER").GetComponent<ReadJson>().height;
            carsInLine = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().carS1;
            posInList = carsInLine.IndexOf(gameObject);
            offset = 5f * (carsInLine.Count - 1 - posInList);
        }

        else if (sector == 2)
        {
            lightS = "Sem2Control";
            rule = GameObject.Find("Sem2Control").GetComponent<LightController>().status;
            speed = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().C2velocity;
            carsInSector = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().S2Coches;
            dxAux = 0;
            dyAux = -1;
            lengthTrack = GameObject.Find("JsonREADER").GetComponent<ReadJson>().width;
            carsInLine = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().carS2;
            posInList = carsInLine.IndexOf(gameObject);
            offset = 5f * (carsInLine.Count - 1 - posInList);
        }

        else if (sector == 3)
        {
            lightS = "Sem3Control";
            rule = GameObject.Find("Sem3Control").GetComponent<LightController>().status;
            speed = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().C3velocity;
            carsInSector = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().S3Coches;
            dxAux = 0;
            dyAux = 1;
            lengthTrack = GameObject.Find("JsonREADER").GetComponent<ReadJson>().width;
            carsInLine = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().carS3;
            posInList = carsInLine.IndexOf(gameObject);
            offset = 5f * (carsInLine.Count - 1 - posInList);
        }

        else if (sector == 4)
        {
            lightS = "Sem4Control";
            rule = GameObject.Find("Sem4Control").GetComponent<LightController>().status;
            speed = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().C4velocity;
            carsInSector = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().S4Coches;
            dxAux = -1;
            dyAux = 0;
            lengthTrack = GameObject.Find("JsonREADER").GetComponent<ReadJson>().height;
            carsInLine = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().carS4;
            posInList = carsInLine.IndexOf(gameObject);
            offset = 5f * (carsInLine.Count - 1 - posInList);
        }
    }

    void Update()
    {   
        // Seguir actualizando datos de movimiento
        rule = GameObject.Find(lightS).GetComponent<LightController>().status;   // Estado del semáforo
        endLight = GameObject.Find(lightS).GetComponent<LightController>().end;  // Fin de la ejecución (se acaban los steps o generations)
        posInList = carsInLine.IndexOf(gameObject); // Posición en lista de carros por sector
        offset = 5f * (carsInLine.Count - 1 - posInList);   // Offset para mantener orden durante luces rojas

        if ((turnList.Count > 0) && (index + 1 <= turnList.Count))
        {
            if (carLap == turnList[index].Lap)  // Verificar si en la vuelta actual el carro cambia de sector
            {
                if (turnList[index].sec == "s1")
                {
                    if ((transform.position.x >= 1f && transform.position.z == -1.5f) || (transform.position.x <= 1f && transform.position.z == 1f)) {
                        turnS1();
                        index++;
                    }
                }

                else if (turnList[index].sec == "s2")
                {
                    if ((transform.position.x == 1f && transform.position.z >= -1.5f) || (transform.position.x == -1.5f && transform.position.z <= -1.5f)) {
                        turnS2();
                        index++;
                    }
                }

                else if (turnList[index].sec == "s3")
                {
                    if ((transform.position.x == 1f && transform.position.z >= 1f) || (transform.position.x == -1.5f && transform.position.z <= 1f)) {
                        turnS3();
                        index++;
                    }
                }

                else if (turnList[index].sec == "s4")
                {
                    if ((transform.position.x >= -1.6f && transform.position.z == -1.5f) || (transform.position.x <= -1.5f && transform.position.z == 1f)) {
                        turnS4();
                        index++;
                    }
                }

            }

        }

        if ((rule == "green" || rule == "yellow" || turnPrivilege) && !endLight)
        {
            speedAux = speed;
        }

        else if (endLight == true)
        {
            speedAux = 0f;
        }

        else if (rule == "red")
        {
            for (int i = 0; i < carsInLine.Count; i++)
            {
                if (carsInLine[i].GetComponent<Autos>().turnPrivilege)
                {
                    offset -= 5f;
                } 
            }
            if (sector == 1)
            {
                if (transform.position.z <= (-4.6 - offset) && transform.position.z >= (-5.5 - offset))
                {
                    speedAux = 0f;
                }

                else
                {
                    speedAux = speed;
                }
            }

            else if (sector == 2)
            {
                if (transform.position.x <= (-4.6 - offset) && transform.position.x >= (-5.5 - offset))
                {
                    speedAux = 0f;
                }

                else
                {
                    speedAux = speed;
                }
            }

            else if (sector == 3)
            {
                if (transform.position.x >= (4.6 + offset) && transform.position.x <= (5.5 + offset))
                {
                    speedAux = 0f;
                }

                else
                {
                    speedAux = speed;
                }
            }

            else if (sector == 4)
            {
                if (transform.position.z >= (4.6 + offset) && transform.position.z <= (5.5 + offset))
                {
                    speedAux = 0f;
                }

                else
                {
                    speedAux = speed;
                }
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, endPos, speedAux * Time.deltaTime);

        if (transform.position == endPos)
        {
            transform.position = startPos;
            carLap++;
            turnPrivilege = false;
        }
    }

    void turnS1()
    {
        // Cambiar posiciones de movimiento
        startPos = new Vector3(1f, 0f, -50f);
        endPos = new Vector3(1f, 0f, 50f);

        // Eliminar carro de la lista del sector anterior
        carsInLine.RemoveAt(posInList);

        // Asignar nuevo semáforo a seguir
        lightS = "Sem1Control";
        rule = GameObject.Find(lightS).GetComponent<LightController>().status;
        turnPrivilege = true; // Privilegio de seguir aunque esté en rojo

        // Encontrar nueva lista y añadir el agente
        carsInLine = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().carS1;

        int newPos = 0;
        for (int i = 0; i < carsInLine.Count; i++)
        {
            if (transform.position.z > carsInLine[i].transform.position.z)
            {
                newPos++;
            }
        }
        carsInLine.Insert(newPos, gameObject);
        posInList = carsInLine.IndexOf(gameObject);
        offset = 5f * (carsInLine.Count - 1 - posInList);

        // Rotar carro hacia su dirección
        Vector3 pos = endPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos, Vector3.up);
        transform.rotation = rotation;
        
        // Asignar velocidad
        speed = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().C1velocity;

        carsInSector = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().S1Coches;

        // Variables de diferencial de sector
        dxAux = 1;
        dyAux = 0;

        lengthTrack = GameObject.Find("JsonREADER").GetComponent<ReadJson>().height;
        sector = 1;
    }

    void turnS2()
    {
        // Cambiar posiciones de movimiento
        startPos = new Vector3(-50f, 0f, -1.5f);
        endPos = new Vector3(50f, 0f, -1.5f);

        // Eliminar carro de la lista del sector anterior
        carsInLine.RemoveAt(posInList);

        // Asignar nuevo semáforo a seguir
        lightS = "Sem2Control";
        rule = GameObject.Find(lightS).GetComponent<LightController>().status;
        turnPrivilege = true; // Privilegio de seguir aunque esté en rojo

        // Encontrar nueva lista y añadir el agente
        carsInLine = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().carS2;

        int newPos = 0;
        for (int i = 0; i < carsInLine.Count; i++)
        {
            if (transform.position.x > carsInLine[i].transform.position.x)
            {
                newPos++;
            }
        }
        carsInLine.Insert(newPos, gameObject);
        posInList = carsInLine.IndexOf(gameObject);
        offset = 5f * (carsInLine.Count - 1 - posInList);

        // Rotar carro hacia su dirección
        Vector3 pos = endPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos, Vector3.up);
        transform.rotation = rotation;

        // Asignar velocidad
        speed = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().C2velocity;

        carsInSector = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().S2Coches;

        // Variables de diferencial de sector
        dxAux = 0;
        dyAux = -1;

        lengthTrack = GameObject.Find("JsonREADER").GetComponent<ReadJson>().width;
        sector = 2;
    }

    void turnS3()
    {
        // Cambiar posiciones de movimiento
        startPos = new Vector3(50f, 0f, 1f);
        endPos = new Vector3(-50f, 0f, 1f);

        // Eliminar carro de la lista del sector anterior
        carsInLine.RemoveAt(posInList);

        // Asignar nuevo semáforo a seguir
        lightS = "Sem3Control";
        rule = GameObject.Find(lightS).GetComponent<LightController>().status;
        turnPrivilege = true; // Privilegio de seguir aunque esté en rojo

        // Encontrar nueva lista y añadir el agente
        carsInLine = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().carS3;

        int newPos = 0;
        for (int i = 0; i < carsInLine.Count; i++)
        {
            if (transform.position.x < carsInLine[i].transform.position.x)
            {
                newPos++;
            }
        }
        carsInLine.Insert(newPos, gameObject);
        posInList = carsInLine.IndexOf(gameObject);
        offset = 5f * (carsInLine.Count - 1 - posInList);

        // Rotar carro hacia su dirección
        Vector3 pos = endPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos, Vector3.up);
        transform.rotation = rotation;

        // Asignar velocidad
        speed = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().C3velocity;

        carsInSector = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().S3Coches;

        // Variables de diferencial de sector
        dxAux = 0;
        dyAux = 1;

        lengthTrack = GameObject.Find("JsonREADER").GetComponent<ReadJson>().width;
        sector = 3;
    }

    void turnS4()
    {
        // Cambiar posiciones de movimiento
        startPos = new Vector3(-1.5f, 0f, 50f);
        endPos = new Vector3(-1.5f, 0f, -50f);

        // Eliminar carro de la lista del sector anterior
        carsInLine.RemoveAt(posInList);

        // Asignar nuevo semáforo a seguir
        lightS = "Sem4Control";
        rule = GameObject.Find(lightS).GetComponent<LightController>().status;
        turnPrivilege = true; // Privilegio de seguir aunque esté en rojo

        // Encontrar nueva lista y añadir el agente
        carsInLine = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().carS4;

        int newPos = 0;
        for (int i = 0; i < carsInLine.Count; i++)
        {
            if (transform.position.z < carsInLine[i].transform.position.z)
            {
                newPos++;
            }
        }
        carsInLine.Insert(newPos, gameObject);
        posInList = carsInLine.IndexOf(gameObject);
        offset = 5f * (carsInLine.Count - 1 - posInList);

        // Rotar carro hacia su dirección
        Vector3 pos = endPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos, Vector3.up);
        transform.rotation = rotation;

        // Asignar velocidad
        speed = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().C4velocity;

        carsInSector = GameObject.Find("GeneradorCoches").GetComponent<GeneradorCoches>().S4Coches;

        // Variables de diferencial de sector
        dxAux = 0;
        dyAux = -1;

        lengthTrack = GameObject.Find("JsonREADER").GetComponent<ReadJson>().height;
        sector = 4;
    }
}

[System.Serializable]
public class Turns
{
    public string sec;
    public int st;
    public int Lap;
};
