using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Load_saves : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public UI_manager_main uiManager;

    void Start()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.json");

        foreach (string file in files)
        {
            string saveName = Path.GetFileNameWithoutExtension(file);

            GameObject btn = Instantiate(buttonPrefab, buttonContainer);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = saveName;

            string capturedName = saveName; // capture for lambda
            btn.GetComponent<Button>().onClick.AddListener(() => 
            {
                uiManager.OnLoadPress(capturedName);
            });
        }
    }
}
