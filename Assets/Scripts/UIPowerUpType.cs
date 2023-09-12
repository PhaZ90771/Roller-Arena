using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPowerUpType : MonoBehaviour
{
    private TextMeshProUGUI txtObj;

    private PlayerController playerController;

    private void Awake()
    {
        txtObj = GetComponent<TextMeshProUGUI>();

        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        txtObj.text = playerController.PowerUpType;
    }
}
