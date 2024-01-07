using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    private float fixedDeltaTime;

    // Start is called before the first frame update
    void Awake()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            Time.timeScale = 0.4f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
        Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
    }
}
