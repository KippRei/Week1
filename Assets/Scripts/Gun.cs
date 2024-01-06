// Controls shooting of projectiles
// Fire 1 = Normal Shot
// Fire 2 = Super Shot

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Reflection;

public class Gun : MonoBehaviour
{
    public GameObject projectile;
    public LockOnProjectile lockOnProjectile;
    public float nextFire = 0.18f; // Time before next normal shot is allowed
    public float nextSuper = 1.0f; // Time before next super is allowed
    public int superNum = 12;
    public GameObject cross;

    private float myTime = 0.0f;
    private float mySuper = 0.0f;


    void Update()
    {
        
        myTime += Time.deltaTime;
        mySuper += Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            GunDirection(); 

            // Checks to see if enemy is clicked for missile lock-on
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);
            if (hit != null && hit.tag == "enemy")
            {
                GameObject crosshairs = Instantiate(cross, hit.gameObject.transform.position, hit.gameObject.transform.rotation);
                LockOnProjectile missile = Instantiate<LockOnProjectile>(lockOnProjectile, transform.position, transform.rotation);
                missile.SetEnemy(hit.gameObject);
                missile.SetCrosshairs(crosshairs);
            }
            else
            {
                Instantiate(projectile, transform.position, transform.rotation);
            }

            // Reset timer for next shot
            myTime = 0;
        }

        if (Input.GetButton("Fire2") && mySuper > nextSuper)
        {
            FireSuper();
            mySuper = 0;
        }
    }

    void FireSuper()
    {
        float missileAngleInterval = 360 / superNum;
        float missileAngle = 0f;
        Quaternion launchAtAngle = Quaternion.Euler(0, 0, missileAngle);

        // Checks to see if enemy is clicked for missile lock-on
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);
        if (hit != null && hit.tag == "enemy")
        {
            for (int i = 0; i < superNum + 1; i++)
            {
                GameObject crosshairs = Instantiate(cross, hit.gameObject.transform.position, hit.gameObject.transform.rotation);
                LockOnProjectile missile = Instantiate<LockOnProjectile>(lockOnProjectile, transform.position, launchAtAngle);
                missile.SetEnemy(hit.gameObject);
                missile.SetCrosshairs(crosshairs);
                launchAtAngle = Quaternion.Euler(0, 0, missileAngle);
                missileAngle += missileAngleInterval;
            }
        }
        else
        {
            for (int i = 0; i < superNum + 1; i++)
            {
                Instantiate(projectile, transform.position, launchAtAngle);
                launchAtAngle = Quaternion.Euler(0, 0, missileAngle);
                missileAngle += missileAngleInterval;
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