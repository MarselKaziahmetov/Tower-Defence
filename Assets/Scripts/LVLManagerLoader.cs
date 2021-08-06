using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVLManagerLoader : MonoBehaviour
{
    public GameObject _lvlManager;

    private void Awake()
    {
        if (LevelManager._instance == null)
        {
            Instantiate(_lvlManager);
        }
    }
}
