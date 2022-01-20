using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct ScoreEntry
{
    public string Initials;
    public DateTime Time;
    public DateTimeOffset UnixTime;
    public TimeSpan AchievedTime;

    public ScoreEntry(string initials, DateTime time)
    {
        if (initials.Length > 3)
            throw new ArgumentException("Initials cannot be longer than 3");
        Initials = initials;
        Time = time;
    }
    public ScoreEntry(string initials, DateTime time, TimeSpan span)
    {
        if (initials.Length > 3)
            throw new ArgumentException("Initials cannot be longer than 3");
        Initials = initials;
        Time = time;
        AchievedTime = span;
    }
}
