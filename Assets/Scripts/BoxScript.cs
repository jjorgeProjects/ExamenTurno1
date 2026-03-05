using UnityEngine;

public class BoxScript : MonoBehaviour
{

    
    private float speed = 2f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }


}