using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWaveType : MonoBehaviour
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
        var waveType = spawnManager.IsBossWave ? "Boss" : "Normal";
        txtObj.text = string.Format("{0} Wave", waveType);
        txtObj.color = spawnManager.IsBossWave ? Color.red : Color.blue;
    }
}
