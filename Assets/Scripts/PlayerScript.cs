using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    
    //Atributos de la clase: los componentes que necesitamos
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Animator animator;

    //Atributo para la velocidad
    [Header("Parameters")]
    [SerializeField] private float speed;

    //Vector2 para almacenar la variación del input
    Vector2 inputMovement;


    void Start()
    {
        //Recuperamos los componentes que necesitamos
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        inputMovement = Vector2.zero;
        speed = 1f;
    }

    void Update()
    {
        //Obtenemos el movimiento en horizontal del componente PlayerInput
        inputMovement = playerInput.actions["Move"].ReadValue<Vector2>();
        Debug.Log(inputMovement.x + " " + inputMovement.y);

        //Calculamos la cantidad de movimiento en cada dimensión,
        //utilizando la velocidad, la variación del input y el uso de la variable deltaTime 
        float deltaXMovement = speed * inputMovement.x * Time.deltaTime;
        float deltaYMovement = speed * inputMovement.y * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + deltaXMovement, transform.position.y + deltaYMovement, transform.position.z);
    }




}
