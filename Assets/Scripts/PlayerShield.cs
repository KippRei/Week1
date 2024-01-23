using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public int shieldHP = 15;
    private Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.Find("Gun").GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldHP <= 0 || Input.GetButtonDown("Shield Off"))
        {
            gun.shieldCount--;
            gun.shieldCountDisp.text = gun.shieldCount.ToString();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "enemy")
        {
            shieldHP -= 1;
        }      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "enemy")
        {
            shieldHP -= 1;
        }
    }
}
