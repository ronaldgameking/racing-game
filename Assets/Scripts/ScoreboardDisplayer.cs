using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreboardDisplayer : MonoBehaviour
{
    //Amount of high scores to show
    public int RankingShowAmount = 20;
    public Transform WhereToAttach;
    public Transform LatestContainer;

    public bool UseDebugData = false;
    public bool AppendTest = false;
    public PlayerData Data = null;

    [SerializeField] 
    private GameObject ScoreEntryPrefab;

    private void Awake()
    {
        Logger.Level = Logger.DebugLevel.Verbose;
        SaveGameManagment managment = SaveGameManagment.GetGlobalInstance(true);
        if (UseDebugData)
        {
            PlayerData tmpTestData = new PlayerData();
            tmpTestData.Scores = new List<ScoreEntry>();
            tmpTestData.Scores.Add(new ScoreEntry("Bob", new DateTime(2020,1,19), new TimeSpan(0, 1, 7)));
            tmpTestData.Scores.Add(new ScoreEntry("Jelle", new DateTime(2020,1,18), new TimeSpan(0, 1, 7)));
            tmpTestData.Scores.Add(new ScoreEntry("Alexander", new DateTime(2020, 1, 17), new TimeSpan(0, 0, 57)));
            tmpTestData.Scores.Add(new ScoreEntry("Boaz", new DateTime(2020,1,15), new TimeSpan(0, 1, 9)));
            tmpTestData.Scores.Add(new ScoreEntry("Bob", new DateTime(2020,1,14), new TimeSpan(0, 1, 59)));
            tmpTestData.Scores.Add(new ScoreEntry("Ronald", DateTime.UtcNow, new TimeSpan(0, 1, 7)));
            tmpTestData.Scores.Add(new ScoreEntry("Jimmy", new DateTime(2020, 1, 16), new TimeSpan(0, 1, 40)));
            
            managment.Save(tmpTestData);
            if (AppendTest)
            {
                tmpTestData.Scores.Add(new ScoreEntry("PLgamer2006", new DateTime(2021, 6, 24), new TimeSpan(0, 1, 8)));
                managment.Save(tmpTestData);
            }
        }

        Data = managment.LoadSorted();
        if (Data != null)
        {
            //ScoreEntry[] best = Data.Scores.OrderBy((entry) =>
            //{
            //    return entry.AchievedTime.TotalMilliseconds;
            //}).ToArray();
            ScoreEntry newest = Data.Scores.OrderByDescending((entry) =>
            {
                return entry.AchievedOn.Ticks;
            }).FirstOrDefault();

            for (int i = 0; i < Data.Scores.Count; i++)
            {
                GameObject go = Instantiate(ScoreEntryPrefab, WhereToAttach);
                ScoreEntryInterfaceData interfaceData = go.GetComponent<ScoreEntryInterfaceData>();
                interfaceData.ScoreNameText.text = $"#{i + 1} {Data.Scores[i].Name}";
                interfaceData.ScoreDateText.text = Data.Scores[i].AchievedOn.ToString();
                interfaceData.ScoreTimeText.text = Data.Scores[i].AchievedTime.ToString();
            }
            GameObject latestGo = Instantiate(ScoreEntryPrefab, LatestContainer);
            ScoreEntryInterfaceData latestInterfaceData = latestGo.GetComponent<ScoreEntryInterfaceData>();
            latestInterfaceData.ScoreNameText.text = $"#{newest.Position + 1} {newest.Name}";
            latestInterfaceData.ScoreDateText.text = newest.AchievedOn.ToString();
            latestInterfaceData.ScoreTimeText.text = newest.AchievedTime.ToString();
        }
    }
}
