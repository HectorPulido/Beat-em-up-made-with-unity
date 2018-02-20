using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : Fighter
{
    [SerializeField]
    float SearchRange = 1;
    [SerializeField]
    float stoppingDistance = 1;

    Vector3 target;
    const int patrol = 0, pursuit = 1;
    int state = patrol;
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SetTarget", 0, 5);
        InvokeRepeating("LunchPunch", 0, 5);
    }

    void LunchPunch()
    {
        if (state != pursuit)
            return;

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("PUNCH_Player")
               && !anim.GetCurrentAnimatorStateInfo(0).IsName("FALL_Player"))
        {
            if (vel.x == 0 && vel.y == 0)
            {
                StartCoroutine("SendPunch");
            }
        }
    }

    void SetTarget()
    {
        if (state == pursuit)
            return;

        target = new Vector2(Random.Range(-1, 1) + transform.position.x,
            Random.Range(verticalBorder.x, verticalBorder.y));
    }


    Vector3 vel;
	void Update ()
    {
        if (state == pursuit)
            target = player.position;

        vel = (transform.position - target);
        sr.flipX = vel.x > 0;

        if (vel.magnitude < stoppingDistance)
            vel = Vector3.zero;

        vel.Normalize();
        rb.velocity = - new Vector2(vel.x * horizontalSpeed, vel.y * verticalSpeed);

        if (state == patrol)
        {
            var range = Physics2D.CircleCast(transform.position, SearchRange, Vector2.up);
            if (range.collider != null)
            {
                if (range.collider.transform == player)
                    state = pursuit;
            }
        }
        else if (state == pursuit)
        {
            if (Vector3.Distance(transform.position, player.position) > SearchRange * 1.2f)
            {
                state = patrol;
            }
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("PUNCH_Player")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("FALL_Player"))
            {
                anim.SetBool("isWalking", vel.x != 0 || vel.y != 0);
            }
        }
    }
    override protected void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SearchRange);
    }

}
