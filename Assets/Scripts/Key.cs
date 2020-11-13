using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string color;
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController controller = collision.GetComponent<RubyController>();
        if (controller != null)
        {
            if (color == "blue")
            {
                Destroy(gameObject);
                controller.ChangeHealth(-100f);
            }
            if(color == "red")
            {
                Destroy(gameObject);
                controller.ToggleKey();
            }

            controller.PlaySound(collectedClip);
        }
    }
}
