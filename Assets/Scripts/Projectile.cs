using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float distance = 0.1f;
	public List<Vector3> travelPoints;
	public float rotationSpeed = 0.1f;
	
	private void Start() => StartCoroutine(Shoot(travelPoints));

    private IEnumerator Shoot(List<Vector3> pointsToTravel)
    {
	   // transform.rotation = Quaternion.Euler(0f,90f,0f);
	    foreach (var point in pointsToTravel)
	    {
		    var dir = point - transform.position;
			if(dir == Vector3.zero) continue;
			
			/*if (dir != Vector3.zero)
			{
				var rot = Quaternion.LookRotation(dir,Vector3.forward);
				transform.rotation = rot;
			}*/
			var trueDir = point - transform.position;

			while (Vector3.Distance(transform.position, point) > 0.05f)
			{
				//rot.y = 0f;
				//transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation,rot,Time.deltaTime * rotationSpeed);
				transform.position = Vector3.MoveTowards(transform.position, point, distance);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(trueDir),Time.deltaTime * rotationSpeed);
				yield return null;
			}
	    }	

	    gameObject.SetActive(false);
    }
}
