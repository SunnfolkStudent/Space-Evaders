using System.IO;
using UnityEngine;

public static class ReadWriteScore
{
    private const string _persistentCopyFileName = "HighScoreCopy.txt";

    public static void CreatePersistentCopy(TextAsset originalTextAsset)
    {
        string persistentCopyPath = Path.Combine(Application.persistentDataPath, _persistentCopyFileName);
        File.WriteAllText(persistentCopyPath, originalTextAsset.text);
        Debug.Log("Persistent copy created successfully.");
    }


    public static int ReadPersistentCopy()
    {
        string persistentCopyPath = Path.Combine(Application.persistentDataPath, _persistentCopyFileName);

        if (File.Exists(persistentCopyPath))
        {
            string content = File.ReadAllText(persistentCopyPath);
            if (int.TryParse(content, out int highScore))
            {
                return highScore;
            }
            else
            {
                Debug.LogWarning("Failed to parse high score from the persistent copy file.");
            }
        }
        else
        {
            Debug.LogWarning("Persistent copy file not found.");
        }

        return 0; // Default value if reading fails
    }

    public static void WritePersistentCopy(int newHighScore)
    {
        string persistentCopyPath = Path.Combine(Application.persistentDataPath, _persistentCopyFileName);

        try
        {
            File.WriteAllText(persistentCopyPath, newHighScore.ToString());
            Debug.Log($"High score written to persistent copy successfully: {newHighScore}");
        }
        catch (IOException e)
        {
            Debug.LogError($"Error writing high score to persistent copy: {e.Message}");
        }
    }
}
