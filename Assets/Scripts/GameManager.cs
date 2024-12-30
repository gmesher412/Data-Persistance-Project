using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string nameText;
    public MainManager Manager;
    public int highScore;
    public string highScorePlayerName;
    
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetNameText(string playerName)
    {
        nameText = playerName;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    { 
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit(); // original code to quit Unity player
    #endif
    }

        [System.Serializable]
    class SaveData
    {
        public string end_PlayerName;
        public int end_Score;
    }

    public void SaveScoreAndName()
    {
         SaveData data = new SaveData
            {
                end_PlayerName = Instance.nameText,
                end_Score = Manager.m_Points
            };

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        Debug.Log("Loading score from: " + path);
        if (File.Exists(path))
        {
            Debug.Log("File exists.");
            string json = File.ReadAllText(path);
            Debug.Log("File content: " + json);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if(data != null)
            {
                highScore = data.end_Score;
                highScorePlayerName = data.end_PlayerName;
                Debug.Log("Score loaded successfully.");
            }
            else
            {
                Debug.LogError("Failed to deserialize JSON.");
            }
        }
        else
        {
            Debug.LogError("Save file not found.");
        }
    }
}
