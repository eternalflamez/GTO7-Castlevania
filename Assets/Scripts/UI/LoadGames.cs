using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoadGames : MonoBehaviour {
    [SerializeField]
    private List<Save> saves;

    [SerializeField]
    private bool loader;
    
	// Use this for initialization
	void Start () {
        for (int i = 0; i < saves.Count; i++)
        {
            string name = PlayerPrefs.GetString("save" + i + "name");
            int seed = PlayerPrefs.GetInt("save" + i + "seed");
            float x = PlayerPrefs.GetFloat("save" + i + "x");
            float y = PlayerPrefs.GetFloat("save" + i + "y");

            if (name == "")
            {
                name = "Empty save";
            }

            saves[i].SetSaveValues(i, name, seed, new Vector2(x, y), !loader);

            if (loader)
            {
                if (seed != 0) // What if seed is 0? 1 in million chance tho
                {
                    saves[i].Unlock();
                }
            }
            else
            {
                saves[i].Unlock();
            }
        }
	}
}
