using UnityEngine;

public class BoxScript : MonoBehaviour
{

    private float speedY = 2f;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.down * speedY * Time.deltaTime;
    }


}