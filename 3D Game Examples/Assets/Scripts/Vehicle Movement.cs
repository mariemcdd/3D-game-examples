using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public float Speed = 5f;
    public float RotationSpeed = 45f;

    private float _horizontalInput;
    private float _verticalInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Speed * _verticalInput * Time.deltaTime);
        transform.Rotate(Vector3.up, RotationSpeed * _horizontalInput * Time.deltaTime);
    }

}