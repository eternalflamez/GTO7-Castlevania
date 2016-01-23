using UnityEngine;
using System.Collections;

public class GamePauser : MonoBehaviour {
    [SerializeField]
    private GameObject optionsPanel;
    private bool paused;

	// Use this for initialization
	void Start () {
        paused = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Time.timeScale = 0;
                paused = true;

                optionsPanel.SetActive(true);
            }
            else
            {
                UnPause();
            }
        }
	}

    public void UnPause()
    {
        Time.timeScale = 1;
        paused = false;

        optionsPanel.SetActive(false);
    }
}
