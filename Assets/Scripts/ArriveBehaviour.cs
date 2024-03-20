using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveBehavoir : MonoBehaviour
{

    public Transform target; // El objeto al que queremos llegar
    public float maxSpeed = 5f; // Velocidad máxima del objeto
    public float slowingDistance = 5f; // Distancia a partir de la cual el objeto comenzará a desacelerar
    public float stoppingDistance = 1f; // Distancia a partir de la cual el objeto se detendrá

    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 targetDirection = target.position - transform.position;

        // Calcula la distancia al objetivo
        float distance = targetDirection.magnitude;

        // Calcula la velocidad deseada
        float desiredSpeed = maxSpeed;

        // Si estamos dentro de la distancia de frenado, ajusta la velocidad
        if (distance < slowingDistance)
        {
            desiredSpeed = maxSpeed * (distance / slowingDistance);
        }

        // Si estamos dentro de la distancia de parada, detén completamente
        if (distance < stoppingDistance)
        {
            desiredSpeed = 0f;
        }


        // Calcula la fuerza de dirección hacia la velocidad deseada
        transform.rotation = Quaternion.LookRotation(targetDirection.normalized);

        transform.position += transform.forward * Time.deltaTime * desiredSpeed;
    }
}
