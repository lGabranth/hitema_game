using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlace : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController player = collision.gameObject.GetComponent<RubyController>();

        if (player != null) player.ChangeHealth(-100f, true);
    }
}
