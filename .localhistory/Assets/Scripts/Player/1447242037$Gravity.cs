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
        Vector3 raycastStart = transform.position - (Vector3.up * height);

        if (_falling)
        {
            _curvePointer += Time.deltaTime * _curveModifier;
            _gravity = _gravityCurve.Evaluate(_curvePointer);

            Vector3 distance = _speedModifier * Vector3.up * _gravity * Time.deltaTime;

            Debug.DrawRay(raycastStart, -Vector2.up);

            RaycastHit2D hitBottomLeft = Physics2D.Raycast(raycastStart, -Vector2.up, distance.y);

            if (hitBottomLeft.collider != null)
            {
                _falling = false;
                _curvePointer = 0;

                transform.position = new Vector3(transform.position.x, hitBottomLeft.point.y + height + 0.0005f, transform.position.z);
            }
            else
            {
                transform.position -= distance;
            }
        }

        RaycastHit2D checkIfFalling = Physics2D.Raycast(raycastStart, -Vector2.up, .1f);
        if (checkIfFalling.collider == null)
        {
            _falling = true;
        }
	}
}
