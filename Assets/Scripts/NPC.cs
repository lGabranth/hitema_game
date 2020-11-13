using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBoxWithCheese;
    public GameObject dialogBox;
    public AudioClip jambiTalk;
    RubyController ruby;
    float timerDisplay;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        dialogBoxWithCheese.SetActive(false);
        timerDisplay = -1.0f;

        ruby = GameObject.Find("Ruby").GetComponent<RubyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay <= 0)
            {
                dialogBox.SetActive(false);
                dialogBoxWithCheese.SetActive(false);
            }
        }
    }

    public void DisplayDialog(bool cheeseTaken)
    {
        timerDisplay = displayTime;
        ruby.PlaySound(jambiTalk);
        if (cheeseTaken)
        {
            ruby.ToggleCheese();
            dialogBoxWithCheese.SetActive(true);
            SwordWall wall = GameObject.Find("KeyWall").GetComponent<SwordWall>();
            if (wall != null)
            {
                wall.Explode();
            }
        }
        else dialogBox.SetActive(true);
    }
}
