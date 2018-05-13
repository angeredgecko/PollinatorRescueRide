using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Storage {

    static string path = Path.Combine(Application.persistentDataPath, "stats.gd");

    public static void Save(Stats stats)
    {
        Debug.Log("Called Save");
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(path); //you can call it anything you want
        bf.Serialize(file, stats);
        file.Close();
    }

    public static bool Load(out Stats stats)
    {
        Debug.Log("Called Load.");
        if (File.Exists(Application.persistentDataPath + "/stats.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            stats = (Stats)bf.Deserialize(file);
            file.Close();
            return true;
        }
        stats = new Stats();
        return false;
    }

    public static void ListIndexedDBFiles()
    {
        foreach (string file in System.IO.Directory.GetFiles(Application.persistentDataPath))
        {
            Debug.Log("File: " + file);
        }
        foreach (string folder in System.IO.Directory.GetDirectories(Application.persistentDataPath))
        {
            Debug.Log("Folder: " + folder);
        }
    }
}
