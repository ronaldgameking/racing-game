using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Linq;

/// <saummary>
/// Deprecated, use <see cref="SaveSystem"/> This was mean to be a cleaner alternative to <see cref="SaveSystem"/>
/// </summary>

public class SaveGameManagment
{
    /// <summary>
    /// The publicly exposed memory stream, returns null if not exposed
    /// </summary>
    public MemoryStream MemoryStream
    {
        get
        {
            if (!ExposeMemoryStream) return null;
            return m_memoryStream;
        }
    }

    /// <summary>
    /// Is the MemoryStream exposed, exposing the stream could impair security or allow externals to modify memory stream
    /// </summary>
    public bool ExposeMemoryStream { get; private set; } = false;
    public bool AutoSave { get; private set; } = false;
    public bool HasCache { get; private set; } = false;

    private static string path = Path.Combine(UnityEngine.Application.persistentDataPath, "sv.bin");
    private static SaveGameManagment m_instance;

    private MemoryStream m_memoryStream = new MemoryStream();

    public SaveGameManagment() { }
    /// <summary>
    /// Create a Save Game Manager with control if MemoryStream is exposed
    /// </summary>
    /// <param name="exposeStream">WARN: setting this to <code>true</code> will expose the memeory stream which may impair security</param>
    public SaveGameManagment(bool exposeStream)
    {
        ExposeMemoryStream = exposeStream;
    }
    /// <summary>
    /// Create a Save Game Manager with control if MemoryStream is exposed
    /// </summary>
    /// <param name="exposeStream">WARN: setting this to <code>true</code> will expose the memeory stream which may impair security</param>
    public SaveGameManagment(bool exposeStream, bool autoSaveDisk)
    {
        ExposeMemoryStream = exposeStream;
        AutoSave = autoSaveDisk;
    }

    /// <summary>
    /// Gets a global <see cref="SaveGameManagment"/> to work with
    /// </summary>
    /// <returns></returns>
    public static SaveGameManagment GetGlobalInstance()
    {
        if (m_instance == null)
        {
            m_instance = new SaveGameManagment(false, false);
        }
        return m_instance;
    }
    /// <summary>
    /// Gets a global <see cref="SaveGameManagment"/> to work with withj or without auto-saving
    /// </summary>
    /// <returns></returns>
    public static SaveGameManagment GetGlobalInstance(bool saving)
    {
        if (m_instance == null)
        {
            m_instance = new SaveGameManagment(false, saving);
        }
        return m_instance;
    }
    public static void ResetIntance()
    {
        m_instance = null;
    }

    /// <summary>
    /// Save the PlayerData in a temporaly memory stream
    /// </summary>
    /// <param name="pd"></param>
    public void Save(PlayerData pd)
    {
        using BinaryWriter binaryWriter = new BinaryWriter(m_memoryStream, Encoding.UTF8, true);
        //pd.Scores.All(entry => {
        //    binaryWriter.Write(entry.Initials.Length);
        //    binaryWriter.Write(entry.Initials);
        //    binaryWriter.Write(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        //    return true; });
        binaryWriter.Write(pd.Scores.Length);
        for (int i = 0; i < pd.Scores.Length; i++)
        {
            Logger.Log("Looping " + i);
            binaryWriter.Write(pd.Scores[i].Initials);
            binaryWriter.Write('\0');
            binaryWriter.Write((pd.Scores[i].Time - TimeExt.UnixEpoch).TotalSeconds);
            binaryWriter.Write('\0');
        }
        if (AutoSave)
            Flush();
    }
    /// <summary>
    /// Saves the memory saves Player Data to Disk (must be called to manually if <see cref="AutoSave"/> is false)
    /// </summary>
    public void Flush()
    {
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
        {
            m_memoryStream.Position = 0;
            m_memoryStream.CopyTo(fs);
            HasCache = true;
        }
    }
    /// <summary>
    /// Loads player data from cache else it reds from file;
    /// </summary>
    /// <returns></returns>
    public PlayerData Load()
    {
        if (!HasCache)
        {
            if (!File.Exists(path))
                return null;

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                m_memoryStream.Position = 0;
                fs.CopyTo(m_memoryStream);
                m_memoryStream.Position = 0;
            }
        }
        BinaryReader binaryReader = new BinaryReader(m_memoryStream, Encoding.UTF8, true);
        PlayerData pd = new PlayerData();
        int savedScores = binaryReader.ReadInt32();
        pd.Scores = new ScoreEntry[10];
        for (int i = 0; i < pd.Scores.Length; i++)
        {
            Logger.LogVerbose("Creating ScoreEntry [" + i + "...");
            ScoreEntry tempScore = new ScoreEntry();
            string outInitials = "";
            binaryReader.ReadChars(3).All(readChar =>
            {
                outInitials += readChar;
                return true;
            }
            );
            tempScore.Initials = outInitials;
            tempScore.Time = DateTime.UtcNow;
            Logger.LogVerbose("Entry {" + tempScore.Initials + ", " + tempScore.Time.ToString() + "}");
            pd.Scores[i] = tempScore;
        }
        return pd;
    }
    /// <summary>
    /// Invalidate cache to force loading from disk
    /// </summary>
    public void InvalidateCache()
    {
        HasCache = false;
    }
}