using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVLManagerLoader : MonoBehaviour
{
    public GameObject _lvlManager;

    private void Awake()
    {
        if (LevelManager.instance == null)
        {
            Instantiate(_lvlManager);
        }
    }
}