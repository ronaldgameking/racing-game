using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    static float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string time = string.Format("{0:0}:{1:00}", minutes, seconds);

        TimerText.text = time;
    }
}