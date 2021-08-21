using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : LVLManagerLoader<TowerManager>
{
    public Buttons _towerBTNPressed
    {
        get; set;
    }
    
    private SpriteRenderer _selectedTowerSprite;
    private List<TowerControll> TowerList = new List<TowerControll>();
    private List<Collider2D> BuildList = new List<Collider2D>();
    private Collider2D _buildTile;

    void Start()
    {
        _selectedTowerSprite = GetComponent<SpriteRenderer>();
        _buildTile = GetComponent<Collider2D>();
        _selectedTowerSprite.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 _mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D _hit = Physics2D.Raycast(_mousePoint, Vector2.zero);

            if (_hit.collider.tag == "TowerPoints")
            {
                _buildTile = _hit.collider;
                _buildTile.tag = "TowerPointFull";
                RegisterBuildState(_buildTile);
                PlaceTower(_hit);
            }
        }
        
        if (_selectedTowerSprite.enabled)
        {
            FollowMousePhantom();
        }
    }

    public void RegisterBuildState(Collider2D _buildTag)
    {
        BuildList.Add(_buildTag);
    }

    public void RegisterTower(TowerControll _tower)
    {
        TowerList.Add(_tower);
    }

    public void RenameTagBuildSite()
    {
        foreach (Collider2D _buildTag in BuildList)
        {
            _buildTag.tag = "TowerPoints";
        }
        BuildList.Clear();
    }

    public void DestroyAllTowers()
    {
        foreach (TowerControll _tower in TowerList)
        {
            Destroy(_tower.gameObject);
        }
        TowerList.Clear();
    }

    public void PlaceTower(RaycastHit2D _hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && _towerBTNPressed != null)
        {
            TowerControll _newTower = Instantiate(_towerBTNPressed._TowerObject);
            _newTower.transform.position = _hit.transform.position;
            BuyTower(_towerBTNPressed.TowerPrice);
            LevelManager._Instance.AudioSource.PlayOneShot(SoundManager._Instance.TowerBuilt);
            RegisterTower(_newTower);
            DisabledDragPhantom();
        }
    }

    public void BuyTower(int _price)
    {
        LevelManager._Instance.SubtractMoney(_price);
    }

    public void SelectedTower(Buttons _towerSelected)
    {
        if (_towerSelected.TowerPrice <= LevelManager._Instance.TotalMoney)
        {
            _towerBTNPressed = _towerSelected;
            EnabledDragPhantom(_towerBTNPressed._PhantomSprite);
        }
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
