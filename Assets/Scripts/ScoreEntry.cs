using System;
using System.Collections;
using System.Collections.Generic;

public struct ScoreEntry
{
    public string Initials;
    public DateTime Time;

    public ScoreEntry(string initials, DateTime time)
    {
        if (initials.Length > 3)
            throw new ArgumentException("Initials cannot be longer than 3");
        Initials = initials;
        Time = time;
    }
}
