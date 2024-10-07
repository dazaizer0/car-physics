using UnityEngine;

public class HookesLaw : MonoBehaviour
{
    [Header("Properties")]
    public float springConstant = 50.0f;
    public float damping = 5.0f;
    public float groundHeight = 0.0f;
    
    [Header("Keys")]
    public KeyCode restartKey = KeyCode.Space;

    private Rigidbody rb;
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = this.transform.position;
    }

    void Update()
    {
        if(Input.GetKey(restartKey))
        {
            this.transform.position = startPosition;
        }
    }

    void FixedUpdate()
    {
        float displacement = transform.position.y - groundHeight;

        Vector3 springForce = new Vector3(0, -springConstant * displacement, 0);
        Vector3 dampingForce = new Vector3(0, -damping * rb.velocity.y, 0);

        rb.AddForce(springForce + dampingForce);
    }
}
