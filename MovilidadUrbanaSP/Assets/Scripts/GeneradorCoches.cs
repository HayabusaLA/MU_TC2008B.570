using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorCoches : MonoBehaviour
{
    public List<GameObject> carPrefabs;
    public int ContadorApariciones;
    public float C1velocity, C2velocity, C3velocity, C4velocity;
    private float spawnOffset = 0f;
    public Steps st;
    public int S1Coches, S2Coches, S3Coches, S4Coches;
    public List<List<int>> cars_dx = new List<List<int>>();
    public List<List<int>> cars_dy = new List<List<int>>();
    public List<List<int>> cars_laps = new List<List<int>>();
    public List<GameObject> carS1 = new List<GameObject>();
    public List<GameObject> carS2 = new List<GameObject>();
    public List<GameObject> carS3 = new List<GameObject>();
    public List<GameObject> carS4 = new List<GameObject>();
    public List<List<Turns>> TurnsList = new List<List<Turns>>();

    // Start is called before the first frame update
    void Start()
    {
        cars_dx = GameObject.Find("JsonREADER").GetComponent<ReadJson>().carsDX;
        cars_dy = GameObject.Find("JsonREADER").GetComponent<ReadJson>().carsDY;
        cars_laps = GameObject.Find("JsonREADER").GetComponent<ReadJson>().LapsList;
        ContadorApariciones = GameObject.Find("JsonREADER").GetComponent<ReadJson>().numCarAgents;
        TurnsList = GameObject.Find("JsonREADER").GetComponent<ReadJson>().TurnList;
        st = GameObject.Find("JsonREADER").GetComponent<ReadJson>().s;
        if (carPrefabs.Count != 0)
        {
            SpawnCars();
        }
        else
        {
            Debug.LogError("No esta asignado el prefab");
        }

    }

    void SpawnCars()
    {
        float spawnOffsetAux1 = spawnOffset;
        float spawnOffsetAux2 = spawnOffset;
        float spawnOffsetAux3 = spawnOffset;
        float spawnOffsetAux4 = spawnOffset;
        int pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;

        for (int i = 0; i < ContadorApariciones; i++)
        {
            var random = new System.Random();
            int prefabIndex = random.Next(carPrefabs.Count);
            if (st.steps[0].cars[i].dx == 1)
            {
                float zNewPos = -50f + spawnOffsetAux1;
                Vector3 newPos = new Vector3(1f, 0.13f, zNewPos);

                GameObject newCar = Instantiate(carPrefabs[prefabIndex], newPos, Quaternion.identity);

                newCar.transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);

                newCar.transform.Rotate(0f, 0f, 0f);
                newCar.AddComponent<Autos>();
                newCar.GetComponent<Autos>().speed = C1velocity;
                newCar.GetComponent<Autos>().startPos = new Vector3(newPos.x, newPos.y, (newPos.z - spawnOffsetAux1));
                newCar.GetComponent<Autos>().endPos = new Vector3(newPos.x, newPos.y, -(newPos.z - spawnOffsetAux1));
                newCar.GetComponent<Autos>().id = st.steps[0].cars[i].id;
                newCar.GetComponent<Autos>().sector = 1;
                newCar.GetComponent<Autos>().dx = cars_dx[i];
                newCar.GetComponent<Autos>().dy = cars_dy[i];
                newCar.GetComponent<Autos>().laps = cars_laps[i];
                pos1++;
                newCar.GetComponent<Autos>().linePos = pos1;
                newCar.GetComponent<Autos>().turnList = TurnsList[i];
                carS1.Add(newCar);
                S1Coches = pos1;
                spawnOffsetAux1 += 5f;
            }

            else if (st.steps[0].cars[i].dy == 1)
            {
                float xNewPos = 50f + spawnOffsetAux3;
                Vector3 newPos = new Vector3(xNewPos, 0.13f, 1f);

                GameObject newCar = Instantiate(carPrefabs[prefabIndex], newPos, Quaternion.identity);

                newCar.transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);

                newCar.transform.Rotate(0f, -90f, 0f);
                newCar.AddComponent<Autos>();
                newCar.GetComponent<Autos>().speed = C3velocity;
                newCar.GetComponent<Autos>().startPos = new Vector3((newPos.x - spawnOffsetAux3), newPos.y, newPos.z); ;
                newCar.GetComponent<Autos>().endPos = new Vector3(-(newPos.x - spawnOffsetAux3), newPos.y, newPos.z);
                newCar.GetComponent<Autos>().id = st.steps[0].cars[i].id;
                newCar.GetComponent<Autos>().sector = 3;
                newCar.GetComponent<Autos>().dx = cars_dx[i];
                newCar.GetComponent<Autos>().dy = cars_dy[i];
                newCar.GetComponent<Autos>().laps = cars_laps[i];
                pos3++;
                newCar.GetComponent<Autos>().linePos = pos3;
                newCar.GetComponent<Autos>().turnList = TurnsList[i];
                carS3.Add(newCar);
                S3Coches = pos3;
                spawnOffsetAux3 -= 5f;
            }

            else if (st.steps[0].cars[i].dy == -1)
            {
                float xNewPos = -50f + spawnOffsetAux2;
                Vector3 newPos = new Vector3(xNewPos, 0.13f, -1.5f);

                GameObject newCar = Instantiate(carPrefabs[prefabIndex], newPos, Quaternion.identity);

                newCar.transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);

                newCar.transform.Rotate(0f, 90f, 0f);
                newCar.AddComponent<Autos>();
                newCar.GetComponent<Autos>().speed = C2velocity;
                newCar.GetComponent<Autos>().startPos = new Vector3((newPos.x - spawnOffsetAux2), newPos.y, newPos.z); ;
                newCar.GetComponent<Autos>().endPos = new Vector3(-(newPos.x - spawnOffsetAux2), newPos.y, newPos.z);
                newCar.GetComponent<Autos>().id = st.steps[0].cars[i].id;
                newCar.GetComponent<Autos>().sector = 2;
                newCar.GetComponent<Autos>().dx = cars_dx[i];
                newCar.GetComponent<Autos>().dy = cars_dy[i];
                newCar.GetComponent<Autos>().laps = cars_laps[i];
                pos2++;
                newCar.GetComponent<Autos>().linePos = pos2;
                newCar.GetComponent<Autos>().turnList = TurnsList[i];
                carS2.Add(newCar);
                S2Coches = pos2;
                spawnOffsetAux2 += 5f;
            }

            else if (st.steps[0].cars[i].dx == -1)
            {
                float zNewPos = 50f + spawnOffsetAux4;
                Vector3 newPos = new Vector3(-1.5f, 0.13f, zNewPos);

                GameObject newCar = Instantiate(carPrefabs[prefabIndex], newPos, Quaternion.identity);

                newCar.transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);

                newCar.transform.Rotate(0f, 180f, 0f);
                newCar.AddComponent<Autos>();
                newCar.GetComponent<Autos>().speed = C4velocity;
                newCar.GetComponent<Autos>().startPos = new Vector3(newPos.x, newPos.y, (newPos.z - spawnOffsetAux4));
                newCar.GetComponent<Autos>().endPos = new Vector3(newPos.x, newPos.y, -(newPos.z - spawnOffsetAux4));
                newCar.GetComponent<Autos>().id = st.steps[0].cars[i].id;
                newCar.GetComponent<Autos>().sector = 4;
                newCar.GetComponent<Autos>().dx = cars_dx[i];
                newCar.GetComponent<Autos>().dy = cars_dy[i];
                newCar.GetComponent<Autos>().laps = cars_laps[i];
                pos4++;
                newCar.GetComponent<Autos>().linePos = pos4;
                newCar.GetComponent<Autos>().turnList = TurnsList[i];
                carS4.Add(newCar);
                S4Coches = pos4;
                spawnOffsetAux4 -= 5f;
            }

        }
    }
}
