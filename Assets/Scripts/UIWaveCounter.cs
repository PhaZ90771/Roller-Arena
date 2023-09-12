using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWaveCounter : MonoBehaviour
{
    private TextMeshProUGUI txtObj;

    private SpawnManager spawnManager;

    private void Awake()
    {
        txtObj = GetComponent<TextMeshProUGUI>();

        spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void Update()
    {
        txtObj.text = string.Format("Enemy Wave #{0}", spawnManager.WaveNumber);
    }
}
