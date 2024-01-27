// Controls shooting of projectiles
// Fire 1 = Normal Shot
// Fire 2 = Super Shot

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Reflection;
using Unity.VisualScripting;
using TMPro;

public class Gun : MonoBehaviour
{
    public Player player;
    public GameObject projectile;
    public Axe axe;
    public GameObject terrainBlock;
    public LockOnProjectile lockOnProjectile;
    public float nextFire = 0.18f; // Time before next normal shot is allowed
    public float nextSuper = 1.0f; // Time before next super is allowed
    public int superNum = 12;
    public GameObject cross;
    public AudioClip shootSound;
    public AudioClip superSound;
    public TextMeshProUGUI shieldCountDisp;
    public int maxShields = 2;

    private Queue<GameObject> projectiles = new Queue<GameObject>();
    private List<GameObject> projectilesFired = new List<GameObject>();
    private float myTime = 0.0f;
    private float mySuper = 0.0f;
    private bool shiftPos = false;
    private List<GameObject> targetedEnemies = new List<GameObject>();
    private List<GameObject> crosses = new List<GameObject>(); // TODO: This is a hacky way of showing crosses when targeting that can later be erased, needs work
    private AudioSource audioSource;
    
    public int shieldCount = 0; // Used by PlayerShield


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        shieldCountDisp.text = shieldCount.ToString();
    }

    void Update()
    {
        if (player.GetPlayerInputEnabled())
        {
            myTime += Time.deltaTime;
            mySuper += Time.deltaTime;

            CheckSlowMoButton();
            CheckShieldButton();
            CheckFireButton();
            CheckSwingButton();
            CheckSuperButton();
        }
    }

    private void CheckSwingButton()
    {
        if (Input.GetButtonDown("axe"))
        {
            GunDirection();
            axe.SwingAxe(transform.position, transform.rotation);
        }
    }

    private void CheckSuperButton()
    {
        // If Fire2 is held, enter targeting mode
        /*        if (Input.GetButton("Fire2"))
                {
                    TargetMultiple();
                }

                if (Input.GetButtonDown("Fire2"))
                {
                    targetedEnemies.Clear();
                }*/

        if (Input.GetButton("Fire2") && mySuper > nextSuper && !shiftPos)
        {
            /*foreach (GameObject cross in crosses)
            {
                Destroy(cross);
            }
            crosses.Clear();*/
            audioSource.PlayOneShot(superSound);
            FireSuper();
            mySuper = 0;
        }
    }

    private void CheckFireButton()
    {
        if (Input.GetButton("Fire1") && myTime > nextFire && !shiftPos)
        {
            GunDirection();

            /*// Checks to see if enemy is clicked for missile lock-on
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);
            if (hit != null && hit.tag == "enemy")
            {
                GameObject crosshairs = Instantiate(cross, hit.gameObject.transform.position, hit.gameObject.transform.rotation);
                LockOnProjectile missile = Instantiate<LockOnProjectile>(lockOnProjectile, transform.position, transform.rotation);
                crosshairs.transform.parent = hit.gameObject.transform;
                missile.SetEnemy(hit.gameObject);
                missile.SetCrosshairs(crosshairs);
            }
            else
            {
                Instantiate(projectile, transform.position, transform.rotation);
            }*/

            audioSource.PlayOneShot(shootSound);
            Instantiate(projectile, transform.position, transform.rotation);

            // Reset timer for next shot
            myTime = 0;
        }
    }

    private void CheckShieldButton()
    {
        if (Input.GetButtonDown("shield") && shieldCount < maxShields)
        {
            shieldCount++;
            shieldCountDisp.text = shieldCount.ToString();
            GunDirection();
            Quaternion rotateTo = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90);
            Instantiate(terrainBlock, transform.position, rotateTo);
        }
    }

    private void CheckSlowMoButton()
    {
        if (Input.GetButton("Fire3"))
        {
            shiftPos = true;
            player.PlayerShift();
        }
        else
        {
            shiftPos = false;
        }

        if (Input.GetButtonUp("Fire3"))
        {
            player.playerShifts = 0; // Let's player know to reset phase shift counter (default 1 phase shift per slow-mo button press)
        }
    }

    void FireSuper()
    {
        float missileAngleInterval = 360 / superNum;
        float missileAngle = 0f;
        Quaternion launchAtAngle = Quaternion.Euler(0, 0, missileAngle);

        for (int i = 0; i < superNum + 1; i++)
        {
            Instantiate(projectile, player.transform.position, launchAtAngle);
            launchAtAngle = Quaternion.Euler(0, 0, missileAngle);
            missileAngle += missileAngleInterval;
        }

       /* if (targetedEnemies.Count == 0)
        {
            for (int i = 0; i < superNum + 1; i++)
            {
                Instantiate(projectile, transform.position, launchAtAngle);
                launchAtAngle = Quaternion.Euler(0, 0, missileAngle);
                missileAngle += missileAngleInterval;
            }
        }
        else
        {
            int targetNum = targetedEnemies.Count;
            int enemyTargeted = 0;
            for (int i = 0; i < superNum + 1; i++)
            {
                LockOnProjectile missile = Instantiate<LockOnProjectile>(lockOnProjectile, transform.position, launchAtAngle);
                GameObject crosshairs = Instantiate(cross, targetedEnemies[enemyTargeted].gameObject.transform.position, targetedEnemies[enemyTargeted].gameObject.transform.rotation);
                crosshairs.transform.parent = targetedEnemies[enemyTargeted].gameObject.transform;
                missile.SetEnemy(targetedEnemies[enemyTargeted]);
                missile.SetCrosshairs(crosshairs);
                launchAtAngle = Quaternion.Euler(0, 0, missileAngle);
                missileAngle += missileAngleInterval;
                enemyTargeted++;

                if (enemyTargeted >= targetNum)
                {
                    enemyTargeted = 0;
                }
            }
        }*/
    }

    void TargetMultiple()
    {
        // Checks to see if enemy is clicked for missile lock-on
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);
        bool inList = false;
        if (hit != null && hit.tag == "enemy")
        {
           foreach (GameObject enemy in targetedEnemies)
            {
                if (hit.gameObject.name == enemy.gameObject.name)
                {
                    inList = true;
                }
            }
            if (inList == false)
            {
                GameObject crosshairs = Instantiate(cross, hit.gameObject.transform.position, hit.gameObject.transform.rotation);
                crosshairs.transform.parent = hit.gameObject.transform;
                crosses.Add(crosshairs);
                targetedEnemies.Add(hit.gameObject);
            }
            else
            {
                inList = false;
            }
        }
    }

    void GunDirection()
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
}