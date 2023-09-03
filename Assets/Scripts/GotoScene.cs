using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour
{
    public uint SceneIndex;

    public void Goto()
    {
        SceneManager.LoadScene((int)SceneIndex);
    }
}
