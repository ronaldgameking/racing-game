using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    private GameManager gameManager;
    GameObject countdown;

    
    public void SetCountDownNow()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.countDownDone = true;
    }
}
