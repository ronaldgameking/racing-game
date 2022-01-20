using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct ScoreEntry
{
    public string Name;
    public DateTime Time;
    public DateTimeOffset UnixTime;
    public TimeSpan AchievedTime;

    public ScoreEntry(string initials, DateTime time)
    {
        Name = initials;
        Time = time;
    }
    public ScoreEntry(string initials, DateTime time, TimeSpan span)
    {
        Name = initials;
        Time = time;
        AchievedTime = span;
    }
}
