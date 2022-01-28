using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    [Range(0,4)]
    public float gameSpeed = 1f;

    void Update()
    {
        if (Time.timeScale != gameSpeed)
            Time.timeScale = gameSpeed;
    }
}