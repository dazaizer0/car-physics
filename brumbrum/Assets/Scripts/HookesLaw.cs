using UnityEngine;

public class HookesLaw : MonoBehaviour
{
    [Header("Properties")]
    public float springConstant = 50.0f;
    public float damping = 5.0f;
    public float distance = 0.0f;
    public float rayLength = 10.0f;
    public LayerMask groundLayer;
    public Transform parent;
    public int numberOfWheels = 4;

    [Header("Offset")]
    public Vector3 wheelOffset;

    public float groundHeight = 0.0f;
    public float distanceToGround;
    private Rigidbody rb;
    private Rigidbody parentRb;
    private Vector3 startLocalPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        parentRb = parent.GetComponent<Rigidbody>();
        startLocalPosition = this.transform.localPosition;
    }

    void Update()
    {
        CalculateDistanceToGround();

        if (distanceToGround > 0)
        {
            groundHeight = transform.position.y - distanceToGround + distance;
        }
    }

    void FixedUpdate()
    {
        Vector3 restPosition = parent.TransformPoint(startLocalPosition + wheelOffset);
        Vector3 displacement = transform.position - new Vector3(restPosition.x, restPosition.y - distance, restPosition.z);
        
        Vector3 springForce = (-springConstant * displacement) / numberOfWheels; 
        Vector3 dampingForce = (-damping * rb.velocity) / numberOfWheels;

        rb.AddForce(springForce + dampingForce);
        parentRb.AddForceAtPosition(-springForce, transform.position);

        Vector3 parentDampingForce = (-damping * parentRb.velocity) / numberOfWheels;
        parentRb.AddForce(parentDampingForce);
    }

    void CalculateDistanceToGround()
    {
        Vector3 rayOrigin = transform.position;
        RaycastHit hit;

        Debug.DrawRay(rayOrigin, Vector3.down * rayLength, Color.red);

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayLength, groundLayer))
        {
            distanceToGround = hit.distance;
            Debug.Log("Distance to Ground: " + distanceToGround);
        }
        else
        {
            distanceToGround = -1;
            Debug.Log("No ground detected.");
        }
    }
}
