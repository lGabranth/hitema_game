using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Cheese : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        RubyController ruby = collision.GetComponent<RubyController>();
        if(ruby != null)
        {
            ruby.ToggleCheese();
        }
    }
}
