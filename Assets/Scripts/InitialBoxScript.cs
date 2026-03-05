using UnityEngine;
using System.Collections;

public class InitialBoxScript : MonoBehaviour
{
    void Start()
    {
        //Varias opciones para hacerla desaparecer al iniciar la escena, puedes usar cualquiera de las siguientes:
        // Opción 1: Destruir el GameObject
        //Destroy(gameObject, 2f); --> Destruye el GameObject después de 2 segundos
        // Opción 2:Llama al método DestroyBox después de 2 segundos
        //Invoke("DestroyBox", 2f); 
        // Opción 3: Coroutina para destruir el GameObject después de un tiempo
        StartCoroutine(DestroyAfterTime(10f));


    }

    void Update()
    {
        
    }

    void DestoyBox()
    {
        Destroy(gameObject);
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
