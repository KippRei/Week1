using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{
    private TextMeshProUGUI debugText;
    // Start is called before the first frame update
    void Start()
    {
        debugText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = Time.fixedDeltaTime.ToString();
    }
}
