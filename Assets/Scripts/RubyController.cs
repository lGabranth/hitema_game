using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    private float maxHealth = 100f;
    public float MaxHealth {  get { return maxHealth;  } }
    private float speed = 3.0f;
    public GameObject projectilePrefab;
    public GameObject prefabDeath;
    public GameObject prefabFireplaceDeath;
    public GameObject fireballPrefab;
    private float currentHealth;
    private int ammoNumber;
    private bool hasUnlimitedPower;

    public float CurrentHealth { get { return currentHealth; } }
    private Dictionary<string, bool> itemsClefs = new Dictionary<string, bool>();

    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    float horizontal;
    float vertical;

    AudioSource audioSource;
    public AudioClip criDeDouleur;
    public AudioClip walking;
    public AudioClip fanfare;
    public GameObject cheeseImage;
    public GameObject swordImage;
    public GameObject keyImage;
    public GameObject wonImg;
    public GameObject menuPause;
    public GameObject deathCount;

    public CinemachineVirtualCamera cmv1;
    public CinemachineVirtualCamera cmv2;
    public CinemachineVirtualCamera cmv3;
    public CinemachineVirtualCamera cmv4;

    private GameObject textProgress;
    private TextMeshProUGUI progressText;
    private TextMeshProUGUI deathCounter;

    int robotToRepair;
    int repairProgress;
    int evilToRepair;
    int repairEvilProgress;
    private int deathNumber;

    bool hasFinishedTheGame;

    // Start is called before the first frame update
    void Start()
    {
        robotToRepair = 7;
        repairProgress = 0;
        evilToRepair = 11;
        repairEvilProgress = 0;

        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        itemsClefs.Add("cheese", false);
        itemsClefs.Add("key", false);
        itemsClefs.Add("sword", false);
        cheeseImage.SetActive(false);
        swordImage.SetActive(false);
        keyImage.SetActive(false);
        menuPause.SetActive(false);
        textProgress = GameObject.Find("remainingText");
        progressText = textProgress.GetComponent<TextMeshProUGUI>();
        hasFinishedTheGame = false;
        deathCounter = deathCount.GetComponent<TextMeshProUGUI>();
        deathNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (hasUnlimitedPower) ThrowFireball();
            if (!hasUnlimitedPower && ammoNumber > 0) Launch();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if(hit.collider != null)
            {
                NPC jambi = hit.collider.GetComponent<NPC>();
                Crow crow = hit.collider.GetComponent<Crow>();
                ClosedChest chest = hit.collider.GetComponent<ClosedChest>();
                WizardNPC wizard = hit.collider.GetComponent<WizardNPC>();
                Sheep sheep = hit.collider.GetComponent<Sheep>();
                if (jambi != null)
                {
                    jambi.DisplayDialog(itemsClefs["cheese"]);
                }
                if(crow != null) crow.DeliverItem();
                if (chest != null) chest.OpenChest();
                if (wizard != null) wizard.DisplayDialog(isWorthy());
                if (sheep != null) sheep.DeliverItem(itemsClefs["key"]);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)) menuPause.SetActive(true);

        if (hasFinishedTheGame)
        {
            float time = 10;
            time -= Time.deltaTime;
            if (time <= 0) menuPause.SetActive(true);
        }
    }

    public void RepairEvilProgress()
    {
        repairEvilProgress++;
        if (repairEvilProgress == evilToRepair) YouWon();
    }

    public void YouWon()
    {
        GameObject.Find("BackgroundMusic").SetActive(false);
        wonImg.SetActive(true);
        GameObject.Find("BadGuys").SetActive(false);
        PlaySound(fanfare);

    }

    public void RepairProgress()
    {
        repairProgress++;
        int restant = robotToRepair - repairProgress;
        if (restant > 0) progressText.SetText($"Il vous reste {restant} robots à réparer pour accéder à cette zone.");
        else progressText.SetText("Vous pouvez désormais accéder à cette zone à l'aide de la clé.");
    }

    public bool requirementsObtained()
    {
        return (itemsClefs["key"] && robotToRepair == repairProgress);
    }

    public void ToggleCheese()
    {
        itemsClefs["cheese"] = !itemsClefs["cheese"];
        cheeseImage.SetActive(itemsClefs["cheese"]);
    }

    public void ToggleSword()
    {
        itemsClefs["sword"] = !itemsClefs["sword"];
        swordImage.SetActive(itemsClefs["sword"]);
    }

    public void ToggleKey()
    {
        itemsClefs["key"] = !itemsClefs["key"];
        keyImage.SetActive(itemsClefs["key"]);
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;

        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public bool isWorthy()
    {
        return itemsClefs["sword"];
    }

    public void becomeWorthy()
    {
        hasUnlimitedPower = true;
    }

    public void ChangeHealth(float amount, bool fireplace = false)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if(amount < 0) PlaySound(criDeDouleur);
        if(currentHealth <= 0)
        {
            speed = 0;
            if (fireplace) Instantiate(prefabFireplaceDeath, rigidbody2d.position, Quaternion.identity);
            else Instantiate(prefabDeath, rigidbody2d.position, Quaternion.identity);

            currentHealth = 100;
            Vector2 position = rigidbody2d.position;
            position.x = position.y = 0;
            rigidbody2d.position = position;
            if (cmv2.gameObject.active) cmv2.gameObject.SetActive(false);
            if (cmv3.gameObject.active) cmv3.gameObject.SetActive(false);
            if (cmv4.gameObject.active) cmv4.gameObject.SetActive(false);
            cmv1.gameObject.SetActive(true);
            speed = 3.0f;
            deathNumber++;
            deathCounter.SetText("Death : " + deathNumber);
        }
        UIHealthBar.instance.SetValue(currentHealth / maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        ammoNumber--;
        UIAmmoCount.instance.ChangeUI(ammoNumber);
    }

    void ThrowFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        
        Fireball projectile = fireball.GetComponent<Fireball>();
        projectile.Launch(lookDirection, 600);

        animator.SetTrigger("Launch");
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void AddAmmo(int amount)
    {
        ammoNumber += amount;
        UIAmmoCount.instance.ChangeUI(ammoNumber);
    }
}
