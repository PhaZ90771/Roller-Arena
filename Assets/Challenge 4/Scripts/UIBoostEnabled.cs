using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoostEnabled : MonoBehaviour
{
    private PlayerControllerX playerController;
    private Text textObj;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerControllerX>();
        textObj = GetComponent<Text>();
    }

    private void Update()
    {
        textObj.text = playerController.boostEnabled ? "Boost Enabled" : "Boost Disabled";
        textObj.color = playerController.boostEnabled ? Color.green : Color.gray;
    }
}
