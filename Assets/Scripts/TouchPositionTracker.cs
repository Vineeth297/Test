using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPositionTracker : MonoBehaviour
{
	private Camera _camera;

	private void Start() => _camera = Camera.main;

	private void Update()
	{
		var mousePosition = Input.mousePosition;

		mousePosition.z = 10f;
		
		if (Camera.main != null) transform.position = _camera.ScreenToWorldPoint(mousePosition);
	}
}
