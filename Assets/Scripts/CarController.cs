using UnityEngine;
using UnityUtils;

public class CarController : MonoBehaviour
{
    public enum ControlType
    {
        Raw,
        PhysicsRaw,
        Physics
    }

    public ControlType controls;

    [DrawIf(nameof(controls), ControlType.Physics, ComparisonType.Equals, DisablingType.DontDraw)]
    public Rigidbody rb;

    public float MoveSpeed = 50;
    public float MaxSpeed = 15;
    public float Drag = 0.98f;
    public float SteerAngle = 20;
    public float Traction = 1;

    private Vector3 MoveForce;

    // Update is called once per frame
    void Update()
    {
        if (controls == ControlType.Physics)
        {
            // Moving
            MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            rb.velocity = MoveForce;

            // Steering
            float steerInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);

            // Drag
            MoveForce *= Drag;
            MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);

            // Traction
            Debug.DrawRay(transform.position, MoveForce.normalized * 3);
            Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
            MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;
        }
        else
        {
            // Moving
            MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            if (controls == ControlType.Raw)
            {
                transform.position += MoveForce * Time.deltaTime;
            }

            // Steering
            float steerInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);

            // Drag
            //MoveForce *= Drag;
            MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);

            // Traction
            Debug.DrawRay(transform.position, MoveForce.normalized * 3);
            Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
            MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;

        }
    }
}