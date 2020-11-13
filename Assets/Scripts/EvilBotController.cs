using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBotController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float timerMax = 3.0f;
    public ParticleSystem smokeEffect;
    public AudioClip gotHit;
    public AudioClip gotFixed;

    Rigidbody2D rigidbody2d;
    Animator animator;
    float timer;
    public int direction = 1;
    RubyController ruby;

    int aleatoire;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = timerMax;

        animator = GetComponent<Animator>();
        ruby = GameObject.Find("Ruby").GetComponent<RubyController>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            direction = -direction;
            timer = timerMax;
            RandomVerti();
        }

    }

    void RandomVerti()
    {
        aleatoire = Random.Range(0, 100);
        vertical = (aleatoire % 2 == 0) ? false : true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y += Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            position.x += Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }

        rigidbody2d.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController player = collision.gameObject.GetComponent<RubyController>();

        if (player != null) player.ChangeHealth(-100f);
    }

    public void Fix()
    {
        ruby.PlaySound(gotHit);
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        ruby.PlaySound(gotFixed);
        ruby.RepairEvilProgress();
    }
}
