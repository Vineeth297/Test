using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quaternion : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 _position;
    private void Update()
    {
        _position = transform.position;

        var direction = target.position - _position;
        
        var rot = UnityEngine.Quaternion.LookRotation(direction,Vector3.up);
        transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation, rot, 0.5f * Time.deltaTime);
        //
    }
}
