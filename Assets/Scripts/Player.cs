using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float movementSpeed = 1;
    public int playerShifts = 0;
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
    public float freezeOnHit = 1f;
    public TrailRenderer trail;

    [SerializeField] private UIScript uiScript;
    [SerializeField] private PlayerSideDetector leftSide;
    [SerializeField] private PlayerSideDetector rightSide;
    [SerializeField] private PlayerSideDetector upSide;
    [SerializeField] private PlayerSideDetector downSide;
    [SerializeField] private int numOfShifts = 1;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private int fullHealth;
    private int treasureVal = 0;
    private bool isStopped = false;
    private Vector3 lastPos;
    private bool playerControlEnabled = true;
    private float testTime;
    private bool testBool = true;

    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        fullHealth = playerHealth;
        testTime = Time.time;
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*        // TODO: testing player on rails movement
                if (Time.time - testTime > 0.5 && testBool)
                {
                    testBool = false;
                    PlayerOnRails(new Vector3(-1.37f, 8.61999989f, 0));
                }*/
        if (!Input.GetButton("left") && !Input.GetButton("right"))
        {
            if (rb.velocity.x < deadzone && rb.velocity.x >= 0 || rb.velocity.x > -deadzone && rb.velocity.x <= 0)
            {
                Debug.Log("deadzone x stop");
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        if (!Input.GetButton("up") && !Input.GetButton("down"))
        {
            if (rb.velocity.y < deadzone && rb.velocity.y >= 0 || rb.velocity.y > -deadzone && rb.velocity.y <= 0)
            {
                Debug.Log("deadzone y stop");
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }

        if (transform.position == lastPos)
        {
            lastPos = transform.position;
            isStopped = true;
        }
        else
        {
            lastPos = transform.position;
            isStopped = false;
        }
        
        if (playerControlEnabled)
        {
            //PlayerRotate();
            PlayerMove();
        }

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

        /*        if (Input.GetButton("left") && camPos.x > 0 && !leftSide.contact)
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
                }*/

        if (Input.GetButton("left") && camPos.x > 0 && !leftSide.contact && rb.velocity.x > -maxSpeed)
        {
            rb.AddForce(new Vector2(-accel, 0));
        }
        else if (Input.GetButton("right") && camPos.x < 1 && !rightSide.contact && rb.velocity.x < maxSpeed)
        {
            rb.AddForce(new Vector2(accel, 0));
        }
        if (Input.GetButton("up") && camPos.y < 1 && !upSide.contact && rb.velocity.y < maxSpeed)
        {
            rb.AddForce(new Vector2(0, accel));
        }
        else if (Input.GetButton("down") && camPos.y > 0 && !downSide.contact && rb.velocity.y > -maxSpeed)
        {
            rb.AddForce(new Vector2(0, -accel));
        }

/*        if (Input.GetButtonUp("left") || Input.GetButtonUp("right"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Input.GetButtonUp("up") || Input.GetButtonUp("down"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
*/
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
    }

    public void PlayerOnRails(Vector3 location)
    {
        Vector3 moveTo = location;
        StartCoroutine(PlayerAutoMove(moveTo));
    }

    IEnumerator PlayerAutoMove(Vector3 location)
    {
        while (transform.position != location)
        {
            transform.position = Vector3.MoveTowards(transform.position, location, movementSpeed * Time.deltaTime);
            yield return new WaitForNextFrameUnit();
        }
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
        if (Input.GetButtonDown("Fire1") && playerShifts < numOfShifts)
        {
            trail.enabled = true;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
            playerShifts += 1;
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("bossZone"))
        {
            followCam.enteredBossZone = true;
        }
 
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("treasure"))
        {
            treasureVal++;
            treasureNum.text = treasureVal.ToString();
        }
        if (!invincible)
        {
            if (col.gameObject.CompareTag("bossProjectile"))
            {
                audioSource.PlayOneShot(playerHitSound);
                playerHealth -= 1;
                PlayerHit();
            }
            if (col.gameObject.CompareTag("bossSuper"))
            {
                audioSource.PlayOneShot(playerHitSound);
                playerHealth -= 4;
                PlayerHit();
            }
        }
    }

    private void PlayerHit()
    {
        followCam.Shake();
    }

    public bool IsStopped()
    {
        return isStopped;
    }

    // Getters and Setters
    public bool GetPlayerInputEnabled()
    {
        return playerControlEnabled;
    }

    public void SetPlayerInputEnabled(bool set)
    {
        playerControlEnabled = set;
    }
}