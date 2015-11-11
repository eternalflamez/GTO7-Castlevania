using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {
    [SerializeField]
    private AnimationCurve _gravityCurve;

    private float _curvePointer;
    private float _gravity;
    private bool _falling;

    private float _curveModifier = .5f;

	// Use this for initialization
	void Start () {
        _gravity = 0;
        _curvePointer = 0;
        _falling = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (_falling)
        {
            _curvePointer += Time.deltaTime * _curveModifier;
            _gravity = _gravityCurve.Evaluate(_curvePointer);

            Vector3 distance = Vector3.up * _gravity * Time.deltaTime;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, distance.y);
            if (hit.collider != null)
            {
                _falling = false;
                _curvePointer = 0;

                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            }
            else
            {
                transform.position -= distance;
            }
        }

        RaycastHit2D checkIfFalling = Physics2D.Raycast(transform.position, -Vector2.up, 0.05f);
        if (checkIfFalling.collider == null)
        {
            _falling = true;
        }
	}
}
