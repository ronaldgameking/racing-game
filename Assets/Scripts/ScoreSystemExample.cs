using System;
using UnityEngine;

public class ScoreSystemExample : MonoBehaviour
{
    public Logger.DebugLevel LogLevel;
    public bool useLegacy = false;
    public PlayerData Data;

    private void Awake()
    {
        Logger.Level = LogLevel;
        if (useLegacy)
        {
#pragma warning disable CS0612 // Type or member is obsolete
            SaveSystem.SaveScore(int.MaxValue);
            SaveSystem.SaveScore(int.MinValue);
            SaveSystem.SaveScore(int.MaxValue);
            SaveSystem.SaveScore(int.MinValue);
            //SaveSystem.SaveScore(int.MaxValue);
            //SaveSystem.SaveScore(int.MinValue);
            //SaveSystem.SaveScore(int.MaxValue);
            //SaveSystem.SaveScore(int.MinValue);
            SaveSystem.Flush();
            SaveSystem.LoadIO();
            Logger.Log(SaveSystem.ReadInt());
            Logger.Log(SaveSystem.ReadInt());
            Logger.Log(SaveSystem.ReadInt());
            Logger.Log(SaveSystem.ReadInt());
            //SaveSystem.ResetSave();
#pragma warning restore CS0612 // Type or member is obsolete
        }
        else
        {
            SaveGameManagment managment = SaveGameManagment.GetGlobalInstance(true);
            PlayerData pd = new PlayerData();
            pd.Scores = new ScoreEntry[] { new ScoreEntry("rob", TimeExt.UnixEpoch + new TimeSpan(100, 0, 0), new TimeSpan(0, 2, 22)), new ScoreEntry("fbi", DateTime.UtcNow, new TimeSpan(0, 1, 34)) };
            managment.Save(pd);
            if (managment.ExposeMemoryStream)
            {
                byte[] buff = new byte[managment.MemoryStream.Length];
                Logger.LogVerbose(managment.MemoryStream.Length);
                Logger.LogVerbose(managment.MemoryStream.Read(buff, 0, (int)managment.MemoryStream.Length));
                managment.MemoryStream.Position = 0;
            }
            else
            {
                Logger.LogVerbose("Skipping Memory debug");
            }
            managment.Flush();
            //managment.InvalidateCache();
            PlayerData restoredData = managment.Load();
            Data = restoredData;
            Logger.LogVerbose(Data.Scores[0].AchievedOn.ToString());
        }
    }
}