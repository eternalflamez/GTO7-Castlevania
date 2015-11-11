using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
    private bool _jumping;
    private bool _waitingForJump;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _jumpDuration;
    private float _jumpTimer;

	// Use this for initialization
	void Start () {
        _jumping = false;
        _waitingForJump = true;
        _jumpTimer = 0;
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
            transform.position += Vector3.up * _speed * Time.deltaTime;
        }
	}
}
