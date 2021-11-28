using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace ANormalGame.IO
{
    public static class Saver
    {
        public const string Format = "ang";

        public static void Serialize(Saveable saveable)
        {
            string Path = FormatPath(saveable);
            FileStream stream = new FileStream(Path, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, saveable);
            stream.Close();
        }

        public static object Load(Saveable saveable)
        {
            string Path = FormatPath(saveable);
            if (!File.Exists(Path))
            {
                Debug.LogError("Invalid Path!");
                return null;
            }

            FileStream stream = new FileStream(Path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            object LoadedData = formatter.Deserialize(stream);
            stream.Close();
            return LoadedData;
        }

        static string FormatPath(Saveable File)
        {
            return Application.persistentDataPath + "/" + File.FileName + "." + Format;
        }
    }
}