using UnityEngine;
using System.Collections;

public class PlayerHealth : Singleton<PlayerHealth> {
    [SerializeField]
    private MusicPlaylist deathMusic;
    [SerializeField]
    private GameObject deathHolder;
    private int health;
    private float parentWidth;
    private RectTransform rectTransform;

    bool dead = false;

    public int Health
    {
        get { return health; }
    }

    void Start()
    {
        parentWidth = transform.parent.GetComponent<RectTransform>().sizeDelta.x;
        health = 0;
        rectTransform = this.GetComponent<RectTransform>();
        ChangeHealth(PlayerPrefs.GetInt("PlayerHealth")); 
    }

    public void ChangeHealth(float delta)
    {
        if(dead)
        {
            return;
        }

        if(health + delta <= 0)
        {
            deathHolder.SetActive(true);
            dead = true;
            delta = health;
            MusicManager.Instance.ChangePlaylist(deathMusic);
            MusicManager.Instance.Repeat = RepeatMode.None;
            MusicManager.Instance.Shuffle = false;
            
        }

        if (health + delta > 100)
        {
            delta = 100 - health; // So we can't go higher than 100
        }

        Vector2 width = rectTransform.sizeDelta;
        width += new Vector2((delta / 100) * parentWidth, 0);
        rectTransform.sizeDelta = width;
        health += Mathf.RoundToInt(delta);
    }
}