using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerController : Drone
{
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private GameObject bulletPrefab;

    protected override void Update()
    {
        base.Update();
        Shoot();
    }

    protected override Vector2 Move()
    {
        Rotate();
        Vector2 velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            velocity += (Vector2)transform.up;
        velocity *= speed;
        return velocity;
    }

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rotateSpeed);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * rotateSpeed);
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        GameObject rebel = GameObject.FindGameObjectWithTag("Slave");
        if (rebel == null)
        {
            GameController.gameState = GameState.Lost;
            return;
        }
        rebel.GetComponent<AiController>().SetNewState("Enemy");
    }
}
