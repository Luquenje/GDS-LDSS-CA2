using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    private const float Y_ANGLE_MIN = 10.0f;
    private const float Y_ANGLE_MAX = 80.0f;

    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;

    [SerializeField] private float distance = 10.0f;
    [SerializeField] private float currentX = 0.0f;
    [SerializeField] private float currentY = 40.0f;
    [SerializeField] private float sensitivityX = 1.0f;
    [SerializeField] private float sensitivitY = 1.0f;

    // Use this for initialization
    void Start () {
        camTransform = transform;
        cam = Camera.main;
	}
	
    void Update()
    {
        //if (Input.GetMouseButton(1))
        //{
            currentX += Input.GetAxis("Mouse X");
            currentY -= Input.GetAxis("Mouse Y");

            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        //}
        
    }

	// Update is called once per frame
	void LateUpdate () {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        
            camTransform.position = lookAt.position + rotation * dir;
        
        camTransform.LookAt(lookAt.position);
	}
}
