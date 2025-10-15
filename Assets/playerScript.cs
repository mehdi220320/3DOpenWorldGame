using UnityEngine;

public class playerScript : MonoBehaviour
{
    [SerializeField] Rigidbody myRigidBody;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpStrength = 10f;
    [SerializeField] float turnSensitivity = 3f;
    [SerializeField] float waterLevelY = 2.0f; 


    Vector3 _moveInput;
    [SerializeField] Animator animator;

    bool _isGrounded = true;
    bool _isJumping = false;
    internal bool IsSwimming=false;
    void Update()
    {
        
        HandleRotation();   
        HandleMovementInput();
        HandleJumpAndFall();
    }

    void HandleRotation()
    {
        if (_isGrounded)
        {
            if (Input.GetKey(KeyCode.Keypad6))
            {
                transform.Rotate(0f, turnSensitivity * Time.deltaTime * 60f, 0f);
            }
            else if (Input.GetKey(KeyCode.Keypad4))
            {
                transform.Rotate(0f, -turnSensitivity * Time.deltaTime * 60f, 0f);
            }
        }
    }




    void HandleMovementInput() { 
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical");
        _moveInput = new Vector3(h, 0, v).normalized; 
    }

    void HandleJumpAndFall()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && !IsSwimming)
        {
            _isGrounded = false;
            _isJumping = true;

            myRigidBody.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);

            animator.SetBool("IsJumping", true);
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsLanding", false);
        }

        if (!_isGrounded && myRigidBody.linearVelocity.y < -0.1f)
        {
            animator.SetBool("IsFalling", true);
            animator.SetBool("IsJumping", false);
        }
    }
    public float testFalling = -5f;
    void FixedUpdate()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;

        Vector3 moveDirection = transform.forward * _moveInput.z + transform.right * _moveInput.x;
        Vector3 moveVelocity = moveDirection * currentSpeed;
        Vector3 newPosition = myRigidBody.position + moveVelocity * Time.deltaTime;
        myRigidBody.MovePosition(newPosition);
        if (_isGrounded && myRigidBody.linearVelocity.y < testFalling)
        {
            _isGrounded = false;
        }

        if (!_isGrounded && myRigidBody.linearVelocity.y < testFalling)
        {
            animator.SetBool("IsFalling", true);
            animator.SetBool("IsJumping", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat("Run", 1);
            animator.SetFloat("RunX", _moveInput.x);
            animator.SetFloat("RunY", _moveInput.z);
        }
        else
        {
            animator.SetFloat("Run", -1);
            animator.SetFloat("MoveX", _moveInput.x);
            animator.SetFloat("MoveY", _moveInput.z);
        }
        if (IsSwimming)
        {
            myRigidBody.useGravity = false;
            animator.SetBool("IsSwimming", true);
            animator.SetFloat("MoveX", _moveInput.x);
            animator.SetFloat("MoveY", _moveInput.z);
        }
        else
        {
            myRigidBody.useGravity = true;
            animator.SetBool("IsSwimming", false);

            // myRigidBody.constraints = RigidbodyConstraints.FreezePositionY;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!_isGrounded)
            {
                _isGrounded = true;
                _isJumping = false;

                animator.SetBool("IsLanding", true);
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", false);

                Invoke(nameof(ResetLanding), 0.2f);
            }
        }
        else
        {
            if (myRigidBody.linearVelocity.y < -0.1f)
            {
                animator.SetBool("IsFalling", true);
                animator.SetBool("IsJumping", false);
            }
        }
    }

    void ResetLanding()
    {
        animator.SetBool("IsLanding", false);
    }
}
