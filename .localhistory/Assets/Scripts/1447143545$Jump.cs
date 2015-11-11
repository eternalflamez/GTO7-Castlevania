using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
    [SerializeField]
    private AnimationCurve _jumpCurve;

    private float _curvePointer;

    private bool _jumping;
    private bool _waitingForJump;

    private float _gravity;
    private float _startSpeed;

	// Use this for initialization
	void Start () {
        _jumping = false;
        _waitingForJump = true;
        _gravity = 0;
        _curvePointer = 0;
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
            transform.position += Vector3.up * (_startSpeed - _gravity);
        }
	}
}
