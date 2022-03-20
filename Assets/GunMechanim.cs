using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMechanim : MonoBehaviour
{
    // Start is called before the first frame update

	private Rigidbody _gunRb;

	[SerializeField] private float forceAmount = 2f;
	[SerializeField] private float fallMultiplier = 2.5f;
	[SerializeField] private float lowJumpMultiplier = 2f;

	[SerializeField] private float torque = 1f;
	
	[SerializeField] private Transform bulletSpawnPosition;
	[SerializeField] private GameObject bullet;
	[SerializeField] private ParticleSystem muzzleParticle;
	
	private void Start()
	{
		_gunRb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    private void Update()
	{
		var currentRotation = transform.rotation.x;
		if (Input.GetMouseButtonDown(0))
		{
			_gunRb.velocity = Vector3.zero;
			//_gunRb.AddExplosionForce(forceAmount,transform.position,5f);
			_gunRb.AddForceAtPosition(-transform.forward * forceAmount,transform.position,ForceMode.Impulse);
			_gunRb.AddTorque(transform.right * torque, ForceMode.Impulse);

			muzzleParticle.transform.position = bulletSpawnPosition.position;
			muzzleParticle.Play();
			Shoot();
		}
		
		Gravity();
    }

	private void Gravity()
	{
		if (_gunRb.velocity.y < 0)
		{
			_gunRb.velocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
		}
		else if (_gunRb.velocity.y > 0)
		{
			_gunRb.velocity += Vector3.up * (Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
		}
	}

	private void Shoot()
	{
		var spawnedBullet = Instantiate(bullet, bulletSpawnPosition.position,UnityEngine.Quaternion.identity);
		
		spawnedBullet.GetComponent<Bullet>().travelDirection = transform.forward;
	}
}
