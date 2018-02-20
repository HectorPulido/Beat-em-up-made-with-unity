using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyEvent : UnityEngine.Events.UnityEvent
{
}
[System.Serializable]
public class MyIntEvent : UnityEngine.Events.UnityEvent<string>
{
}

public class PunchScript : MonoBehaviour {

    [SerializeField]
    float force = 1000;
    [SerializeField]
    int lives = 3;
    public int stamina = 100;
    [SerializeField]
    float invulnerableTime = 0.5f;

    
    [HideInInspector]
    public bool guard = false;

    public MyEvent whenDie;
    public MyIntEvent whenLoseLife;
    public MyIntEvent whenLoseStamina;

    Rigidbody2D rb;
    Animator anim;

    void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("AddStamina", 10, 10);
    }

    void AddStamina()
    {
        stamina += 10;
        whenLoseStamina.Invoke(stamina.ToString());
    }

    bool invulnerable = false;
    void Punch(Vector3 position)
    {
        if (invulnerable)
            return;
        if (guard)
        {
            stamina -= 10;
            whenLoseStamina.Invoke(stamina.ToString());
            return;
        }
        print("Auch...");
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("FALL_Player"))
        {
            invulnerable = true;
            StartCoroutine(SetVulnerable());
            anim.SetTrigger("getPunch");
            position.y = transform.position.y;
            rb.AddForce((transform.position - position) * force, ForceMode2D.Impulse);
            lives--;
            whenLoseLife.Invoke(lives.ToString());
            if (lives < 0)
                whenDie.Invoke();
            
        }
    }
    IEnumerator SetVulnerable()
    {
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }
}
