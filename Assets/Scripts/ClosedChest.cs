using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedChest : MonoBehaviour
{
    GameObject chestOpen;
    public GameObject redkey;
    public GameObject bluekey;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        chestOpen = GameObject.Find("chest-open");
        chestOpen.SetActive(false);
        redkey.SetActive(false);
        bluekey.SetActive(false);
    }

    public void OpenChest()
    {
        Destroy(gameObject);
        chestOpen.SetActive(true);
        redkey.SetActive(true);
        bluekey.SetActive(true);
    }
}
