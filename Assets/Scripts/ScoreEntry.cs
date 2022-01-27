using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct ScoreEntry
{
    public string Name;
    public bool Latest;
    public int Position;
    public DateTime AchievedOn;
    public DateTimeOffset UnixTime;
    public TimeSpan AchievedTime;

    public ScoreEntry(string initials, DateTime time, TimeSpan span)
    {
        Name = initials;
        AchievedOn = time;
        AchievedTime = span;
        Latest = false;
        Position = 0;
    }

    /// <summary>
    /// The constuctor for lazy people
    /// </summary>
    /// <param name="initials"></param>
    /// <param name="time"></param>
    /// <param name="span"></param>
    public ScoreEntry(string initials, long span)
    {
        Name = initials;
        AchievedOn = DateTime.UtcNow;
        AchievedTime = new TimeSpan(span);
        Latest = false;
        Position = 0;
    }
    //public ScoreEntry()
    //{
    //    Name = "";
    //    AchievedOn = DateTime.UtcNow;
    //    AchievedTime = new TimeSpan(0);
    //    Latest = false;
    //    Position = 0;
    //}
}
