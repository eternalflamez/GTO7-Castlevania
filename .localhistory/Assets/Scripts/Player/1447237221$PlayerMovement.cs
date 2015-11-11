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
    private KeyCode _left;
    [SerializeField]
    private KeyCode _right;

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
	void Update () {
        if(_tapped)
        {
            _doubleTapTimer += Time.deltaTime;

            if(_doubleTapTimer > _doubleTapWindow)
            {
                _doubleTapTimer = 0;
                _tapped = false;
            }
        }

        if(Tapped(_left) || Tapped(_right))
        {
            if(_tapped)
            {
                // Run
                _running = true;
            }

            _tapped = true;
        }

        Vector3 speed = new Vector3();

        if(Pressed(_left))
        {
            speed = Vector3.left;
        }
        else if(Pressed(_right))
        {
            speed = Vector3.right;
        }
        else
        {
            _running = false;
            return;
        }

        if(_running)
        {
            speed *= _runSpeed;
        }
        else
        {
            speed *= _walkSpeed;
        }

        speed *= Time.deltaTime;

        this.transform.position += speed;
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
