using System;
using UnityEngine;
using UnityEngine.Serialization;

public class AiController : Drone
{
    [SerializeField]
    private float pursueTime;
    [SerializeField]
    private float alignmentDistance;
    [SerializeField]
    private float distanceObstacleCheck;
    [SerializeField]
    private float alignmentWeight;
    [SerializeField]
    private float separationWeight;
    [SerializeField]
    private float cohesionWeight;
    [SerializeField]
    private float avoidWeight;

    private GameObject target;

    protected override void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    protected override Vector2 Move()
    {
        velocity += (Vector2)SteeringBehavior.Avoid(gameObject, velocity, distanceObstacleCheck, avoidWeight);
        switch (tag)
        {
            case "Slave":
                GameObject[] slaves = GameObject.FindGameObjectsWithTag("Slave");
                velocity += (Vector2)Flocking.Cohesion(gameObject, slaves, alignmentDistance) * cohesionWeight;
                velocity += (Vector2)Flocking.Separation(gameObject, slaves, alignmentDistance) * separationWeight;
                velocity += (Vector2)Flocking.Alignment(gameObject, slaves, alignmentDistance) * alignmentWeight;
                velocity += (Vector2)SteeringBehavior.Seek(transform.position, target.transform.position, velocity, speed, mass);
                velocity.Normalize();
                velocity *= speed;
                break;
            case "Enemy":
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                velocity += (Vector2)Flocking.Cohesion(gameObject, enemies, alignmentDistance) * cohesionWeight;
                velocity += (Vector2)Flocking.Separation(gameObject, enemies, alignmentDistance) * separationWeight;
                velocity += (Vector2)Flocking.Alignment(gameObject, enemies, alignmentDistance) * alignmentWeight;
                velocity += (Vector2)SteeringBehavior.Pursue(transform.position, target.transform.position,
                    target.GetComponent<Drone>().velocity, velocity, speed, mass, pursueTime);
                velocity.Normalize();
                velocity *= 2;
                break;
            default:
                Debug.LogError("Tag " + tag + " not handled");
                break;
        }
        return velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet")) return;
        SetNewState("Slave");
    }

    public void SetNewState(string newTag)
    {
        tag = newTag;
        switch (newTag)
        {
            case "Enemy":
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case "Slave":
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            default:
                Debug.LogError("Tag " + tag + " not handled");
                break;
        }
    }
}
