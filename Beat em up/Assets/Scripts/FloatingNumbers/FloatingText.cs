using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour {

    public TextMesh text;
    public float upSpeed;
    public float lifeTime;

    public void Init(string Text)
    {
        text.text = Text.ToString();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += Vector3.up * upSpeed * Time.deltaTime;
    }
}
