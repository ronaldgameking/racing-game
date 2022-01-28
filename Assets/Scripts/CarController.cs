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
    [DrawIf(nameof(controls), ControlType.Physics, ComparisonType.Equals, DisablingType.DontDraw)]
    public AnimationCurve SpeedCurve;

    public float MoveSpeed = 50;
    public float MaxSpeed = 15;
    public float Drag = 0.98f;
    public float SteerAngle = 20;
    public float Traction = 1;

    private Vector3 MoveForce;
    private float timeHeldControl = 0f;

    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameManager.countDownDone == true)
        {
            // Moving
            MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.fixedDeltaTime;
            
            rb.velocity = MoveForce;

            // Steering
            float steerInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.fixedDeltaTime);

            // Drag
            MoveForce *= Drag;
            if (Input.GetAxis("Vertical") != 0)
            {
                timeHeldControl += Time.fixedDeltaTime;
            }
            else
            {
                timeHeldControl -= Time.fixedDeltaTime;
                //MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);
            }
            timeHeldControl = Mathf.Clamp(timeHeldControl, -1f, 1f);
            MoveForce = Vector3.ClampMagnitude(MoveForce, SpeedCurve.Evaluate(timeHeldControl) * MaxSpeed);

            // Traction
            Debug.DrawRay(transform.position, MoveForce.normalized * 3);
            Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
            MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.fixedDeltaTime) * MoveForce.magnitude;
        }
        else
        {
            // Moving
            MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.fixedDeltaTime;
            if (controls == ControlType.Raw)
            {
                transform.position += MoveForce * Time.fixedDeltaTime;
            }

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
}