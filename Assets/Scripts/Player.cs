using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float movementSpeed = 1;
    public bool playerShifted = false;
    public float accel = 1f;
    public float decel = 2f;
    public float maxSpeed = 3f;
    public float deadzone = .1f;
    public FollowPlayer followCam;
    public int playerHealth = 10;
    public Slider healthBar;
    public TextMeshProUGUI treasureNum;
    public bool invincible = false;
    public AudioClip playerHitSound;

    [SerializeField] private UIScript uiScript;
    [SerializeField] private PlayerSideDetector leftSide;
    [SerializeField] private PlayerSideDetector rightSide;
    [SerializeField] private PlayerSideDetector upSide;
    [SerializeField] private PlayerSideDetector downSide;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private int fullHealth;
    private int treasureVal = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        fullHealth = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerRotate();
        PlayerMove();
        float healthValue = (float)playerHealth / (float)fullHealth;
        healthBar.value = healthValue;

        if (playerHealth <= 0)
        {
            PlayerDie();
        }
    }

    void PlayerDie()
    {
        gameObject.SetActive(false);
        uiScript.GameOver();
    }

    void PlayerMove()
    {
        Vector3 camPos = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 playerPosition = gameObject.transform.position;

        if (Input.GetButton("left") && camPos.x > 0 && !leftSide.contact)
        {
            transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetButton("right") && camPos.x < 1 && !rightSide.contact)
        {
            transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetButton("up") && camPos.y < 1 && !upSide.contact)
        {
            transform.Translate(0, movementSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetButton("down") && camPos.y > 0 && !downSide.contact)
        {
            transform.Translate(0, -movementSpeed * Time.deltaTime, 0);
        }
        
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
    }

    void PlayerRotate()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = gameObject.transform.position;
        float yCoord = mousePosition.y - playerPosition.y;
        float xCoord = mousePosition.x - playerPosition.x;
        float rotateToRad = Mathf.Atan(yCoord / xCoord);
        float toAdd = -90;
        if (mousePosition.x < playerPosition.x)
        {
            toAdd = 90;
        }
        float rotateToDeg = rotateToRad * (180 / Mathf.PI) + toAdd;
        transform.rotation = Quaternion.Euler(0, 0, rotateToDeg);
    }

    public void PlayerShift()
    {
        if (Input.GetButtonDown("Fire1") && !playerShifted)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
            playerShifted = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "bossZone")
        {
            followCam.enteredBossZone = true;
        }
        if (!invincible)
        {
            if (col.tag == "bossProjectile")
            {
                audioSource.PlayOneShot(playerHitSound);
                playerHealth -= 1;
            }
            if (col.tag == "bossSuper")
            {
                audioSource.PlayOneShot(playerHitSound);
                playerHealth -= 4;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "treasure")
        {
            Debug.Log("coin");
            treasureVal++;
            treasureNum.text = treasureVal.ToString();
        }
    }
}