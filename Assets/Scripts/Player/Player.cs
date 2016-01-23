using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Jump))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Gravity))]
public class Player : MonoBehaviour {
    private Gravity _gravity;
    private PlayerMovement _playerMovement;
    private Jump _jump;

	// Use this for initialization
	void Start () {
        _gravity = GetComponent<Gravity>();
        _playerMovement = GetComponent<PlayerMovement>();
        _jump = GetComponent<Jump>();

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
