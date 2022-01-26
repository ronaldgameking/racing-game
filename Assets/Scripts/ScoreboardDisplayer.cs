using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreboardDisplayer : MonoBehaviour
{
    //Amount of high scores to show
    public int RankingShowAmount = 20;
    public Transform WhereToAttach;

    [SerializeField] 
    private GameObject ScoreEntryPrefab;

    private void Awake()
    {
        PlayerData tmpTestData = new PlayerData();
        tmpTestData.Scores = new ScoreEntry[] {
            new ScoreEntry("Bob", System.DateTime.UtcNow, new System.TimeSpan(0, 1, 7)),
            new ScoreEntry("Jimmy", System.DateTime.UtcNow, new System.TimeSpan(0, 1, 20))
        };
        SaveGameManagment.GetGlobalInstance(true).Save(tmpTestData);
        PlayerData data = SaveGameManagment.GetGlobalInstance().Load();
        if (data != null)
        {
            ScoreEntry[] best = data.Scores.OrderBy((entry) =>
            {
                return entry.AchievedTime.TotalMilliseconds;
            }).ToArray();
            for (int i = 0; i < data.Scores.Length; i++)
            {
                GameObject go = Instantiate(ScoreEntryPrefab, WhereToAttach);
                ScoreEntryInterfaceData interfaceData = go.GetComponent<ScoreEntryInterfaceData>();
                //interfaceData.ScoreNameText.text = $"#{i + 1} {data.Scores[i].Name}";
                //interfaceData.ScoreDateText.text = data.Scores[i].AchievedOn.ToString();
                //interfaceData.ScoreTimeText.text = data.Scores[i].AchievedTime.ToString();
                interfaceData.ScoreNameText.text = $"#{i + 1} {best[i].Name}";
                interfaceData.ScoreDateText.text = best[i].AchievedOn.ToString();
                interfaceData.ScoreTimeText.text = best[i].AchievedTime.ToString();
            }
        }
    }
}
