
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedMove = 10f;
    public float jumpPower = 7f;
    public float gravityScale = 1f;
    public CharacterController playerController;
    public Camera playerCamera;
    public Animator animator;
    
    
    private Vector3 moveDirection;
    private Vector3 jumpDirection;
    private float yStore;
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


}
