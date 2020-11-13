using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class WizardNPC : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBoxNotWorthy;
    public GameObject dialogBoxWorthy;
    public AudioClip wizardWhipser;
    RubyController ruby;
    float timerDisplay;

    // Start is called before the first frame update
    void Start()
    {
        dialogBoxNotWorthy.SetActive(false);
        dialogBoxWorthy.SetActive(false);
        timerDisplay = -1.0f;

        ruby = GameObject.Find("Ruby").GetComponent<RubyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay <= 0)
            {
                dialogBoxNotWorthy.SetActive(false);
                dialogBoxWorthy.SetActive(false);
            }
        }
    }

    public void DisplayDialog(bool hasSword)
    {
        timerDisplay = displayTime;
        ruby.PlaySound(wizardWhipser);
        if (hasSword)
        {
            ruby.becomeWorthy();
            dialogBoxWorthy.SetActive(true);
            UIAmmoCount.instance.AmmoToMagic();
            ruby.ToggleSword();
        }
        else dialogBoxNotWorthy.SetActive(true);
    }
}
