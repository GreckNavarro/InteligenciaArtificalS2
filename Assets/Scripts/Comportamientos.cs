using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comportamientos : MonoBehaviour
{


    public float radio;
    public float maxSpeed = 100f;
    public float maxSpeedRotation = 5.0f;
    public float stopdistance = 150f;
    public float Speed;
    public Transform Target;
    public float radioWander;
    public float rateWander;
    public float frameRateWander = 0;
    public float obstacleDetectionDistance;
    public float avoidanceRotationSpeed;

    public void Seek(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * maxSpeedRotation);
        transform.position += transform.forward * Time.deltaTime * maxSpeed;
    }

    public void Flee(Vector3 target)
    {
        Vector3 direction = (transform.position - target).normalized;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * maxSpeedRotation);
        transform.position += transform.forward * Time.deltaTime * maxSpeed;
    }

    public void Evade(Transform targetTransform, float predictionTime)
    {
        Vector3 targetPos = targetTransform.position + (targetTransform.forward * predictionTime);
        Flee(targetPos);
    }

    public void Arrive(Vector3 target)
    {
        Vector3 direction = (target - transform.position);
        float distance = direction.magnitude;

        if(distance < radio)
        {
            Speed = Mathf.Clamp(distance/radio,0,radio);
        }
        else
        {
            Speed = Mathf.Lerp(Speed, maxSpeed, Time.deltaTime * 5);
        }

        transform.position += direction * Time.deltaTime * Speed;

    }

    public void Wander()
    {
        Vector3 rnd = Vector3.zero;
        Vector3 target = Vector3.zero;

        if(frameRateWander > rateWander)
        {
            rnd = Random.insideUnitSphere * radioWander;
            rnd.y = 0;
            target = transform.position + transform.forward + rnd;
            frameRateWander = 0;
            transform.rotation = Quaternion.LookRotation((target - transform.position).normalized);
        }
        frameRateWander += Time.deltaTime;
        transform.position += transform.forward * Time.deltaTime * Speed;
    }

    public void Pursuit(Transform targetTransform, float predictionTime)
    {
        Vector3 targetPos = targetTransform.position + (targetTransform.forward * predictionTime);
        Seek(targetPos);
    }

    public void ObstacleAvoidance()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward,out hit, obstacleDetectionDistance))
        {
            Vector3 avoidanceDirection = Vector3.Cross(hit.normal, Vector3.up);
            transform.Rotate(avoidanceDirection * avoidanceRotationSpeed * Time.deltaTime);

        }
        else
        {
            transform.position += transform.forward * Time.deltaTime * maxSpeed;
        }

    }

    public void Separation(List<Transform> agents, float separationDistance)
    {
        Vector3 separationForce = Vector3.zero;

        foreach(Transform agent in agents)
        {
            float distance = Vector3.Distance(transform.position, agent.position);

            if(distance > 0 && distance < separationDistance)
            {
                Vector3 direction = transform.position - agent.position;
                direction.Normalize();
                separationForce += direction / distance;
            }
        }

        transform.position += separationForce;
    }

    public Vector3 Cohesion(List<Transform> agents)
    {
        Vector3 cohesionCenter = Vector3.zero;

        foreach(Transform agent in agents)
        {
            cohesionCenter += agent.position;
        }

        cohesionCenter /= agents.Count;
        return cohesionCenter - transform.position;
    }


    public Vector3 Alignment(List<Transform> agents)
    {
        Vector3 averageDirection = Vector3.zero;

        foreach (Transform agent in agents)
        {
            averageDirection += agent.forward;
        }

        averageDirection /= agents.Count;

        return averageDirection.normalized;
    }

    public void PathFollowing(List<Vector3> path, float arrivalRadius)
    {
        if(path.Count == 0)
        {
            Debug.LogWarning("No hay camino definido para seguir. ");
            return;
        }

        Vector3 target = path[0];
        if(Vector3.Distance(transform.position, target) < arrivalRadius)
        {
            path.RemoveAt(0);
        }
        if(path.Count > 0)
        {
            Seek(target);
        }
        else
        {
            Arrive(target);
        }

    }

    private void Update()
    {
        Flee(Target.transform.position);
    }

















}
