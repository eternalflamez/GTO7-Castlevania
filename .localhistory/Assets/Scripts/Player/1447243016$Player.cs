using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Jump))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Gravity))]
public class Player : MonoBehaviour {
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Gravity _gravity;
    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private Jump _jump;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
