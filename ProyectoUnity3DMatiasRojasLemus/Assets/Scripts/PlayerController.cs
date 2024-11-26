
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedMove = 10f;
    public float jumpPower = 7f;
    public float gravityScale = 1f;
    public CharacterController playerController;
    public Camera playerCamera;
    public Animator animator;
    public Transform transPlayer;
    
    
    private Vector3 moveDirection;
    private Vector3 jumpDirection;
    private float yStore;



    //Posicion CheckPoint
    private float posicionX;
    private float posicionY;
    private float posicionZ;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkMovement();
    }

    void checkMovement()
    {
        yStore = moveDirection.y;

        //Movimiento
        checkMove();

        moveDirection.y = yStore;

        //Salto
        checkJump();

        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
        playerController.Move(moveDirection * Time.deltaTime);

        //Rotación del personaje
        playerRotation();

        //Deteccion de suelo
        checkGround();
    }

    void playerRotation()
    {
        transform.rotation = Quaternion.Euler(0f, playerCamera.transform.rotation.eulerAngles.y, 0f);
    }

    void checkJump()
    {
        if (Input.GetButton("Jump"))
        {
            moveDirection.y = jumpPower;
        }
    }

    void checkMove()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveDirection = moveDirection * speedMove;

        if ((Mathf.Abs(playerController.velocity.x) != 0) || (Mathf.Abs(playerController.velocity.z) != 0))
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void checkGround()
    {
        if (playerController.isGrounded)
        {
            animator.SetBool("isGrounded", true);
        }
        else
        {
            animator.SetBool("isGrounded", false);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "water")
        {
            animator.SetBool("isDead", true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "water")
        {
            animator.SetBool("isDead", false);
        }
        // Si entra en contacto con un Checkpoint, guarda la posición con la que ha entrado a dicho checkpoint
        else if (collision.gameObject.tag == "CheckPoint")
        {
            posicionX = transPlayer.position.x;
            posicionY = transPlayer.position.y;
            posicionZ = transPlayer.position.z;
        }
    }
}
