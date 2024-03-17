//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PeopleSpawner : MonoBehaviour
//{
//    public GameObject personajePrefab;
//    public List<Transform> puntosDeSpawn;

//    void Start()
//    {
//        SpawnPersonaje();
//    }

//    void SpawnPersonaje()
//    {
//        if (personajePrefab != null && puntosDeSpawn.Count > 0)
//        {
//            GameObject nuevoPersonaje = Instantiate(personajePrefab, puntosDeSpawn[0].position, Quaternion.identity);
//            MovimientoPuntos movimientoPersonaje = nuevoPersonaje.GetComponent<MovimientoPuntos>();
//            if (movimientoPersonaje != null)
//            {
//                movimientoPersonaje.SetPuntosAseguir(puntosDeSpawn);
//            }
//            else
//            {
//                Debug.LogWarning("El prefab del personaje no tiene componente MovimientoPuntos.");
//            }
//        }
//        else
//        {
//            Debug.LogWarning("Falta el prefab del personaje o los puntos de spawn.");
//        }
//    }
//}