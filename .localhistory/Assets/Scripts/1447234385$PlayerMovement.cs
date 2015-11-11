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

	// Use this for initialization
	void Start () {
        _doubleTapTimer = 0;
        _tapped = false;
	}
	
	// Update is called once per frame
	void Update () {
        /*
         * if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.position += Vector3.left * Time.deltaTime * _walkSpeed;
                _tapped = true;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.position += Vector3.right * Time.deltaTime * _walkSpeed;
                _tapped = true;
            }
         * */

        if(_tapped)
        {
            _doubleTapTimer += Time.deltaTime;

            if(_doubleTapTimer > _doubleTapWindow)
            {
                _tapped = false;
            }
        }

        if(Pressed(_left) || Pressed(_right))
        {
            if(_tapped)
            {

            }

            _tapped = true;
        }
    }

    private bool Tapped(KeyCode button)
    {
        return Input.GetKey(button);
    }

    private bool Pressed(KeyCode button)
    {
        return Input.GetKeyDown(button);
    }
}
