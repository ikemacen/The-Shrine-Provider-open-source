using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Playercontrol : MonoBehaviour
{


    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnspeed = 360;
    private Vector2 _input;
    private Vector3 _movementDirection;
    public Vector3 initalrotation = new Vector3(-90,0,0);

    private void Start()
    {
        //transform.rotation = Quaternion.Euler(initalrotation);
        transform.rotation = Quaternion.identity;
    }
    void Update()
    {
        _input = GatherInput();
        _movementDirection = CalculateDirection(_input);
        // Normalize here to prevent faster diagonal movement
        _movementDirection.Normalize();
        print(_movementDirection);
        // Only write to rotation if movement is not zero
        // Otherwise, don't rotate
        if (_movementDirection != Vector3.zero)
        {
            transform.rotation = look(_movementDirection);
        }
    }
    void FixedUpdate()
    {
        Move();
    }
    Quaternion look(Vector3 MovementDirection)
    {
        //var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        //var skewedInput = matrix.MultiplyPoint3x4(_Input);

        var relative = (transform.position + MovementDirection) - transform.position;
        var rot = Quaternion.LookRotation(relative, Vector3.up);
        return Quaternion.RotateTowards(transform.rotation,rot, _turnspeed * Time.deltaTime);
    }

    // Read input and translate into an input vector
    Vector2 GatherInput()
    {
        float xDirection = Input.GetAxisRaw("Horizontal");
        float yDirection = Input.GetAxisRaw("Vertical");

        return new Vector2(xDirection, yDirection);
    }

    // Turn the input vector into the correct 3d directional vectors
    Vector3 CalculateDirection(Vector2 input) {
        // These are the correct movement values according to each input axis
        Vector3 adjustedXVector = new Vector3(-1, 0, 1);
        Vector3 adjustedZVector = new Vector3(1, 0, 1);
        // We calculate the correct movement direction
        adjustedXVector *= -_input.x;
        adjustedZVector *= _input.y;
        // Add both directions together to get the finalized movement direction
        return adjustedZVector + adjustedXVector;
    }

    // Execute the movement to our player rigidbody
    void Move()
    {
        _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speed * Time.deltaTime);
    }

}
