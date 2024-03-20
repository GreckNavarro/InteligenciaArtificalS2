using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehavoiur : MonoBehaviour
{
    public Transform target; // El objeto al que queremos llegar
    public float maxSpeed = 5f; // Velocidad máxima del objeto
    public float maxSpeedRotation = 5f; // Velocidad máxima del objeto
    void Start()
    {

    }

    void Update()
    {


        // Calcula la dirección hacia el objetivo
        Vector3 targetDirection = target.position - transform.position;


        // Calcula la fuerza de dirección hacia la velocidad deseada
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetDirection.normalized), Time.deltaTime * maxSpeedRotation);

        transform.position += transform.forward * Time.deltaTime * maxSpeed;

    }
}
