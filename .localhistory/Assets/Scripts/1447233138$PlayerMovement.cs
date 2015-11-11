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
	    if(Input.GetKey(KeyCode.LeftArrow))
        {
            if (!_tapped)
            {
                this.transform.position += Vector3.left * Time.deltaTime * 3;
                _tapped = true;
            }
            else
            {
                if(_doubleTapTimer < _doubleTapWindow)
                { 
                    _tapped = false;
                }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * Time.deltaTime * 3;
        }
	}
}
