using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : LVLManagerLoader<TowerManager>
{
    Buttons _towerBTNPressed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectedTower(Buttons _towerSelected)
    {
        _towerBTNPressed = _towerSelected;
        Debug.Log("yep" + _towerBTNPressed.gameObject);
    }
}
