using UnityEngine;

public class CarSuspension : MonoBehaviour
{
    public Transform[] wheels;
    public Collider[] wheelColliders;
    public float baseSpringConstant = 10.0f;
    public float baseDamping = 1.0f;
    public float restLength = 0.7f;
    public float maxCompression = 0.3f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private float springConstant;
    private float damping;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Skalowanie sił sprężystości i tłumienia w zależności od masy samochodu
        springConstant = baseSpringConstant * rb.mass / 1000.0f;
        damping = baseDamping * rb.mass / 1000.0f;

        for (int i = 0; i < wheels.Length; i++)
        {
            if (wheelColliders[i] == null)
            {
                SphereCollider wheelCollider = wheels[i].gameObject.AddComponent<SphereCollider>();
                wheelCollider.radius = 0.3f;
                wheelCollider.center = Vector3.zero;
                wheelCollider.material = new PhysicMaterial() { frictionCombine = PhysicMaterialCombine.Average, bounciness = 0 };
                wheelColliders[i] = wheelCollider;
            }
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            Transform wheel = wheels[i];
            Collider wheelCollider = wheelColliders[i];
            
            if (Physics.CheckSphere(wheel.position, wheelCollider.bounds.extents.y, groundLayer))
            {
                float compression = Mathf.Clamp(restLength - wheelCollider.bounds.extents.y, 0, maxCompression);

                Vector3 springForce = transform.up * (springConstant * compression);

                Vector3 dampingForce = -damping * rb.GetPointVelocity(wheel.position).y * transform.up;

                rb.AddForceAtPosition(springForce + dampingForce, wheel.position);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (wheels != null && wheels.Length > 0)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                if (wheels[i] != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(wheels[i].position, 0.3f);
                }
            }
        }
    }
}
