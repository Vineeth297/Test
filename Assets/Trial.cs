using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trial : MonoBehaviour
{
	public List<Transform> traversePoints;

	public float lerpTime = 0.1f;
    void Start()
	{
		StartCoroutine(StartMoving());
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private IEnumerator StartMoving()
	{
		for (int i = 0; i < traversePoints.Count; i++)
		{
			while (Vector3.Distance(transform.position, traversePoints[i].position) > 0.05f)
			{
				transform.position = Vector3.MoveTowards(transform.position,traversePoints[i].position , lerpTime);
				yield return null;
			}
		}
	}
}
