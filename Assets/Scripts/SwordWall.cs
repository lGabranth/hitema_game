using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWall : MonoBehaviour
{
    Animator animator;
    public GameObject Explosion;
    public AudioClip explode;
    private Rigidbody2D rigidbody2d;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Explode()
    {
        Instantiate(Explosion, rigidbody2d.position, Quaternion.identity);
        GameObject.Find("Ruby").GetComponent<RubyController>().PlaySound(explode);
        Destroy(gameObject);
    }
}
