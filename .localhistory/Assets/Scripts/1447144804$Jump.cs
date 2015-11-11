using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
    private bool _jumping;
    private bool _waitingForJump;

    private float _startSpeed;

	// Use this for initialization
	void Start () {
        _jumping = false;
        _waitingForJump = true;
        _startSpeed = 4;
	}
	
	// Update is called once per frame
	void Update () {
	    if(_waitingForJump && Input.GetKeyDown(KeyCode.Space))
        {
            _jumping = true;
            _waitingForJump = false;
        }

        if(_jumping)
        {
            transform.position += Vector3.up * _startSpeed * Time.deltaTime;
        }
	}
}
