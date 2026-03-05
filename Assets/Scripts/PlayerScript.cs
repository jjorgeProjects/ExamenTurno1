using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    //Componentes que usaremos
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Animator animator;

    //Variable para el movimiento
    private Vector2 movementInput;

    [SerializeField] private float moveSpeed = 5f;

    void Start()
    {
        //Obtenemos los componentes necesarios
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Obtenimos el input de movimiento del jugador
        movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        //Aplicamos el movimiento al Rigidbody2D
        rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, rb.linearVelocity.y);

    }

}
