using UnityEngine;

public class SuspensionW : MonoBehaviour
{
    [Header("Properties")]
    public float springConstant = 100.0f;
    public float damping = 10.0f;
    public float distance = 0.0f;
    public float rayLength = 10.0f;
    public LayerMask groundLayer;
    public Transform parent;
    public int numberOfWheels = 4;
    public float yRotationAxis = 0.0f;

    [Header("Offset")]
    public Vector3 wheelOffset;

    public float groundHeight = 0.0f;
    public float distanceToGround;
    private Rigidbody rb;
    private Rigidbody parentRb;
    private Vector3 startLocalPosition;
    private Quaternion initialRotation;
    private bool grounded;

    public float minHeightAboveGround = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        parentRb = parent.GetComponent<Rigidbody>();
        startLocalPosition = this.transform.localPosition;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        CalculateDistanceToGround();

        if (distanceToGround > 0)
        {
            groundHeight = transform.position.y - distanceToGround + distance;
        }

        PreventWheelSinking();
    }

    void FixedUpdate()
    {   
        Quaternion updatedRotation = Quaternion.Euler(parent.rotation.eulerAngles.z, parent.rotation.eulerAngles.y + yRotationAxis, transform.rotation.eulerAngles.z);
        transform.rotation = updatedRotation;

        Vector3 restPosition = parent.TransformPoint(startLocalPosition + wheelOffset);
        Vector3 displacement = transform.position - new Vector3(restPosition.x, restPosition.y - distance, restPosition.z);

        Vector3 springForce = (-springConstant * displacement) / numberOfWheels; 
        Vector3 dampingForce = (-damping * rb.velocity) / numberOfWheels;

        Vector3 parentDampingForce = (-damping * parentRb.velocity) / numberOfWheels;

        if (distanceToGround < 1.0f && distanceToGround > 0.0f)  {
            parentRb.AddForce(parentDampingForce);
            // rb.AddForce(springForce + dampingForce);
            parentRb.AddForceAtPosition(-springForce, transform.position);
        }

        rb.AddForce(springForce + dampingForce);
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

    void PreventWheelSinking()
    {
        if (distanceToGround > 0 && distanceToGround < minHeightAboveGround)
        {
            Vector3 correctedPosition = transform.position;
            correctedPosition.y = groundHeight + minHeightAboveGround;
            transform.position = correctedPosition;
        }
    }
}
