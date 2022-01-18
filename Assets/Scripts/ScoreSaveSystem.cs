using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ScoreSaveSystem
{
    private static MemoryStream ms;
    private static bool m_initiated = false;
    private static string path = Path.Combine(Application.persistentDataPath, "sv.bin");

    public static void SaveScore(int a)
    {
        if (!m_initiated)
        {
            Logger.LogWarning("Not initiated yet, initializing...");
            ms = new MemoryStream();
            m_initiated = true;
        }
        byte[] saveBytes = Encoding.UTF8.GetBytes(a.ToString());
        ms.Write(saveBytes, 0, saveBytes.Length);
    }
    public static void Save<T>(T a)
    {
        if (!m_initiated)
        {
            Logger.LogWarning("Not initiated yet, initializing...");
            ms = new MemoryStream();
            m_initiated = true;
        }
        byte[] saveBytes = Encoding.UTF8.GetBytes(a.ToString());
        ms.Write(saveBytes, 0, saveBytes.Length);
    }

    /// <summary>
    /// Writes the current cached save to the disk
    /// </summary>
    public static void Flush()
    {
        if (ms == null)
        {
            Logger.LogWarning("Nothing to flush from the buffer!");
            return;
        }
        Logger.Log("Writing file...");
        
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            using (BinaryWriter br = new BinaryWriter(fs))
            {
                
            }
        }
        System.Diagnostics.Process.Start(Application.persistentDataPath);
    }
}
