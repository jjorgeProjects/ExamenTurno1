using UnityEngine;

public class DeadZoneScript : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // Si el jugador entra en la zona de muerte, llamamos a la función GameOver del GameManager
            gameManager.GetComponent<GameManagerScript>().GameOver();
        }
    }
}
