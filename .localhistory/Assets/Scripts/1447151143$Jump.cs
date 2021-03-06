﻿using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
    [SerializeField]
    private Gravity _gravity;

    private bool _jumping;
    private bool _waitingForJump;

    [SerializeField]
    private float _maxSpeed;

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
	void Update () 
    {
        // Are we allowed to jump?
        if (_waitingForJump)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_gravity.Falling)
            {
                _jumping = true;
                _waitingForJump = false;
            }
        }
        else if(!_jumping && !_gravity.Falling) // If we are waiting for the jump and are not falling
        {
            // We are allowed to jump again
            _waitingForJump = true;
        }

        if(_jumping)
        {
            _jumpTimer += Time.deltaTime;
            transform.position += Vector3.up * _maxSpeed * Time.deltaTime;

            if(_jumpTimer > _jumpDuration)
            {
                _jumping = false;
                _jumpTimer = 0;
            }
        }
	}
}
