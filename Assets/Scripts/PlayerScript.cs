using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.Animations;
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
    // Variable para verificar si el jugador está en el suelo
    public bool isGrounded = false;

    void Start()
    {
        //Obtenemos los componentes necesarios
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        // Evita que el raycast detecte el propio collider del jugador
        Physics2D.queriesStartInColliders = false; 
        // Inicializamos el parámetro de animación "IsGrounded" en false para que el jugador comience en el aire    
        isGrounded = false;
        animator.SetBool("IsGrounded", isGrounded);
       
    }

    void Update()
    {
        //Obtenimos el input de movimiento del jugador
        movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
        // Actualizamos el parámetro de velocidad en el Animator para controlar las animaciones de movimiento
        animator.SetFloat("velocityX", Mathf.Abs(movementInput.x));
        if(movementInput.x > 0)
        {
            // Si el jugador se mueve hacia la derecha, aseguramos que la escala del sprite esté orientada hacia la derecha
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(movementInput.x < 0)
        {
            // Si el jugador se mueve hacia la izquierda, invertimos la escala del sprite para que mire hacia la izquierda
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        //Aplicamos el movimiento al Rigidbody2D
        rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, rb.linearVelocity.y);
        // Realizamos un raycast hacia abajo para verificar si el jugador está en el suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.47f);
        // Si el raycast detecta un collider debajo del jugador, consideramos que está en el suelo
        if(hit.collider != null)
        {
            // Si el raycast detecta un collider debajo del jugador, consideramos que está en el suelo
            isGrounded = true;
            animator.SetBool("IsGrounded", isGrounded);
        }else
        {   // Si el raycast no detecta ningún collider, el jugador está en el aire
            isGrounded = false;
            animator.SetBool("IsGrounded", isGrounded);
        }
        
    }

    // Función para manejar el input de salto del jugador
    public void Jump(InputAction.CallbackContext context)
    {
        //Verificamos la fase del input para aplicar la fuerza de salto en la etapa "performed"
        if(context.performed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Función para manejar la interacción con las cajas al entrar en contacto con ellas
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colision con: " + collision.gameObject.name);
        // Verificamos si el objeto con el que colisionamos es una caja (su nombre comienza con "Box")
        if(collision.gameObject.name.StartsWith("Box") && this.transform.parent == null)
        {   // Si el jugador no tiene un objeto padre, asignamos la caja como su hijo para que se mueva junto con el jugador
            this.transform.SetParent(collision.transform);
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Verificamos si el objeto con el que dejamos de colisionar es una caja (su nombre comienza con "Box")
        if(collision.gameObject.name.StartsWith("Box"))
        {   // Si el jugador deja de colisionar con la caja, removemos la relación de padre-hijo para que la caja ya no se mueva junto con el jugador
            this.transform.SetParent(null);
        }
    }



}
