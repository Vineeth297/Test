using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBlendTree : MonoBehaviour
{
	private Animator _animator;

	private static readonly int Velocity = Animator.StringToHash("Velocity");
	private float _velocity = 0f;
	public float accelaration = 0.1f;
	private void Start()
	{
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		bool isForwardPressed = Input.GetKey(KeyCode.W);

		if (isForwardPressed && _velocity < 1f) _velocity += Time.deltaTime * accelaration;

		if (!isForwardPressed && _velocity > 0f) _velocity -= Time.deltaTime * accelaration;

		if (!isForwardPressed && _velocity <= 0f) _velocity = 0f;
		
		_animator.SetFloat(Velocity,_velocity);
	}
}
