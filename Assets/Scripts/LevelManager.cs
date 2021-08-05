using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    public GameObject _spawnPoint;
    public GameObject[] _enemiesForm;
    public int _maxEnemiesOnScreen;
    public int _totalEnemies;
    public int _enemiesPerSpawn;    //количество спавнящихся противников за раз 

    private int _enemiesOnScreen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        if ( (_enemiesPerSpawn > 0) && (_enemiesOnScreen < _totalEnemies) )
        {
            for (int i = 0; i < _enemiesPerSpawn; i++)
            {
                if (_enemiesOnScreen < _maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(_enemiesForm[0]) as GameObject;
                    newEnemy.transform.position = _spawnPoint.transform.position;
                    _enemiesOnScreen++;
                }
            }
        }
    }
}
