using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject SpawnPoint1;
    [SerializeField] private GameObject SpawnPoint2;
    [SerializeField] private GameObject SpawnPoint3;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject initialBoxPrefab;
    [SerializeField] private GameObject playerSpawnPoint;
    [SerializeField] private GameObject initialBoxSpawnPoint;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button resetButton;


    // Variable para llevar el marcador de puntos
    private int score = 0;

    public bool isGameOver = false;

    void Start()
    {
        // Llamamos a la función SpawnBox cada 2 segundos para generar cajas de forma periódica
       InvokeRepeating("SpawnBox", 2f, 2f);

        //El botón de reset solo se muestra cuando el juego termina, por lo que lo ocultamos al iniciar la escena
       resetButton.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void GameOver()
    {
        scoreText.text = "Game Over, Final Score: " + this.score.ToString();
        isGameOver = true;
        // Mostramos el botón de reset para que el jugador pueda reiniciar el juego
        resetButton.gameObject.SetActive(true);
    }

    public void SpawnBox()
    {
        // Generamos un número aleatorio entre 0 y 2 para seleccionar uno de los tres puntos de spawn
        int spawnPointIndex = UnityEngine.Random.Range(0, 3);
        Vector3 spawnPosition;
        switch(spawnPointIndex)
        {
            case 0:
                spawnPosition = SpawnPoint1.transform.position;
                break;
            case 1:
                spawnPosition = SpawnPoint2.transform.position;
                break;
            case 2:
                spawnPosition = SpawnPoint3.transform.position;
                break;
            default:
                spawnPosition = SpawnPoint1.transform.position;
                break;
        }
        // Instanciamos una nueva caja en la posición seleccionada
        Instantiate(boxPrefab, spawnPosition, Quaternion.identity);
    }


    public void AddScore()
    {
        // Si el juego ha terminado, no incrementamos el marcador
        if(isGameOver)
        {
            return; 
        }

        // Incrementamos el marcador en 1 cada vez que se llama a esta función
        this.score++;
        scoreText.text = "Score: " +  this.score.ToString();
    }

    public void ResetGame()
    {
        // Reiniciamos el marcador a 0 y actualizamos el texto en pantalla
        this.score = 0;
        scoreText.text = "Score: " + this.score.ToString();
        // Reiniciamos el estado del juego para permitir que se sigan sumando puntos
        isGameOver = false;

        // Reposicionamos al jugador y a la caja inicial en sus puntos de spawn correspondientes
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerSpawnPoint.transform.position;
        // Reiniciamos la velocidad del jugador para evitar que siga moviéndose después de ser reposicionado   
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; 
        // Destruimos todas las cajas que estén actualmente en la escena para limpiar el juego
        Instantiate(initialBoxPrefab, initialBoxSpawnPoint.transform.position, Quaternion.identity);

        // Ocultamos el botón de reset nuevamente para que no se muestre durante el juego
        resetButton.gameObject.SetActive(false);
    }

}
