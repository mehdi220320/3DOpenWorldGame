using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayerFootsteps : MonoBehaviour
{
    [Header("Detection")]
    public float moveThreshold = 0.2f;   // how much speed counts as "moving"
    public float stepDelay = 0.4f;       // time between steps

    [Header("Audio")]
    public AudioSource source;           // assign or it will auto-grab
    public AudioClip[] clips;            // put MetalSteps_01 here (or multiple)

    private float timer;
    private CharacterController cc;
    private Rigidbody rb;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        if (!source) source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = false;
    }

    void Update()
    {
        float speed = 0f;
        bool grounded = true;

        if (cc)
        {
            speed = cc.velocity.magnitude;
            grounded = cc.isGrounded;
        }
        else if (rb)
        {
            speed = rb.linearVelocity.magnitude;
            // simple ground check for Rigidbody players
            grounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.2f);
        }
        else
        {
            // fallback to input if neither is present
            var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            speed = input.magnitude;
        }

        if (grounded && speed > moveThreshold)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f && clips != null && clips.Length > 0)
            {
                // small randomization for variety
                source.pitch = Random.Range(0.96f, 1.04f);
                source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
                timer = stepDelay;
            }
        }
        else
        {
            timer = 0f;
        }
    }
}