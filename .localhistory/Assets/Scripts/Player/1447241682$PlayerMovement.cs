using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float _runSpeed;
    [SerializeField]
    private float _walkSpeed;
    [SerializeField]
    private float _doubleTapWindow;

    [SerializeField]
    private KeyCode _leftButton;
    [SerializeField]
    private KeyCode _rightButton;
    [SerializeField]
    private KeyCode _jumpButton;

    [SerializeField]
    private Jump _jump;

    private float _doubleTapTimer;
    private bool _tapped;
    private bool _running;

	// Use this for initialization
	void Start () {
        _doubleTapTimer = 0;
        _tapped = false;
        _running = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        #region jump
        if (Tapped(KeyCode.Space))
        {
            _jump.DoJump();
        }
        #endregion

        #region move
        if (_tapped)
        {
            _doubleTapTimer += Time.deltaTime;

            if(_doubleTapTimer > _doubleTapWindow)
            {
                _doubleTapTimer = 0;
                _tapped = false;
            }
        }

        if(Tapped(_leftButton) || Tapped(_rightButton))
        {
            if(_tapped)
            {
                // Run
                _running = true;
            }

            _tapped = true;
        }

        Vector3 speed = new Vector3();

        if(Pressed(_leftButton))
        {
            speed = Vector3.left;
        }
        else if(Pressed(_rightButton))
        {
            speed = Vector3.right;
        }
        else
        {
            _running = false;
            return;
        }

        Vector3 direction = speed;

        if(_running)
        {
            speed *= _runSpeed;
        }
        else
        {
            speed *= _walkSpeed;
        }

        speed *= Time.deltaTime;

        // Raycast to see if we are not moving through walls
        float width = (transform.lossyScale.x / 2);
        Vector3 raycastStart = transform.position + (direction * width);

        Debug.DrawRay(raycastStart, direction);
        RaycastHit2D hit = Physics2D.Raycast(raycastStart, direction, speed.x);

        if (hit.collider != null)
        {
            _jumpTimer = _jumpDuration + 1;
        }

        this.transform.position += speed;
        #endregion
    }

    private bool Pressed(KeyCode button)
    {
        return Input.GetKey(button);
    }

    private bool Tapped(KeyCode button)
    {
        return Input.GetKeyDown(button);
    }
}
