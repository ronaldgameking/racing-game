using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LapCounter : MonoBehaviour
{
    private bool finished = false;
    private float lapCount;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TMP_InputField inputField;

    private Timer timer;

    private void Start()
    {
        inputField.gameObject.SetActive(false);
        inputField.enabled = false;
    }
    private void Update()
    {
        Debug.Log(lapCount);
        text.text = lapCount.ToString() + "/3";        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {            
            lapCount = lapCount + 1; 
            
            if(lapCount > 3)
            {
                inputField.gameObject.SetActive(true);
                inputField.enabled = true;
                long time = timer.EndRace();



                Debug.Log("Finish");
                finished = true;
                lapCount = 3;
            }            
        }
    }
}
