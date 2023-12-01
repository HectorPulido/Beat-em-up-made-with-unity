using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    public static Vector2 LimitsY = new Vector2(-0.594f, -1.636f);

    [SerializeField]
    public Vector2 limitsY = new Vector2(-0.594f, -1.636f);

    public float offset;
    public Transform target;

    void Start()
    {
        LimitsY = limitsY;
    }

    void Update()
    {
        if (!target)
            return;
        transform.position = new Vector3(offset + target.position.x,
            transform.position.y,
            transform.position.z);
    }
}
