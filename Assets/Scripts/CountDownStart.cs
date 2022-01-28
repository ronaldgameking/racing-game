using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownStart : MonoBehaviour
{
    public GameObject countdown;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            countdown.active = true;
        }
    }
}
