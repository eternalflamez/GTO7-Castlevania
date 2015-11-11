using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {
    [SerializeField]
    private AnimationCurve _gravityCurve;

    [SerializeField]
    private Jump _jump;

    private float _curvePointer;
    [SerializeField]
    private float _gravity;
    private bool _falling;

    [SerializeField]
    private float _curveModifier = .34f;
    [SerializeField]
    private float _speedModifier = 8;

    public bool Falling
    {
        get { return _falling; }
        private set {}
    }

	// Use this for initialization
	void Start () {
        _gravity = 0;
        _curvePointer = 0;
        _falling = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(_jump.Jumping)
        {
            return;
        }

        float height = (transform.lossyScale.y / 2);
        float width = (transform.lossyScale.x / 2);
        Vector3 raycastStart = transform.position - (Vector3.up * height);

        if (_falling)
        {
            _curvePointer += Time.deltaTime * _curveModifier;
            _gravity = _gravityCurve.Evaluate(_curvePointer);

            Vector3 distance = _speedModifier * Vector3.up * _gravity * Time.deltaTime;

            Debug.DrawRay(raycastStart + (Vector3.left * width), -Vector2.up);
            Debug.DrawRay(raycastStart + (Vector3.right * width), -Vector2.up);

            RaycastHit2D hitBottomLeft = Physics2D.Raycast(raycastStart + (Vector3.left * width), -Vector2.up, distance.y);
            RaycastHit2D hitBottomRight = Physics2D.Raycast(raycastStart + (Vector3.right * width), -Vector2.up, distance.y);

            if (hitBottomLeft.collider != null || hitBottomRight.collider != null)
            {
                _falling = false;
                _curvePointer = 0;

                if (hitBottomLeft.collider != null)
                {
                    transform.position = new Vector3(transform.position.x, hitBottomLeft.point.y + height + 0.0005f, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, hitBottomRight.point.y + height + 0.0005f, transform.position.z);
                }
            }
            else
            {
                transform.position -= distance;
            }
        }

        RaycastHit2D checkIfFallingLeft = Physics2D.Raycast(raycastStart + (Vector3.left * width - .01f), -Vector2.up, .1f);
        RaycastHit2D checkIfFallingRight = Physics2D.Raycast(raycastStart + (Vector3.right * width - 0.1f), -Vector2.up, .1f);
        if (checkIfFallingLeft.collider == null && checkIfFallingRight.collider == null)
        {
            _falling = true;
        }
	}
}
