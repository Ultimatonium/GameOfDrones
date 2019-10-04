using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        Destroy(gameObject);
    }
}
