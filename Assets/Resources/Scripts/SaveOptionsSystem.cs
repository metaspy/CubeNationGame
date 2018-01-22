using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveOptionsSystem
{
    public static bool SaveOptions(SaveOptions saveOptions, string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(GetOptionsPath(name), FileMode.Create))
        {
            try
            {
                formatter.Serialize(stream, saveOptions);
            }
            catch (Exception)
            {
                return false;
            }
        }

        return true;
    }

    public static SaveOptions LoadOptions(string name)
    {
        if (!DoesOptionsExist(name))
        {
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(GetOptionsPath(name), FileMode.Open))
        {
            try
            {
                return formatter.Deserialize(stream) as SaveOptions;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public static bool DeleteOptions(string name)
    {
        try
        {
            File.Delete(GetOptionsPath(name));
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public static bool DoesOptionsExist(string name)
    {
        return File.Exists(GetOptionsPath(name));
    }

    private static string GetOptionsPath(string name)
    {
        return Path.Combine(Application.persistentDataPath + "/options/", "Options.sav");
    }
}

[Serializable]
public class MySaveOptions : SaveOptions
{
    public float mainSoundLevel;

    public float backgroundSoundLevel;

    public float musicSoundLevel;

    public float renderDistance;

    public KeyCode firstAbility;
    public KeyCode secondAbility;
    public KeyCode thirdAbility;
    public KeyCode fourthAbility;
    public KeyCode fifthAbility;
    public KeyCode sixthAbility;
    public KeyCode seventhAbility;
}
