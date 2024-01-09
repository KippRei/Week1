// Controls shooting of projectiles
// Fire 1 = Normal Shot
// Fire 2 = Super Shot

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Reflection;
using Unity.VisualScripting;

public class Gun : MonoBehaviour
{
    public Player player;
    public GameObject projectile;
    public LockOnProjectile lockOnProjectile;
    public float nextFire = 0.18f; // Time before next normal shot is allowed
    public float nextSuper = 1.0f; // Time before next super is allowed
    public int superNum = 12;
    public GameObject cross;

    private float myTime = 0.0f;
    private float mySuper = 0.0f;
    private bool shiftPos = false;
    private List<GameObject> targetedEnemies = new List<GameObject>();
    private List<GameObject> crosses = new List<GameObject>(); // TODO: This is a hacky way of showing crosses when targeting that can later be erased, needs work

    void Update()
    {
        myTime += Time.deltaTime;
        mySuper += Time.deltaTime;

        if (Input.GetButton("Fire3"))
        {
            shiftPos = true;
        }
        else
        {
            shiftPos = false;
        }

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

            Instantiate(projectile, transform.position, transform.rotation);
            // Reset timer for next shot
            myTime = 0;
        }

        else if (Input.GetButton("Fire3"))
        {
            player.PlayerShift();
        }
        if (Input.GetButtonUp("Fire3"))
        {
            player.playerShifted = false;
        }

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
            FireSuper();
            mySuper = 0;
        }
    }

    void FireSuper()
    {
        float missileAngleInterval = 360 / superNum;
        float missileAngle = 0f;
        Quaternion launchAtAngle = Quaternion.Euler(0, 0, missileAngle);

        if (targetedEnemies.Count == 0)
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
        }
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
        /*    Debug.Log("Mouse Position x = " + mousePosition.x);
            Debug.Log("Mouse Position y = " + mousePosition.y);
            Debug.Log("Player Position x = " + playerPosition.x);
            Debug.Log("Player Position y = " + playerPosition.y);
            Debug.Log("Angle in Rads = " + rotateToRad);
            Debug.Log("Angle in Degrees = " + rotateToDeg);*/
        transform.rotation = Quaternion.Euler(0, 0, rotateToDeg);
    }
}