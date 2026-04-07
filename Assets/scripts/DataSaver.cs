using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.UI;
using System.Text;
using TMPro;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class DataSaver : MonoBehaviour
{

    public TextMeshProUGUI positionSavedShow; // optional: show current path
    private GameData gameDataCurrent;

    void Start()
    {
        // Get game data from your recording script
        gameDataCurrent = transform.GetComponent<RecordDataExample>().gameData;

        // Show the base save path
        positionSavedShow.text = GetSavePath();

#if UNITY_ANDROID
        // Request write permission if needed (older Android)
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
#endif
    }


    public void SaveData()
    {
        SaveJson(gameDataCurrent);
        SaveCsv(gameDataCurrent);
        SaveXml(gameDataCurrent);
        SaveYaml(gameDataCurrent);
        Debug.Log("All data saved!");
    }


    public void MoveToDownloads()
    {
        string savedFolder = Path.Combine(GetSavePath(), "SavedData");
        if (!Directory.Exists(savedFolder))
        {
            Debug.LogWarning("SavedData folder does not exist!");
            return;
        }

        string downloadsPath;

        if (Application.platform == RuntimePlatform.Android)
        {
            downloadsPath = "/storage/emulated/0/Download/SavedData"; // Android Downloads
        }
        else
        {
            downloadsPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "SavedData");
        }

        if (!Directory.Exists(downloadsPath))
            Directory.CreateDirectory(downloadsPath);

        foreach (string file in Directory.GetFiles(savedFolder))
        {
            string destFile = Path.Combine(downloadsPath, Path.GetFileName(file));
            File.Copy(file, destFile, true);
            Debug.Log("Copied to Downloads: " + destFile);
        }

        Debug.Log("All files moved to Downloads folder!");
    }

    

    void SaveJson(GameData gameData)
    {
        string folder = Path.Combine(GetSavePath(), "SavedData");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string pathFull = Path.Combine(folder, "gameData.json");
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(pathFull, json);
        Debug.Log("Saved JSON: " + pathFull);
    }

    void SaveCsv(GameData gameData)
    {
        string folder = Path.Combine(GetSavePath(), "SavedData");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string pathFull = Path.Combine(folder, "gameData.csv");
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("time,posX,posY,score");

        foreach (var entry in gameData.entries)
            sb.AppendLine($"{entry.time},{entry.posX},{entry.posY},{entry.score}");

        File.WriteAllText(pathFull, sb.ToString());
        Debug.Log("Saved CSV: " + pathFull);
    }

    void SaveXml(GameData gameData)
    {
        string folder = Path.Combine(GetSavePath(), "SavedData");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string pathFull = Path.Combine(folder, "gameData.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));

        using (FileStream stream = new FileStream(pathFull, FileMode.Create))
        {
            serializer.Serialize(stream, gameData);
        }

        Debug.Log("Saved XML: " + pathFull);
    }


    void SaveYaml(GameData gameData)
    {
        string folder = Path.Combine(GetSavePath(), "SavedData");

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string pathFull = Path.Combine(folder, "gameData.yaml");

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("entries:");

        foreach (var entry in gameData.entries)
        {
            sb.AppendLine("  - time: " + entry.time);
            sb.AppendLine("    posX: " + entry.posX);
            sb.AppendLine("    posY: " + entry.posY);
            sb.AppendLine("    score: " + entry.score);
        }

        File.WriteAllText(pathFull, sb.ToString());

        Debug.Log("Saved YAML: " + pathFull);
    }





    private string GetSavePath()
    {
        if (Application.isEditor)
            return Application.dataPath; // Editor folder
        else
            return Application.persistentDataPath; // Android, iOS, Standalone builds
    }
}