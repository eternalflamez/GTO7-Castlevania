using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {
    [SerializeField]
    private AnimationCurve _gravityCurve;

    private float _curvePointer;
    private float _gravity;
    private bool _falling;

	// Use this for initialization
	void Start () {
        _gravity = 0;
        _curvePointer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        _curvePointer += Time.deltaTime;
        _gravity = _gravityCurve.Evaluate(_curvePointer);

        transform.position -= Vector3.up * _gravity * Time.deltaTime;
	}
}
