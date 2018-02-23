using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float offset;
    [SerializeField]
    float speed;

    void Update()
    {
        transform.position = new Vector3(target.position.x + offset , 
            transform.position.y,
            1);
    }
}
