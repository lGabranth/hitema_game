using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Crow : MonoBehaviour
{
    public GameObject itemToDeliver;
    Rigidbody2D rigidbody2d;
    public AudioClip crowShoot;
    private RubyController ruby;

    public void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        ruby = GameObject.Find("Ruby").GetComponent<RubyController>();
    }

    public void DeliverItem()
    {
        ruby.PlaySound(crowShoot);
        Instantiate(itemToDeliver, rigidbody2d.position + Vector2.down * 2f, Quaternion.identity);
    }
}
