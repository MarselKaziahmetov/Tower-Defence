using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] TowerControll _towerObject;
    [SerializeField] Sprite _phantomSprite;
    [SerializeField] int _towerPrice;

    public int TowerPrice
    {
        get
        {
            return _towerPrice;
        }
    }

    public TowerControll _TowerObject
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
