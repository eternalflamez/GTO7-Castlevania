﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Jump))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Gravity))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour {
    private Animator _animator;
    private Gravity _gravity;
    private PlayerMovement _playerMovement;
    private Jump _jump;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
        _gravity = GetComponent<Gravity>();
        _playerMovement = GetComponent<PlayerMovement>();
        _jump = GetComponent<Jump>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void FixedUpdate()
    {
        _animator.SetBool("Ground", !_jump.Jumping && !_gravity.Falling);

        float hSpeed = 0;

        if(_playerMovement.Moving)
        {
            if(_playerMovement.Running)
            {
                hSpeed = _playerMovement.RunSpeed;
            }
            else
            {
                hSpeed = _playerMovement.WalkSpeed;
            }
        }

        _animator.SetFloat("Speed", 0);
    }
}
