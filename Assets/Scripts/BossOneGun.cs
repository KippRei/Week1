// Controls shooting of projectiles
// Fire 1 = Normal Shot
// Fire 2 = Super Shot

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Reflection;
using Unity.VisualScripting;

public class BossOneGun : MonoBehaviour
{
    public Player player;
    public GameObject bossProjectile;
    public GameObject bossSuperProjectile;
    public int numOfSuperProj = 3;
    public float superProjInterval = 0.75f;
    public float bossFireRate = 0.3f; // Time before next normal boss shot
    public float nextBossSuper = 3.0f; // Time before next boss super is allowed
    public AudioClip shootSound;
    public AudioClip superSound;

    private float bossTime = 0.0f;
    private float bossSuper = 0.0f;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        GunDirection();
        bossTime += Time.deltaTime;
        bossSuper += Time.deltaTime;

        if (bossTime >= bossFireRate)
        {
            Instantiate(bossProjectile, transform.position, transform.rotation);
            audioSource.PlayOneShot(shootSound);
            bossTime = 0;
        }
        if (bossSuper >= nextBossSuper)
        {
            StartCoroutine(FireSuper());
        }
    }

    IEnumerator FireSuper()
    {
        for (int i = 0; i < numOfSuperProj; i++)
        {
            Instantiate(bossSuperProjectile, transform.position, transform.rotation);
            audioSource.PlayOneShot(superSound);
            bossSuper = 0;
            yield return new WaitForSeconds(superProjInterval);
        }
        
    }

    void GunDirection()
    {
        Vector2 playerPosition = player.transform.position;
        float yCoord = playerPosition.y - transform.position.y;
        float xCoord = playerPosition.x - transform.position.x;
        float rotateToRad = Mathf.Atan(yCoord / xCoord);
        float toAdd = -90;
        if (playerPosition.x < transform.position.x)
        {
            toAdd = 90;
        }
        float rotateToDeg = rotateToRad * (180 / Mathf.PI) + toAdd;
        transform.rotation = Quaternion.Euler(0, 0, rotateToDeg);
    }
}