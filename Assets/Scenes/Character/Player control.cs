using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontrol : MonoBehaviour
{


    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnspeed = 360;
    private Vector3 _Input;
    public Vector3 Initalrotation = new Vector3(-90,0,0);

    private void Start()
    {
        transform.rotation = Quaternion.Euler(Initalrotation);
    }
    void Update()
    {
        Gatherinput();
        look();
    }
    void FixedUpdate()
    {
        Move();
    }
    void look()
    {
        if (_Input != Vector3.zero)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var skewedInput = matrix.MultiplyPoint3x4(_Input);

            var relative = (transform.position + _Input) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,rot, _turnspeed * Time.deltaTime);
        }
       
    }
    void Gatherinput()
    {
        float xDriection = Input.GetAxisRaw("Horizontal");
        float zDriection = Input.GetAxisRaw("Vertical");

        _Input = new Vector3(xDriection, 0, zDriection);
    }

    void Move()
    {
        _rb.MovePosition(transform.position + (transform.forward * _Input.magnitude) * _speed * Time.deltaTime);
    }

}
