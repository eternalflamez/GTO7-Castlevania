using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {
    [SerializeField]
    private AnimationCurve _gravityCurve;

    private float _curvePointer;
    private float _gravity;
    private bool _falling;

    private float _curveModifier = .5f;

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
        if (_falling)
        {
            float height = (transform.lossyScale.y / 2);

            _curvePointer += Time.deltaTime * _curveModifier;
            _gravity = _gravityCurve.Evaluate(_curvePointer);

            Vector3 distance = 8 * Vector3.up * _gravity * Time.deltaTime;

            Debug.DrawRay(transform.position - (Vector3.up * height), -Vector2.up);

            RaycastHit2D hit = Physics2D.Raycast(transform.position - (Vector3.up * transform.lossyScale.y), -Vector2.up, distance.y);
            if (hit.collider != null)
            {
                _falling = false;
                _curvePointer = 0;

                transform.position = new Vector3(transform.position.x, hit.point.y + height + 0.02f, transform.position.z);
            }
            else
            {
                transform.position -= distance;
            }
        }

        RaycastHit2D checkIfFalling = Physics2D.Raycast(transform.position, -Vector2.up, 1f);
        if (checkIfFalling.collider == null)
        {
            _falling = true;
        }
	}
}
