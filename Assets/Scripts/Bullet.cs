using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Vector3 travelDirection;
	public float shootForce = 10f;
	
	private void Update()
	{
		Shoot();
	}

	private void Shoot()
	{
		transform.Translate(travelDirection * (Time.deltaTime * shootForce));
	}
}
