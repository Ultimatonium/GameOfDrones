using UnityEngine;

public class Flocking : MonoBehaviour
{
    public static Vector3 Alignment(GameObject source, GameObject[] boids, float alignmentDistance)
    {
        int boidsInRange = 0;
        Vector3 mediumDirection = Vector3.zero;
        foreach (GameObject boid in boids)
        {
            if (boid == source) continue;
            if (Vector3.Distance(source.transform.position, boid.transform.position) < alignmentDistance)
            {
                mediumDirection += (Vector3)boid.GetComponent<Drone>().velocity;
                boidsInRange++;
            }
        }
        if (boidsInRange == 0) return mediumDirection;
        mediumDirection /= boidsInRange;
        return mediumDirection.normalized;
    }

    public static Vector3 Separation(GameObject source, GameObject[] boids, float alignmentDistance)
    {
        int boidsInRange = 0;
        Vector3 fleeDirection = Vector3.zero;
        foreach (GameObject boid in boids)
        {
            if (boid == source) continue;
            if (Vector3.Distance(source.transform.position, boid.transform.position) < alignmentDistance)
            {
                //fleeDirection += source.transform.position - boid.transform.position;
                fleeDirection += boid.transform.position - source.transform.position;
                boidsInRange++;
            }
        }
        if (boidsInRange == 0) return fleeDirection;
        fleeDirection /= boidsInRange;
        fleeDirection *= -1;
        return fleeDirection.normalized;
    }

    public static Vector3 Cohesion(GameObject source, GameObject[] boids, float alignmentDistance)
    {
        int boidsInRange = 0;
        Vector3 kuschelDirection = Vector3.zero;
        foreach (GameObject boid in boids)
        {
            if (boid == source) continue;
            if (Vector3.Distance(source.transform.position, boid.transform.position) < alignmentDistance)
            {
                kuschelDirection += boid.transform.position;
                boidsInRange++;
            }
        }
        if (boidsInRange == 0) return kuschelDirection;
        kuschelDirection /= boidsInRange;
        kuschelDirection = kuschelDirection - source.transform.position;
        return kuschelDirection.normalized;
    }
}
