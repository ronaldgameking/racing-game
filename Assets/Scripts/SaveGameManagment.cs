using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Linq;

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
    /// Gets a global <see cref="SaveGameManagment"/> to workwith. Either with or without auto-saving
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
    /// <summary>
    /// Gets a unsafe global <see cref="SaveGameManagment"/> exposing the memory stream and disabling auto saving. THIS WILL DESTROY THE PREVIOUS INSTANCE
    /// </summary>
    /// <returns></returns>
    public static SaveGameManagment GetUnsafeGlobalInstance()
    {
        m_instance = null;
        m_instance = new SaveGameManagment(true, false);
        
        return m_instance;
    }
    /// <summary>
    /// Gets a unsafe global <see cref="SaveGameManagment"/> exposing the memory stream and disabling auto saving. Set keepData to true to transfer the data to the new instance
    /// <para> THIS WILL DESTROY THE PREVIOUS INSTANCE</para>
    /// </summary>
    /// <returns></returns>
    public static SaveGameManagment GetUnsafeGlobalInstance(bool keepData)
    {
        MemoryStream ms = m_instance.m_memoryStream;
        bool cache = m_instance.HasCache;
        m_instance = null;
        m_instance = new SaveGameManagment(true, false);
        m_instance.m_memoryStream = ms;
        m_instance.ExposeMemoryStream = true;
        
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

        binaryWriter.Write(pd.Scores.Length);
        Logger.LogVerbose("Written amount of scores");
        for (int i = 0; i < pd.Scores.Length; i++)
        {
            binaryWriter.Write(pd.Scores[i].Name);
            binaryWriter.Write((pd.Scores[i].AchievedOn - TimeExt.UnixEpoch).TotalSeconds);
            binaryWriter.Write(pd.Scores[i].AchievedTime.TotalSeconds);
        }
        m_memoryStream.Position = 0;
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
            long backupPos = m_memoryStream.Position;
            m_memoryStream.Position = 0;
            fs.Position = 0;
            m_memoryStream.CopyTo(fs);
            m_memoryStream.Position = backupPos;
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
            }
            m_memoryStream.Position = 0;
        }
        BinaryReader binaryReader = new BinaryReader(m_memoryStream, Encoding.UTF8, true);
        PlayerData pd = new PlayerData();
        int savedScores = binaryReader.ReadInt32();
        pd.Scores = new ScoreEntry[savedScores];
        for (int i = 0; i < pd.Scores.Length; i++)
        {
            Logger.LogVerbose("Loading ScoreEntry [" + i + "...");
            ScoreEntry tempScore = new ScoreEntry();

            string outName = binaryReader.ReadString();
            tempScore.Name = outName;
            TimeSpan epockAchievedOffset = TimeSpan.FromSeconds(binaryReader.ReadDouble());
            DateTime aquireDate = TimeExt.UnixEpoch + epockAchievedOffset;
            tempScore.AchievedOn = aquireDate;
            TimeSpan achievedTime = TimeSpan.FromSeconds(binaryReader.ReadDouble());
            tempScore.AchievedTime = achievedTime;

            Logger.LogVerbose($"Entry {{ id: {i}, Initials: {tempScore.Name}, Date achieved on: {tempScore.AchievedOn} ,Achieved time: {tempScore.AchievedTime}}}");
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