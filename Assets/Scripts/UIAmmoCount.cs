using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class UIAmmoCount : MonoBehaviour
{
    public static UIAmmoCount instance { get; private set; }
    private Text textMesh;
    private GameObject mestText;
    private GameObject ammos;
    private GameObject magic;

    void Awake()
    {
        instance = this;
        mestText = GameObject.Find("AmmoNumberText");
        textMesh = mestText.GetComponent<Text>();

        ammos = GameObject.Find("Ammos");
        magic = GameObject.Find("MagicBaby");
        magic.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        textMesh.text = "0";
        textMesh.color = Color.red;
    }

    public void ChangeUI(int ammoCurrent)
    {
        textMesh.text = ammoCurrent.ToString();
        textMesh.color = (ammoCurrent > 0) ? Color.white : Color.red;
    }

    public void AmmoToMagic()
    {
        if (GameObject.Find("Ruby").GetComponent<RubyController>().isWorthy())
        {
            ammos.SetActive(false);
            magic.SetActive(true);
            textMesh.text = "";
        }
    }
}
