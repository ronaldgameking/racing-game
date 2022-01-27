using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotater : MonoBehaviour
{
    public Vector3 Axis = Vector3.up;

    [Min(0.001f)]
    public float Speed = 2f;

    private void Update()
    {
        transform.Rotate(Axis * Speed * Time.deltaTime);
    }
}
