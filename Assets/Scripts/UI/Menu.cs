using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    [SerializeField]
    private GameObject StartPanel;
    [SerializeField]
    private GameObject LoadPanel;
    [SerializeField]
    private GameObject StartSeedPanel;


    public void ShowStartMenu()
    {
        StartPanel.SetActive(true);
        LoadPanel.SetActive(false);
        StartSeedPanel.SetActive(false);
    }

    public void ShowSeedMenu()
    {
        StartPanel.SetActive(false);
        LoadPanel.SetActive(false);
        StartSeedPanel.SetActive(true);
    }

    public void StartGame(InputField field)
    {
        int seed;
        if (int.TryParse(field.text, out seed))
        {
            PlayerPrefs.SetInt("CurrentSeed", seed);
        }
        else if(field.text == "")
        {
            seed = Random.Range(0, 100000);
            PlayerPrefs.SetInt("CurrentSeed", seed);
        }

        PlayerPrefs.SetFloat("PlayerPosX", 5.4f);
        PlayerPrefs.SetFloat("PlayerPosY", 2.7f);

        PlayerPrefs.SetInt("PlayerHealth", 100);

        PlayerPrefs.Save();

        SceneManager.LoadScene(2);
    }

    public void ShowLoadMenu()
    {
        StartPanel.SetActive(false);
        LoadPanel.SetActive(true);
        StartSeedPanel.SetActive(false);
    }

    public void LoadGame()
    {

    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
         // set the PlayMode to stop
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif 
    }
}
