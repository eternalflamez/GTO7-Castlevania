using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {
	// Use this for initialization
	void Start () {
        SceneManager.LoadSceneAsync(1);
	}
}
