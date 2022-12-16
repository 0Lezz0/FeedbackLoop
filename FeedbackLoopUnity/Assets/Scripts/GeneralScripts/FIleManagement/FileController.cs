using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class FileController
{
    private static string fileName = "/loop.data";

    public static void SaveCurrentLoop(LoopConfig loopConfig)
    {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + fileName;
        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, loopConfig);

        stream.Close();
    }

    public static LoopConfig LoadData()
    {
        try
        {
            string path = Application.persistentDataPath + fileName;

            Debug.Log(path);
            if (File.Exists(path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter(); 
                FileStream fileStream = new FileStream(path, FileMode.Open);
                LoopConfig loop = binaryFormatter.Deserialize(fileStream) as LoopConfig; 
                fileStream.Close(); 
                return loop; 
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            return null;
        }

    }


}

[System.Serializable]
public class LoopConfig
{
    public int currentLoop;

    public LoopConfig(int currentLoop)
    {
        this.currentLoop = currentLoop;
    }
}
