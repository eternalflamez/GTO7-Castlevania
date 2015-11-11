using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
    [SerializeField]
    private AnimationCurve _jumpCurve;

    private float _curvePointer;
    private bool _jumping;
    private bool _waitingForJump;

	// Use this for initialization
	void Start () {
        _jumping = false;
        _waitingForJump = true;
        _curvePointer = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if(_waitingForJump && Input.GetKeyDown(KeyCode.Space))
        {
            _jumping = true;
        }

        if(_jumping)
        {

        }
	}
}
