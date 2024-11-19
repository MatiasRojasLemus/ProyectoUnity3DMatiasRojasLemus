
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedMove = 10f;
    public float jumpPower = 7f;
    public CharacterController playerController;
    public float gravityScale = 1f;
    
    
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
        yStore = moveDirection.y;

        //Movimiento
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveDirection = moveDirection * speedMove;


        moveDirection.y = yStore;


        //Salto
        if (Input.GetButton("Jump"))
        {
            moveDirection.y = jumpPower;
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
        playerController.Move(moveDirection * Time.deltaTime);
    }
}
