using System.Collections.Generic;
using System.Linq;
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

	[SerializeField] private Transform muzzle;
	private float _xToAdd, _yToAdd;

	private void Start()
	{
		_camera = Camera.main;
		if (_camera != null) _drawPlane = new Plane(_camera.transform.forward * -1, transform.position);
		
		pathPositions = new List<Vector3>();
		travelPoints = new List<Vector3>();

		_xToAdd = 0f;
		_yToAdd = 0f;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if(_trail)
				Destroy(_trail);
			
			_trail = Instantiate(drawPrefab, transform.position, Quaternion.identity);

			var touchRay = _camera.ScreenPointToRay(Input.mousePosition);

			print(touchRay);

			if (!_drawPlane.Raycast(touchRay, out var hit)) return;
			_trail.transform.position = touchRay.GetPoint(hit);
			// _drawingStartPosition = touchRay.GetPoint(hit);
			
			_drawingStartPosition = new Vector3(touchRay.GetPoint(hit).x,touchRay.GetPoint(hit).y,0f);
			_xToAdd = muzzle.position.x - _drawingStartPosition.x;
			_yToAdd = muzzle.position.y - _drawingStartPosition.y;
			
			if(pathPositions.Count > 0)
				pathPositions.Clear();
				
			pathPositions.Add(muzzle.position);
		}
		else if (Input.GetMouseButton(0))
		{
			var touchDragRay = _camera.ScreenPointToRay(Input.mousePosition);
			
			if (!_drawPlane.Raycast(touchDragRay, out var hit)) return;
			_trail.transform.position = touchDragRay.GetPoint(hit);
			//_trail.transform.position = new Vector3(touchDragRay.GetPoint(hit).x,touchDragRay.GetPoint(hit).y,0f);
			
			var point = new Vector3(_trail.transform.position.x, _trail.transform.position.y,0f);	
			var pointToAdd = new Vector3(_xToAdd + point.x, _yToAdd + point.y, point.z );
			
			if (!(Time.time > _addTime)) return;
				
			_addTime = Time.time + timeGap;
			
			if(!pathPositions.Contains(pointToAdd))
				pathPositions.Add(pointToAdd);
		}

		if (!Input.GetKeyDown(KeyCode.A)) return;
		
		var projectile = Instantiate(projectilePrefab, pathPositions[0], Quaternion.identity);
		GeneratePathPoints();
		projectile.GetComponent<Projectile>().travelPoints = travelPoints;
	}

	private void GeneratePathPoints()
	{
		if(travelPoints.Count > 0) travelPoints.Clear();
		
		var xPoints = new float[pathPositions.Count];
		for (var i = 0; i < pathPositions.Count; i++)
		{
			travelPoints.Add(pathPositions[i]);
			xPoints[i] = pathPositions[i].x;
		}

		var horizontalLength = Mathf.Abs(xPoints.Min() - xPoints.Max());
		var verticalLength = pathPositions[^1].y - pathPositions[0].y;
		
		for (var i = 1; i <= maxRepetitions; i++)
		{
			for (var j = 1; j < pathPositions.Count; j++)
			{
				var newPoint = pathPositions[j] + new Vector3(horizontalLength, verticalLength,0f) * i;
				travelPoints.Add(newPoint);
			}	
		}
	}
}

