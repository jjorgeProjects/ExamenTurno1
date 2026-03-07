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
        // Verificamos si el objeto que entra en la zona de muerte es el jugador o una caja
        if(other.gameObject.CompareTag("Player") || other.gameObject.transform.Find("Player") != null)
        {
            // Si el jugador entra en la zona de muerte, llamamos a la función GameOver del GameManager
            gameManager.GetComponent<GameManagerScript>().GameOver();
        }else if(other.gameObject.name.StartsWith("Box"))
        {
            // Si una caja entra en la zona de muerte, la destruimos la caja y sumamos un punto al marcador
            gameManager.GetComponent<GameManagerScript>().AddScore();
            Destroy(other.gameObject);
        }
    }
}
