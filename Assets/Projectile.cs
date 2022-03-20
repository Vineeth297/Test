using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float distance = 0.1f;
	public List<Vector3> travelPoints;
    
	private void Start() => StartCoroutine(Shoot(travelPoints));

    private IEnumerator Shoot(List<Vector3> pointsToTravel)
    {
	    foreach (var point in pointsToTravel)
	    {
		    while (Vector3.Distance(transform.position, point) > 0.05f)
		    {
			    transform.position = Vector3.MoveTowards(transform.position, point, distance);
			    yield return null;
		    }
	    }

	    gameObject.SetActive(false);
    }
}
