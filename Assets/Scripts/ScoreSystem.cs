using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public class SomeData
    {
        public int someInt = 100;
    }

    private void Awake()
    {
        SaveSys();
    }

    void SaveSys()
    {
        ScoreSaveSystem.SaveScore(4500);
        ScoreSaveSystem.SaveScore(200);
        ScoreSaveSystem.SaveScore(400);
        ScoreSaveSystem.SaveScore(100);
        ScoreSaveSystem.SaveScore(5200);
        ScoreSaveSystem.Flush();
    }
}