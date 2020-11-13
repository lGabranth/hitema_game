using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public AudioClip throwClip;
    RubyController ruby;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        ruby = GameObject.Find("Ruby").GetComponent<RubyController>();
    }

    public void Launch(Vector2 direction, float force)
    {
        ruby.PlaySound(throwClip);
        rigidbody2d.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController bot = collision.collider.GetComponent<EnemyController>();
        EvilBotController evilBot = collision.collider.GetComponent<EvilBotController>();
        if (bot != null) bot.Fix();
        if (evilBot != null) evilBot.Fix();
        Destroy(gameObject);
    }
}
