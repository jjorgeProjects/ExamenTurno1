using System;
using UnityEngine;
using TMPro;
public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject SpawnPoint1;
    [SerializeField] private GameObject SpawnPoint2;
    [SerializeField] private GameObject SpawnPoint3;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private TMP_Text scoreText;

    // Variable para llevar el marcador de puntos
    private int score = 0;

    public bool isGameOver = false;

    void Start()
    {
        // Llamamos a la función SpawnBox cada 2 segundos para generar cajas de forma periódica
       InvokeRepeating("SpawnBox", 2f, 2f);
    }

    void Update()
    {
        
    }

    public void GameOver()
    {
        scoreText.text = "Game Over, Final Score: " + this.score.ToString();
        isGameOver = true;
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


}
