using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripting : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            var screenPos = _camera.WorldToScreenPoint(Input.mousePosition);
            var mousePosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, screenPos.z));
            print("ScreenToWorldPoint " + mousePosition);  // return the camera position

            var mousePosition1 = _camera.ScreenToViewportPoint(Input.mousePosition);
            print("ScreenToViewportPoint " + mousePosition1);
            
            
        }
    }
}
