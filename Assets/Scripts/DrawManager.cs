using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
	[SerializeField] private GameObject drawPrefab;

	private GameObject _trail;
	private Plane _drawPlane;
	private Camera _camera;
	
	private Vector3 _drawingStartPosition;

	public List<Vector3> pathPositions;

	public float timeGap;
	private float _addTime = 0f;

	public List<Vector3> travelPoints;
	public GameObject projectilePrefab;

	[SerializeField]private int maxRepetitions = 1;

	private void Start()
	{
		_camera = Camera.main;
		if (_camera != null) _drawPlane = new Plane(_camera.transform.forward * -1, transform.position);
		
		pathPositions = new List<Vector3>();
		travelPoints = new List<Vector3>();
	}

	private void Update()
	{

		if (Input.GetMouseButtonDown(0))
		{
			if(_trail)
				Destroy(_trail);
			
			_trail = Instantiate(drawPrefab, transform.position, Quaternion.identity);

			var touchRay = _camera.ScreenPointToRay(Input.mousePosition);
			
			if (!_drawPlane.Raycast(touchRay, out var hit)) return;
			_drawingStartPosition = touchRay.GetPoint(hit);
				
			if(pathPositions.Count > 0)
				pathPositions.Clear();
				
			pathPositions.Add(_drawingStartPosition);
		}
		else if (Input.GetMouseButton(0))
		{
			var touchDragRay = _camera.ScreenPointToRay(Input.mousePosition);
			
			if (!_drawPlane.Raycast(touchDragRay, out var hit)) return;
			
			_trail.transform.position = touchDragRay.GetPoint(hit);

			if (!(Time.time > _addTime)) return;
				
			_addTime = Time.time + timeGap;
			
			if(!pathPositions.Contains(_trail.transform.position))
				pathPositions.Add(_trail.transform.position);
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			
			var projectile = Instantiate(projectilePrefab, pathPositions[0], Quaternion.identity);
			GeneratePathPoints();
			projectile.GetComponent<Projectile>().travelPoints = travelPoints;
		}
	}

	private void GeneratePathPoints()
	{
		if(travelPoints.Count > 0)
			travelPoints.Clear();
		
		for (var i = 0; i < pathPositions.Count; i++)
		{
			travelPoints.Add(pathPositions[i]);
		}
		var lastPoint = travelPoints[^1];

		for (var j = 0; j < maxRepetitions; j++)
		{
			for (var i = 0; i < pathPositions.Count; i++)
			{
				var newPoint = lastPoint + pathPositions[i];
				travelPoints.Add(newPoint);
			}
			lastPoint = travelPoints[^1];
		}
	}
	
}
