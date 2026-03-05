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
    
    //Variables para configurar la velocidad de movimiento y la fuerza de salto
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    void Start()
    {
        //Obtenemos los componentes necesarios
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        // Evita que el raycast detecte el propio collider del jugador
        Physics2D.queriesStartInColliders = false; 

        animator.SetBool("IsGrounded", false);
    }

    void Update()
    {
        //Obtenimos el input de movimiento del jugador
        movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
        // Actualizamos el parámetro de velocidad en el Animator para controlar las animaciones de movimiento
        animator.SetFloat("velocityX", Mathf.Abs(movementInput.x));
        
    }

    void FixedUpdate()
    {
        //Aplicamos el movimiento al Rigidbody2D
        rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, rb.linearVelocity.y);
        // Realizamos un raycast hacia abajo para verificar si el jugador está en el suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
        // Si el raycast detecta un collider debajo del jugador, consideramos que está en el suelo
        if(hit.collider != null)
        {
            // Si el raycast detecta un collider debajo del jugador, consideramos que está en el suelo
            animator.SetBool("IsGrounded", true);
        }
        else
        {   // Si el raycast no detecta ningún collider, el jugador está en el aire
            animator.SetBool("IsGrounded", false);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        //Verificamos la fase del input para aplicar la fuerza de salto en la etapa "performed"
        if(context.performed)
        {
            //Aplicamos una fuerza hacia arriba para saltar
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

}
