using UnityEngine;

public abstract class Drone : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float mass;

    public Vector2 velocity;

    protected virtual void Awake()
    {
        velocity = Vector2.zero;
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        velocity = Move() * Time.deltaTime;
        transform.position += (Vector3)velocity;
    }

    protected abstract Vector2 Move();
}
