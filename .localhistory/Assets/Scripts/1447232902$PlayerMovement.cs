using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float _doubleTapWindow;
    private float _doubleTapTimer;
    private bool _tapped;

	// Use this for initialization
	void Start () {
        _doubleTapTimer = 0;
        _tapped = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!_tapped)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.position += Vector3.left * Time.deltaTime * 3;
                _tapped = true;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.position += Vector3.right * Time.deltaTime * 3;
            }
        }
	}
}
