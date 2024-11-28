
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedMove = 15f;
    public float jumpPower = 10f;
    public float gravityScale = 1f;
    public CharacterController playerController;
    public Camera playerCamera;
    public Animator animator;
    public Transform transPlayer;
    
    
    private Vector3 moveDirection;
    private Vector3 jumpDirection;
    private float yStore;


    private bool isDead;



    //Posicion CheckPoint
    private float posicionX;
    private float posicionY;
    private float posicionZ;



    // Start is called before the first frame update
    public void Start()
    {
        speedMove = 15f;
        jumpPower = 10f;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        CheckMovement();
        Respawn();
    }

    void CheckMovement()
    {
        yStore = moveDirection.y;

        //Movimiento
        CheckMove();

        moveDirection.y = yStore;

        //Salto
        CheckJump();

        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
        playerController.Move(moveDirection * Time.deltaTime);

        //Rotaci�n del personaje
        PlayerRotation();

        //Deteccion de suelo
        CheckGround();
    }

    void PlayerRotation()
    {
        transform.rotation = Quaternion.Euler(0f, playerCamera.transform.rotation.eulerAngles.y, 0f);
    }

    void CheckJump()
    {
        if (Input.GetButton("Jump") && playerController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
    }

    void CheckMove()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveDirection = moveDirection * speedMove;

        if ((Mathf.Abs(playerController.velocity.x) != 0f) || (Mathf.Abs(playerController.velocity.z) != 0f))
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void CheckGround()
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

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "water")
        {
            Death();
        }

    }


    public void OnTriggerExit(Collider collision)
    {
        // Si entra en contacto con un Checkpoint, guarda la posici�n con la que ha entrado a dicho checkpoint
        if (collision.gameObject.tag == "checkPoint")
        {
            posicionX = transPlayer.position.x;
            posicionY = transPlayer.position.y;
            posicionZ = transPlayer.position.z;
            Debug.Log(posicionX);
            Debug.Log(posicionY);
            Debug.Log(posicionZ);
        }
    }

    void Death(){
        speedMove = 0f;
        jumpPower = 0f;
        isDead = true;
        animator.SetBool("isDead", true);
    }

    void Respawn(){
        if(isDead && Input.GetKey(KeyCode.R)){
            transPlayer.position = new Vector3(posicionX,posicionY,posicionZ);
            animator.SetBool("isDead", false);
            isDead = false;
            speedMove = 15f;
            jumpPower = 10f;
        }
    }
}
