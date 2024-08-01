using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramovement : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smooth;
    private Vector3 velocity = Vector3.zero;

    private void Awake() //sets the camera to follow the targeted player
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate() //updates the cmaera postion based on the players current postions movement path
    {
        Vector3 targetpostion = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetpostion, ref velocity, smooth);

    }
}
