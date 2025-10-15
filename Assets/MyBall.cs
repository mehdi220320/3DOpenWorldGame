using UnityEngine;
using UnityEngine.InputSystem;

public class MyBall : MonoBehaviour
{
    public Rigidbody myRigidBody;
    public float moveStrength=10f;
    public float jumpStrength=10f;

    private Camera mainCamera;

    void Start()
    {
        if (myRigidBody == null)
            myRigidBody = GetComponent<Rigidbody>();

        mainCamera = Camera.main;
    }
    void Update()
    {
        // Jump with Space key
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            myRigidBody.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            horizontal = -1f;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            horizontal = 1f;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            vertical = 1f;
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            vertical = -1f;

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        if (movement != Vector3.zero)
        {
            myRigidBody.AddForce(movement.normalized * moveStrength, ForceMode.Force);
        }
    }
}