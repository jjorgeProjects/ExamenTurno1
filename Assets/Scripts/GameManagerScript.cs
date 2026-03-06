using UnityEngine;
using TMPro;
public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject SpawnPoint1;
    [SerializeField] private GameObject SpawnPoint2;
    [SerializeField] private GameObject SpawnPoint3;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private TMP_Text scoreText;

    bool gameOver = false;
    int score = 0;  

    void Start()
    {
        InvokeRepeating("SpawnBox", 3f, 2f);

    }

    void Update()
    {
        
    }

    public void SetGameOver()
    {
        gameOver = true;
        scoreText.text = "Game over";
    }

    public void IncreaseScore()
    {
        if(gameOver == false) { 
            score++;
            scoreText.text = "Score: " + score;
        }
    }

    void SpawnBox()
    {
        int spawnPointIndex = Random.Range(0, 3);
        Vector3 spawnPosition;
        switch (spawnPointIndex)
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
                spawnPosition = SpawnPoint3.transform.position;
                break;

        }

        Instantiate(boxPrefab, spawnPosition, Quaternion.identity);

    }

}
