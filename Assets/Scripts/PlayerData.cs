using System.Collections.Generic;
using System.Linq;
/// <summary>
/// Player Data class everything should be stored here
/// </summary>
[System.Serializable]
public class PlayerData
{
    public int SaveVersion = 0;
    //WARN: everything must be a poperty
    public List<ScoreEntry> Scores { get; set; }
}
