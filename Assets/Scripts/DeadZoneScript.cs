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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Destruirlo
            Destroy(collision.gameObject);
            gameManager.GetComponent<GameManagerScript>().SetGameOver();

        }
        else if (collision.gameObject.name.StartsWith("Box"))
        {
            Destroy(collision.gameObject);
            gameManager.GetComponent<GameManagerScript>().IncreaseScore();
        }
    }
}
