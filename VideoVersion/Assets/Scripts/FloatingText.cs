using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class FloatingText : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    float lifeTime;

    TextMesh tm;

    void Awake()
    {
        tm = GetComponent<TextMesh>();
        Destroy(gameObject, lifeTime);
    }

    public void Init(string text)
    {
        tm.text = text;
    }

	void Update ()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
	}
}
