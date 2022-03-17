using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

	public float distance = 0.1f;
	public List<Vector3> travelPoints;
    void Start()
	{
		StartCoroutine(Shoot(travelPoints));
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private IEnumerator Shoot(List<Vector3> pointsToTravel)
	{
		//var bullet = Instantiate(projectilePrefab, pointsToTravel[0], Quaternion.identity);
		for (var point = 0; point < pointsToTravel.Count; point++)
		{
			while (Vector3.Distance(transform.position, pointsToTravel[point]) > 0.05f)
			{
				transform.position = Vector3.MoveTowards(transform.position, pointsToTravel[point], distance);
				yield return null;
			}
		}
	}
}
