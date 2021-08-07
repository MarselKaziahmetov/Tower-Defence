using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : LVLManagerLoader<TowerManager>
{
    Buttons _towerBTNPressed;
    SpriteRenderer _selectedTowerSprite;

    void Start()
    {
        _selectedTowerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 _mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D _hit = Physics2D.Raycast(_mousePoint, Vector2.zero);

            if (_hit.collider.tag == "TowerPoints")
            {
                _hit.collider.tag = "TowerPointFull"; 

                PlaceTower(_hit);
            }
        }
        
        if (_selectedTowerSprite.enabled)
        {
            FollowMousePhantom();
        }
    }

    public void PlaceTower(RaycastHit2D _hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && _towerBTNPressed != null)
        {
            GameObject _newTower = Instantiate(_towerBTNPressed._TowerObject);
            _newTower.transform.position = _hit.transform.position;
            DisabledDragPhantom();
        }
    }

    public void SelectedTower(Buttons _towerSelected)
    {
        _towerBTNPressed = _towerSelected;
        EnabledDragPhantom(_towerBTNPressed._PhantomSprite);

        Debug.Log("yep" + _towerBTNPressed.gameObject);
    }

    public void FollowMousePhantom()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void EnabledDragPhantom(Sprite _towerPhantom)
    {
        _selectedTowerSprite.enabled = true;
        _selectedTowerSprite.sprite = _towerPhantom;
    }

    public void DisabledDragPhantom()
    {
        _selectedTowerSprite.enabled = false;
    }
}
