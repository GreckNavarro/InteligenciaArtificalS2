using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Transform cursor; // Cámara desde la que se lanzará el raycast

    void Update()
    {
        // Detectar clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Lanzar un rayo desde la posición del mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Realizar la intersección del rayo con el plano
            if (Physics.Raycast(ray, out hit))
            {
                // Obtener la posición donde golpea el raycast y mostrarla en la consola
                Debug.Log("Posición de impacto: " + hit.point);
                cursor.position = hit.point;
            }
        }
    }
}
