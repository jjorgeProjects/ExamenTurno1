using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //Aquí están los componentes definidos 
    //Estos serán los que necesitamos manipular
    //PERO NO BASTA CON DEFINIRLOS SOLAMENTE
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Animator animator;
    Vector2 movementInput;

    //public float speedX;
    [SerializeField] private float speedX;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool IsGrounded;

    void Start()
    {
       //HAY QUE INICIALIZARLOS AQUÍ
       // EL ESQUEMA ES:
       // variableComponente = GetComponent<TipoComponente>();
       rb = GetComponent<Rigidbody2D>();
       playerInput = GetComponent<PlayerInput>();
       animator = GetComponent<Animator>();

       IsGrounded = false;

        //Desactiva las colisiones con el propio collider
        Physics2D.queriesStartInColliders = false;


    }

    void Update()
    {   //La variable playerInput tiene un actions
        // en ese actions podemos acceder a las acciones definidas en el InputAction map
        // por ejemplo, el action "Move".
        //Si hacemos esto, no guardamos el resultado en ningún sitio
        //playerInput.actions["Move"].ReadValue<Vector2>(); 
        movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
        animator.SetFloat("velocityX", Mathf.Abs(movementInput.x));

        if(movementInput.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (movementInput.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }

    }

    //TODO LO QUE TENGA QUE VER CON EL RB, VA EN FIXEDUPDATE
    private void FixedUpdate()
    {

       
        //Como tenemos que hacer cosas con el RB tenemos que usar la variable rb
        rb.linearVelocityX = movementInput.x * speedX;



        //Detectar el suelo con RayCast
        //Necesitamos un origen, la posición del jugador
        //Dirección para emitir el rayo y longitud
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.47f);


        if(hit.collider == null)
        {
            IsGrounded = false;
            animator.SetBool("IsGrounded", false);
        }
        else
        {
            IsGrounded = true;
            animator.SetBool("IsGrounded", true);

        }

    }

    public void Jump(InputAction.CallbackContext context)
    {
        //Tres fases: started, performed, cancelled
        if (context.performed && IsGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("Box"))
        {

            this.transform.SetParent(collision.gameObject.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("Box"))
        {
            this.transform.SetParent(null);
        }
    }

}
