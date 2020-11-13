using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public AudioClip satanShoot;
    private RubyController ruby;

    public void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        ruby = GameObject.Find("Ruby").GetComponent<RubyController>();
    }

    public void DeliverItem(bool hasKey)
    {
        ruby.PlaySound(satanShoot);
        if (hasKey)
        {
            ruby.ToggleSword();
        }
    }
}
