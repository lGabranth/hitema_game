using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwapScene2And3 : MonoBehaviour
{
    public CinemachineVirtualCamera cmv1;
    public CinemachineVirtualCamera cmv2;

    void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController player = collision.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            if (cmv1.gameObject.active)
            {
                cmv2.gameObject.SetActive(true);
                cmv1.gameObject.SetActive(false);
            }
            else
            {
                cmv1.gameObject.SetActive(true);
                cmv2.gameObject.SetActive(false);
            }
        }
    }
}
