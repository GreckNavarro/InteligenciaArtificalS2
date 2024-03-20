using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Transform cursor; // C�mara desde la que se lanzar� el raycast

    void Update()
    {
        // Detectar clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Lanzar un rayo desde la posici�n del mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Realizar la intersecci�n del rayo con el plano
            if (Physics.Raycast(ray, out hit))
            {
                // Obtener la posici�n donde golpea el raycast y mostrarla en la consola
                Debug.Log("Posici�n de impacto: " + hit.point);
                cursor.position = hit.point;
            }
        }
    }
}
