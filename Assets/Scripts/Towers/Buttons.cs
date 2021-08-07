using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject _towerObject;
    [SerializeField] Sprite _phantomSprite;

    public GameObject _TowerObject
    {
        get
        {
            return _towerObject;
        }
    }

    public Sprite _PhantomSprite
    {
        get
        {
            return _phantomSprite;
        }
    }
}
