using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string saveFilePath;
    public static SaveLoadManager Instance { get; private set;}

    private void Awake()
    {
        // DeleteAllApplicationFiles();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        saveFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");
    }
    public void SaveInventoryState(InventoryData inventoryState)
    {
        string json = JsonUtility.ToJson(inventoryState, true);
        Debug.Log("" + json);
        File.WriteAllText(saveFilePath, json);
        Debug.Log($"Player state saved to {saveFilePath}");
    }
    public InventoryData LoadInventoryState()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            InventoryData state = JsonUtility.FromJson<InventoryData>(json);
            Debug.Log("Player state loaded");
            return state;
        }
        else
        {
            Debug.LogWarning("Save file not found");
            return new InventoryData();
        }
    }


    public void DeleteAllApplicationFiles()
    {
        string path = Application.persistentDataPath;

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
        else
        {
            Debug.LogWarning("No files found to delete.");
        }
        Directory.CreateDirectory(path);
    }
}
