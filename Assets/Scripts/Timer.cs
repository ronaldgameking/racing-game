using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    public bool UseHighPrecision;

    private float timer;
    private Stopwatch watch;

    private void Awake()
    {
        watch = new Stopwatch();
        watch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UseHighPrecision)
        {
            timer += Time.deltaTime;

            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);
            string time = string.Format("{0:0}:{1:00}", minutes, seconds);

            TimerText.text = time;
        }
        else
        {
            int minutes = watch.Elapsed.Minutes;
            int seconds = watch.Elapsed.Seconds;
            int miliseconds = watch.Elapsed.Milliseconds;

            string time = $"{minutes:0}:{seconds:00}.{miliseconds:000}";

            TimerText.text = time;
        }
    }
}