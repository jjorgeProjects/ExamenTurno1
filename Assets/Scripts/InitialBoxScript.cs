using System.Collections;
using UnityEngine;

public class InitialBoxScript : MonoBehaviour
{
    void Start()
    {
        //Opcion1: Destroy
        Destroy(gameObject, 10f);

        //Opcion2: Invoke
        //Invoke("Destruir", 10f);

        //Opcion3: Corrutina
        //StartCoroutine(DestruirCorrutina());

    }

    void Update()
    {
        
    }

    void Destruir()
    {
        Destroy(gameObject);
    }

    IEnumerator DestruirCorrutina()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
