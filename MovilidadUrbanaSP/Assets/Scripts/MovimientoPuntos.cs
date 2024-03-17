using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPuntos : MonoBehaviour
{
    public Transform[] puntos; // Array de puntos a seguir
    public float velocidad = 5f; // Velocidad de movimiento del personaje
    public float rotacionSuavizada = 10f; // Suavizado de la rotación
    private int indicePunto = 0; // Índice del punto actual
    private bool avanzando = true; // Indica si está avanzando o retrocediendo

    void Update()
    {
        // Verificar si hay puntos definidos
        if (puntos.Length == 0)
            return;

        // Obtener dirección hacia el punto actual o anterior
        Vector3 direccion;
        if (avanzando)
        {
            direccion = (puntos[indicePunto].position - transform.position).normalized;
        }
        else
        {
            direccion = (puntos[indicePunto - 1].position - transform.position).normalized;
        }
        transform.position += direccion * velocidad * Time.deltaTime;

        // Rotar hacia la dirección del próximo punto o anterior
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, rotacionSuavizada * Time.deltaTime);

        // Si estamos lo suficientemente cerca del punto actual, cambiar de punto
        if (Vector3.Distance(transform.position, puntos[indicePunto].position) < 0.1f)
        {
            if (avanzando)
            {
                indicePunto++;
                // Si hemos alcanzado el último punto, comenzar a retroceder
                if (indicePunto >= puntos.Length)
                {
                    indicePunto = puntos.Length - 1;
                    avanzando = false;
                }
            }
            else
            {
                indicePunto--;
                // Si hemos llegado al primer punto, volver a avanzar
                if (indicePunto < 0)
                {
                    indicePunto = 0;
                    avanzando = true;
                }
            }
        }
    }

    // Dibujar gizmos para visualizar los puntos en el editor
    private void OnDrawGizmos()
    {
        if (puntos == null || puntos.Length < 2)
            return;

        for (int i = 0; i < puntos.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(puntos[i].position, 0.3f);

            if (i > 0)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(puntos[i - 1].position, puntos[i].position);
            }
        }
    }
}
