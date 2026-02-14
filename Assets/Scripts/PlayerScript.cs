using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Animator animator;

    //Atributo para la velocidad
    [Header("Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    //Vector2 para almacenar la variación del input
    Vector2 inputMovement;

    float deltaXMovement;
    float deltaYMovement;


    void Start()
    {
        //Recuperamos los componentes que necesitamos
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        inputMovement = Vector2.zero;
        speed = 1f;
        jumpForce = 4f;
    }

    void Update()
    {
        //Obtenemos el movimiento en horizontal del componente PlayerInput
        inputMovement = playerInput.actions["Move"].ReadValue<Vector2>();

        deltaXMovement = speed * inputMovement.x;

    }

    private void FixedUpdate()
    {
        //Modificamos la velocidad que tiene el Rigidbody2D en cada actualización en base a la entrada
        rb.linearVelocity = new Vector2(deltaXMovement, rb.linearVelocity.y);
    }

    //Callback para el salto
    public void Jump(InputAction.CallbackContext callbackContext)
    {
        //Cuando se lanza este evento, se aplica una fuerza hacia arriba para simular el salto
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            Debug.Log("He chocado con la caja.");
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            Debug.Log("Sigo chocando con la caja.");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            Debug.Log("Dejo de chocar con la caja.");
        }
    }

}
