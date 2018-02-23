using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    public float offset;
    public Transform target;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (!target)
            return;
        transform.position = new Vector3(offset + target.position.x, 
            transform.position.y, 
            transform.position.z);	
	}
}
