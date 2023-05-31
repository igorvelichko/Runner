using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 camerapos;
    void Start()
    {
        camerapos = transform.position - player.position;
    }

    
    void FixedUpdate()
    {
        Vector3 Position = new Vector3(transform.position.x, transform.position.y, camerapos.z + player.position.z);
        transform.position = Position;
    }
}
