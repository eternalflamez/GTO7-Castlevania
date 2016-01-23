using UnityEngine;
using System.Collections;
using Steamworks;

public class SteamHandler : MonoBehaviour {
    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;
    
	// Use this for initialization
	void Start () {
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);

            m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
    {
        if (pCallback.m_bActive != 0)
        {
            Time.timeScale = 0;
            Debug.Log("Steam Overlay has been activated");
        }
        else
        {
            Time.timeScale = 1;
            Debug.Log("Steam Overlay has been closed");
        }
    }
}
