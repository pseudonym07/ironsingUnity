using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float sensitivity;
    [SerializeField] Transform coordinate;

    Vector3 savedRotation = Vector3.zero;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        transform.position = coordinate.position + Vector3.up * 0.5f;

        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        Vector3 rotationVector = new Vector3(-y, x, 0); 
        savedRotation += rotationVector * sensitivity;
        
        transform.rotation = Quaternion.Euler(savedRotation);
    }
}
