using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField]
    protected float horizontalSpeed;
    [SerializeField]
    protected float verticalSpeed;
    [SerializeField]
    protected Vector2 verticalBorder;
    [SerializeField]
    protected Transform punchPositionLeft;
    [SerializeField]
    protected Transform punchPositionRight;
    [SerializeField]
    protected float radiusPunch = 0.5f;

    protected Animator anim;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(punchPositionLeft.position, radiusPunch);
        Gizmos.DrawWireSphere(punchPositionRight.position, radiusPunch);
    }

    IEnumerator SendPunch()
    {
        anim.SetTrigger("punch");
        yield return new WaitForSeconds(0.9f);

        Vector2 punchPosition = sr.flipX ? punchPositionRight.position : punchPositionLeft.position;

        var punch = Physics2D.CircleCast(punchPosition, radiusPunch, Vector2.up);
        if (punch.collider != null)
        {
            if(punch.collider.gameObject != gameObject)
                punch.collider.SendMessage("Punch", transform.position, SendMessageOptions.DontRequireReceiver);
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Pot"))
        {
            SendMessage("Punch", col.transform.position);
        }
    }
}
