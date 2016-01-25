using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class Save : MonoBehaviour {
    private string savename;
    private int seedValue;
    private Vector2 playerPosition;
    private int health;
	
    [SerializeField]
    private Text savenameText;
    [SerializeField]
    private Text seedValueText;
    [SerializeField]
    private Button button;
    [SerializeField]
    private InputField input;

    private bool saveClicked;

    [SerializeField]
    private GameObject editPanel;
    [SerializeField]
    private GameObject basePanel;

    private int saveNumber;

    public void SetSaveValues(int saveNumber, string name, int seed, Vector2 position, bool save, int health)
    {
        this.savename = name;
        this.seedValue = seed;
        this.playerPosition = position;
        this.saveNumber = saveNumber;
        this.health = health;

        savenameText.text = savename;
        seedValueText.text = seedValue.ToString();

        if (!save)
        {
            button.onClick.AddListener(LoadGame);
        }
        else
        {
            input.text = savename;
            button.onClick.AddListener(SaveGame);
        }

        saveClicked = false;
    }

    public void Unlock()
    {
        button.interactable = true;
    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("CurrentSeed", seedValue);
        PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
        PlayerPrefs.SetInt("PlayerHealth", health);

        PlayerPrefs.Save();

        SceneManager.LoadScene(2);
    }

    public void SaveGame()
    {
        if (saveClicked)
        {
            if(input.text == "")
            {
                input.text = " ";
            }

            int seed = FindObjectOfType<Generator>().Seed;

            PlayerPrefs.SetString("save" + saveNumber + "name", input.text);
            PlayerPrefs.SetInt("save" + saveNumber + "seed", seed);
            Vector2 position = FindObjectOfType<PlatformerCharacter2D>().transform.position;
            PlayerPrefs.SetFloat("save" + saveNumber + "x", position.x);
            PlayerPrefs.SetFloat("save" + saveNumber + "y", position.y);
            PlayerPrefs.SetInt("save" + saveNumber + "health", PlayerHealth.instance.Health);

            savenameText.text = input.text;
            seedValueText.text = seed.ToString();
            editPanel.SetActive(false);
            basePanel.SetActive(true);

            input.text = input.text;

            saveClicked = false;
            PlayerPrefs.Save();
        }
        else
        {
            saveClicked = true;
            editPanel.SetActive(true);
            basePanel.SetActive(false);
        }
    }
}
