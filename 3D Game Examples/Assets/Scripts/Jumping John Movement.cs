using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingJohnMovement : MonoBehaviour
{
    public int score = 0;
    public float turnSpeed = 20f;
    public float moveSpeed = 1f;
    public float JumpForce = 10f;
    public float GravityModifier = 1f;
    public float outOfBounds = -10f;
    public bool IsOnGround = true;
    public bool isAtCheckpoint = false;
    public GameObject checkPointAreaObject;
    public GameObject finishAreaObject;
    private Vector3 _movement;
    // Animator _Animator;
    private Rigidbody _rigidbody;
    private Quaternion _rotation = Quaternion.identity;
    private Vector3 _defaultGravity = new Vector3(0f, -9.81f, 0f);
    private Vector3 _startingPosition;

    private Vector3 _checkpointPosition;

    // Start is called before the first frame update
    void Start()
    {
        // _Animator = GetComponent<Animator> ();
        _rigidbody = GetComponent<Rigidbody>();
        Physics.gravity = _defaultGravity;
        Physics.gravity *= GravityModifier;
        _startingPosition = transform.position;
    }

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }

        if(transform.position.y < outOfBounds)
        {
            if(isAtCheckpoint)
            {
                transform.position = _checkpointPosition;
            }
            else
            {
                transform.position = _startingPosition;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _movement.Set(horizontal, 0f, vertical);
        _movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        // _Animator.SetBool ("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, _movement, turnSpeed * Time.deltaTime, 0f);
        _rotation = Quaternion.LookRotation (desiredForward);

        _rigidbody.MovePosition (_rigidbody.position + _movement * moveSpeed * Time.deltaTime);
        _rigidbody.MoveRotation (_rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }

        if(collision.gameObject.CompareTag("Spinner"))
        {
            if(isAtCheckpoint)
            {
                transform.position = checkPointAreaObject.transform.position;
            }
            else
            {
                transform.position = _startingPosition;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == checkPointAreaObject)
        {
            isAtCheckpoint = true;
            _checkpointPosition = checkPointAreaObject.transform.position;
        }

        if(other.gameObject == finishAreaObject)
        {
            isAtCheckpoint = false;
            transform.position = _startingPosition;
        }

        if(other.gameObject.CompareTag("Collectible - Destroy"))
        {
            score++;
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Collectible - Return"))
        {
            other.gameObject.SetActive(false);
        }
    }
}