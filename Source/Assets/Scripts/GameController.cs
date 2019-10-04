using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private float droneSpawnSpeed;
    [SerializeField]
    private GameObject dronePrefab;
    [SerializeField]
    private GameObject deadText;

    public static GameState gameState;

    private void Awake()
    {
        gameState = GameState.Start;
    }

    private void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        if (GameController.gameState == GameState.Lost)
        {
            deadText.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
            CancelInvoke();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    public void StartInvoke()
    {
        InvokeRepeating(nameof(SpawnDrone), 1, droneSpawnSpeed);
    }

    private void SpawnDrone()
    {
        Instantiate(dronePrefab, transform.position, Quaternion.identity);
    }

    private void ResetGame()
    {
        gameState = GameState.Running;
        deadText.SetActive(false);
        CancelInvoke();
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(player);
        }
        foreach (GameObject drone in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(drone);
        }
        foreach (GameObject drone in GameObject.FindGameObjectsWithTag("Slave"))
        {
            Destroy(drone);
        }
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(bullet);
        }
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        StartInvoke();
    }
}
