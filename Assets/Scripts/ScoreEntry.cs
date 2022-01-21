using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct ScoreEntry
{
    public string Name;
    public DateTime AchievedOn;
    public DateTimeOffset UnixTime;
    public TimeSpan AchievedTime;

    public ScoreEntry(string initials, DateTime time, TimeSpan span)
    {
        Name = initials;
        AchievedOn = time;
        AchievedTime = span;
    }
}
