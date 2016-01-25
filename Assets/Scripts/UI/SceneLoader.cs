using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void GoToMain()
    {
        MusicManager.Instance.Destroy();

        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
