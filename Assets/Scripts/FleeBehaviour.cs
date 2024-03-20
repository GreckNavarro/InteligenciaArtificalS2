using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleek : MonoBehaviour
{
    public Transform target; // El objetivo al que se quiere huir
    public float speed = 5f; // Velocidad de movimiento de la entidad que huye
    public float maxSpeedRotation = 5f; // Velocidad máxima del objeto
    public float stopdistance = 150f;

    void Update()
    {
        if (target != null)
        {
            
            Vector3 direction = transform.position - target.position;
            float currentdistance = direction.magnitude;
            direction.Normalize(); // Normalizar para obtener una dirección constante

            if (currentdistance < stopdistance)
            {
                speed = 100;
            }
            else
            {
                speed = 0f;
            }


            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction.normalized), Time.deltaTime * maxSpeedRotation);

            transform.position += transform.forward * Time.deltaTime * speed;
        }
    }
}
