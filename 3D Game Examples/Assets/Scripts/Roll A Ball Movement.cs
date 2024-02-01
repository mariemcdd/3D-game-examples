using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerBallMovement : MonoBehaviour
{
    private int _count;

    private Rigidbody _playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput;
        float verticalInput;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);
        _playerRigidbody.AddForce(movement);
    }
}