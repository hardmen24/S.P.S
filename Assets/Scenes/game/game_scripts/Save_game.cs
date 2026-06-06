using System;
using System.IO;
using UnityEngine;

public class Save_game : MonoBehaviour
{
    
    // Generates a secure cross-platform path for the save file
    private static string GetSavePath(string filename)
    {
        return Path.Combine(Application.persistentDataPath, filename + ".json");
    }

    /// <summary>
    /// Saves any serializable object to a JSON file.
    /// </summary>
    public static bool SaveData<T>(string filename, T data)
    {
        string path = GetSavePath(filename);

        try
        {
            // Convert the data object to a JSON string
            string json = JsonUtility.ToJson(data, true);

            // Write the text to the platform-specific path
            File.WriteAllText(path, json);
            Debug.Log($"[SaveSystem] Data successfully saved to: {path}");
            return true;
        }
        catch (DirectoryNotFoundException dirEx)
        {
            Debug.LogError($"[SaveSystem] Save failed. Directory not found: {dirEx.Message}");
        }
        catch (UnauthorizedAccessException unauthEx)
        {
            Debug.LogError($"[SaveSystem] Save failed. Directory permissions denied: {unauthEx.Message}");
        }
        catch (IOException ioEx)
        {
            Debug.LogError($"[SaveSystem] Save failed. Disk might be full or file is locked: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[SaveSystem] An unexpected error occurred while saving: {ex.Message}");
        }

        return false;
    }

    /// <summary>
    /// Loads a JSON file and populates the specified object type.
    /// </summary>
    public static T LoadData<T>(string filename) where T : new()
    {
        string path = GetSavePath(filename);

        if (!File.Exists(path))
        {
            Debug.LogWarning($"[SaveSystem] Save file not found at {path}. Returning default/new object.");
            return new T(); // Returns an empty data structure if no file exists yet
        }

        try
        {
            // Read the JSON string from the file
            string json = File.ReadAllText(path);

            // Convert JSON back into the C# object
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log($"[SaveSystem] Data successfully loaded from: {path}");
            return data;
        }
        catch (FileNotFoundException fileEx)
        {
            Debug.LogError($"[SaveSystem] Load failed. File disappeared: {fileEx.Message}");
        }
        catch (IOException ioEx)
        {
            Debug.LogError($"[SaveSystem] Load failed. File could be corrupted or locked: {ioEx.Message}");
        }
        catch (ArgumentException argEx)
        {
            Debug.LogError($"[SaveSystem] Load failed. JSON data is invalid/corrupted: {argEx.Message}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[SaveSystem] An unexpected error occurred while loading: {ex.Message}");
        }

        return new T(); // Return a fresh object if loading completely fails
    }

}
