using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
    [SerializeField]
    private Gravity _gravity;

    private bool _jumping;
    private bool _waitingForJump;

    [SerializeField]
    private float _maxSpeed;
    private float _speed;

    [SerializeField]
    private float _jumpDuration;
    [SerializeField]
    private float _jumpTimer;

    public bool Jumping
    {
        get { return _jumping; }
        private set { }
    }

	// Use this for initialization
	void Start () {
        _jumping = false;
        _waitingForJump = true;
        _jumpTimer = 0;
        _speed = _maxSpeed;
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
            if(!Input.GetKey(KeyCode.Space))
            {
                _jumpTimer = _jumpDuration + 1;
            }

            _jumpTimer += Time.deltaTime;
            transform.position += Vector3.up * _speed * Time.deltaTime;

            // Normalise the speed towards the end of the jump
            _speed -= ((_jumpTimer + 2.5f) * (_jumpTimer + 2.5f)) * Time.deltaTime;

            if(_jumpTimer > _jumpDuration)
            {
                _jumping = false;
                _jumpTimer = 0;
                _speed = _maxSpeed;
            }
        }
	}
}
