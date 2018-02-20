using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Fighter
{
    [SerializeField]
    float throwVelocity = 3;
    [SerializeField]
    Rigidbody2D potPrefab;

    PunchScript ps;

    void Start()
    {
        ps = GetComponent<PunchScript>();
    }

    void Update ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(h != 0)
            sr.flipX = (0 < h);

        ps.guard = Input.GetKey(KeyCode.C) && ps.stamina > 0;
        anim.SetBool("guard", ps.guard);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("PUNCH_Player")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("FALL_Player")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("GUARD_Player"))
        {
            anim.SetBool("isWalking", h != 0 || v != 0);
            rb.velocity = new Vector2(-h * horizontalSpeed, v * verticalSpeed);
            transform.position = new Vector3(transform.position.x,
                Mathf.Clamp(transform.position.y, verticalBorder.x, verticalBorder.y), // Bordes
                transform.position.z);
            if (Input.GetKeyDown(KeyCode.X))
            {
                StartCoroutine("SendPunch");
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine("ThrowPot");
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }        
    }

    IEnumerator ThrowPot()
    {
        if (ScoreManager.singleton.pots > 0)
        {
            ScoreManager.singleton.pots--;
            anim.SetTrigger("throw");
            yield return new WaitForSeconds(0.5f);
            Rigidbody2D go;
            if (sr.flipX)
            {
                go = Instantiate(potPrefab,
                    punchPositionRight.position,
                    potPrefab.transform.rotation);
                go.velocity = -Vector2.right * throwVelocity;
            }
            else
            {
                go = Instantiate(potPrefab,
                   punchPositionLeft.position,
                   potPrefab.transform.rotation);
                go.velocity = Vector2.right * throwVelocity;
            }
            Destroy(go, 10);
        }

    }
}
