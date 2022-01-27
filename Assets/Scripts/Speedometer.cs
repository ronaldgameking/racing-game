using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour 
{
    private const float Max_Speed_Angle = 0;
    private const float Zero_Speed_Angle = 180;

    private Transform NeedleTranform;

    private float SpeedMax;
    private float Speed;

    private void Awake() {
        NeedleTranform = transform.Find("Needle");

        Speed = 0f;
        SpeedMax = 200f;
    }

    private void Update() 
    {
        HandlePlayerInput();

        NeedleTranform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
    }

    private void HandlePlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            float acceleration = 50f;
            Speed += acceleration * Time.deltaTime;
        }
        else
        {
            float deceleration = 150f;
            Speed -= deceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            float brakeSpeed = 150f;
            Speed -= brakeSpeed * Time.deltaTime;
        }

        Speed = Mathf.Clamp(Speed, 0f, SpeedMax);
    }

    private float GetSpeedRotation() 
    {
        float totalAngleSize = Zero_Speed_Angle - Max_Speed_Angle;

        float speedNormalized = Speed / SpeedMax;

        return Zero_Speed_Angle - speedNormalized * totalAngleSize;
    }
}