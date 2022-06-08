using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class FileSystem
{
    public static void SaveFile(Info inf)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/system.iot";
        FileStream stream = new FileStream(path, FileMode.Create);
        Data dt = new Data(inf);
        formatter.Serialize(stream, dt);
        stream.Close();
    }

    public static Data LoadFile()
    {
        string path = Application.persistentDataPath + "/system.iot";
        Data dt;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            dt = formatter.Deserialize(stream) as Data;
            stream.Close();
            return dt;
        }
        else
        {
            dt = new Data(200, 0);
            return dt;
        }
        
    }
}
