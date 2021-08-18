using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus
{
    next, 
    play, 
    gameover, 
    win
}

public class LevelManager : LVLManagerLoader<LevelManager>
{
    [SerializeField] private Text _totalMoneyText;
    [SerializeField] private Text _currentWaveText;
    [SerializeField] private Text _totalEscapedText; 
    [SerializeField] private Text _playBTNText;
    [SerializeField] private Button _playBTN;
    [SerializeField] private int _totalWaves = 10;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject[] _enemiesForm;  //разновидность врагов
    [SerializeField] private int _maxEnemiesOnScreen;
    [SerializeField] private int _totalEnemies;
    [SerializeField] private int _enemiesPerSpawn;    //количество спавнящихся противников за раз 

    public List<EnemyController> EnemyList = new List<EnemyController>();

    private const float _spawnDelay= 0.5f; // задержка между спавнов противников
   
    private int _waveNumber = 0;
    private int _totalMoney = 50;
    private int _totalEscaped = 0;
    private int _roundEscaped = 0;
    private int _totalKilled = 0;
    private int _whichEnemySpawned;
    GameStatus _currentStatus = GameStatus.play;

    public int TotalMoney
    {
        get
        {
            return _totalMoney;
        }
        set
        {
            _totalMoney = value;
            _totalMoneyText.text = _totalMoney.ToString();
        }
    }

    private void Start()
    {
        _playBTN.gameObject.SetActive(false);
        ShowMenu();
    }

    private void Update()
    {
        HandleEscape();
    }

    private IEnumerator SpawnEnemy()
    {
        if ( (_enemiesPerSpawn > 0) && (EnemyList.Count < _totalEnemies) )
        {
            for (int i = 0; i < _enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < _maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(_enemiesForm[0]) as GameObject;
                    newEnemy.transform.position = _spawnPoint.transform.position;
                }
            }

            yield return new WaitForSeconds(_spawnDelay);
            StartCoroutine(SpawnEnemy());
        }
    }

    public void RegisterEnemy(EnemyController _enemy)
    {
        EnemyList.Add(_enemy);
    }

    public void UnregisterEnemy(EnemyController _enemy)
    {
        EnemyList.Remove(_enemy);
        Destroy(_enemy.gameObject);
    }

    public void DestroyEnemies()
    {
        foreach (EnemyController _enemy in EnemyList )
        {
            Destroy(_enemy.gameObject);
        }

        EnemyList.Clear();
    }

    public void AddMoney(int _amount)
    {
        TotalMoney += _amount;
    }

    public void SubtractMoney(int _amount)
    {
        TotalMoney -= _amount;
    }

    public void ShowMenu()
    {
        switch (_currentStatus)
        {
            case GameStatus.next:
                _playBTNText.text = "Next Wave >>>";
                break;

            case GameStatus.play:
                _playBTNText.text = "Play game!!!";
                break;

            case GameStatus.gameover:
                _playBTNText.text = "Play again!";
                break;

            case GameStatus.win:
                _playBTNText.text = "Play game!!!";
                break;
        }
        _playBTN.gameObject.SetActive(true);
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager._Instance.DisabledDragPhantom();
            TowerManager._Instance._towerBTNPressed = null;
        }
    }
}
