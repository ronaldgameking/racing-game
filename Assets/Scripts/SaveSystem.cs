using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using UnityEngine;

[Obsolete]
public static class SaveSystem
{
    private static MemoryStream ms = new MemoryStream();
    private static BinaryWriter bw = new BinaryWriter(ms, Encoding.UTF8, true);
    private static BinaryReader br = new BinaryReader(ms, Encoding.UTF8, true);

    private static string path = Path.Combine(Application.persistentDataPath, "sv.bin");

    private static void PrintMsDebug()
    {
        Logger.Log("Memory stream stat Pos: " + ms.Position + " Lenght: " + ms.Length);
    }
    private static void PrintMsDebug(string caller)
    {
        Logger.Log("Memory stream stat (" + caller + ") Pos: " + ms.Position + " Lenght: " + ms.Length);
    }

    public static void SaveScore(int a)
    {
        
        bw.Write(a);
    }

    public static void Flush()
    {
        if (ms == null)
        {
            Logger.LogWarning("Nothing to flush from the buffer!");
            return;
        }
        Logger.Log("Writing file...");

        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
        {
            using (BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8, true))
            {
                //using BinaryReader br = new BinaryReader(ms, Encoding.UTF8, true);

                ms.Position = 0;
                while (ms.Length != ms.Position)
                {
                    PrintMsDebug("Write0");
                    int numWr = br.ReadInt32();
                    Logger.LogVerbose("Writing " + numWr + " ...");
                    bw.Write(br.ReadInt32());
                }
            }
        }
        System.Diagnostics.Process.Start(Application.persistentDataPath);
    }
    public static void LoadIO()
    {
        Logger.LogVerbose("Attempting to load file");
        if (!File.Exists(path))
        {
            Logger.LogWarning("File does not exists, nothing to load");
            return;
        }
        byte[] fileBytes = File.ReadAllBytes(path);
        ms.Write(fileBytes, 0, fileBytes.Length);
        ms.Position = 0;
        //using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        //{
        //    ms.Position = 0;
        //    fs.Position = 0;
        //    fs.CopyTo(ms);
        //    //using (BinaryReader br = new BinaryReader(fs, Encoding.UTF8, false))
        //    //{
        //        //BinaryWriter bw = new BinaryWriter(ms, Encoding.UTF8, true);

        //        //while (br.PeekChar() != -1)
        //        //{
        //        //    Logger.Log("Before" + br.PeekChar());
        //        //    bw.Write(br.ReadInt32());
        //        //    Logger.Log("After" + br.PeekChar());
        //        //}
        //        //bw.Dispose();
        //    //}
        //}
    }

    public static int ReadInt()
    {
        PrintMsDebug();
        int readValue = br.ReadInt32();
        Logger.Log(readValue);
        return readValue;
    }
    public static void ResetSave()
    {
        if (!File.Exists(path))
        {
            Logger.Log("File does not exists, nothing to load");
            return;
        }
        File.Delete(path);
    }
}
