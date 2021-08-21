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
    [SerializeField] private EnemyController[] _enemiesForm;  //разновидность врагов
    [SerializeField] private int _totalEnemies = 5;
    [SerializeField] private int _enemiesPerSpawn;    //количество спавнящихся противников за раз 

    public List<EnemyController> EnemyList = new List<EnemyController>();

    private const float _spawnDelay= 0.5f; // задержка между спавнов противников
   
    private int _waveNumber = 0;
    private int _totalMoney = 50;
    private int _totalEscaped = 0;
    private int _roundEscaped = 0;
    private int _totalKilled = 0;
    private int _whichEnemySpawned;
    int _enemiesToSpawn = 0;
    private AudioSource _audioSource;
    private GameStatus _currentStatus = GameStatus.play;

    public int TotalEscaped
    {
        get
        {
            return _totalEscaped;
        }
        set
        {
            _totalEscaped = value;
        }
    }

    public int RoundEscaped
    {
        get
        {
            return _roundEscaped;
        }
        set
        {
            _roundEscaped = value;
        }
    }

    public int TotalKilled
    {
        get
        {
            return _totalKilled;
        }
        set
        {
            _totalKilled = value;
        }
    }

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

    public AudioSource AudioSource
    {
        get
        {
            return _audioSource;
        }
    }

    private void Start()
    {
        _playBTN.gameObject.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
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
                if (EnemyList.Count < _totalEnemies)
                {
                    EnemyController newEnemy = Instantiate(_enemiesForm[Random.Range(0,_enemiesToSpawn)]) as EnemyController;
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

    public void IsWaveOver()
    {
        _totalEscapedText.text = "Escaped " + TotalEscaped + "/10";

        if ((RoundEscaped + TotalKilled) == _totalEnemies)
        {
            if (_waveNumber <= _enemiesForm.Length)
            {
                _enemiesToSpawn = _waveNumber;
            }
            SetCurrentGameState();
            ShowMenu();
        }
    }

    public void SetCurrentGameState()
    {
        if (_totalEscaped >= 10)
        {
            _currentStatus = GameStatus.gameover;
        }
        else if ( _waveNumber == 0 && (RoundEscaped + TotalKilled) == 0 )
        {
            _currentStatus = GameStatus.play;
        }
        else if (_waveNumber >= _totalWaves)
        {
            _currentStatus = GameStatus.win;
        }
        else
        {
            _currentStatus = GameStatus.next;
        }
    }

    public void PlayButtonPressed()
    {
        switch (_currentStatus)
        {
            case GameStatus.next:
                _waveNumber++;
                _totalEnemies += _waveNumber;
                break;

            default:
                _totalEnemies = 5;
                TotalEscaped = 0;
                TotalMoney = 50;
                _enemiesToSpawn = 0;
                TowerManager._Instance.DestroyAllTowers();
                TowerManager._Instance.RenameTagBuildSite();
                _totalMoneyText.text = TotalMoney.ToString();
                _totalEscapedText.text = "Escaped " + TotalEscaped + "/10";
                _audioSource.PlayOneShot(SoundManager._Instance.Newgame);
                break;
        }
        DestroyEnemies();
        TotalKilled = 0;
        RoundEscaped = 0;
        _currentWaveText.text = "Wave" + (_waveNumber + 1);
        StartCoroutine(SpawnEnemy());
        _playBTN.gameObject.SetActive(false);
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
                AudioSource.PlayOneShot(SoundManager._Instance.Gameover);
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
