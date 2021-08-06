using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : LVLManagerLoader<LevelManager>
{
    public GameObject _spawnPoint;
    public GameObject[] _enemiesForm;  //разновидность врагов
    public int _maxEnemiesOnScreen;
    public int _totalEnemies;
    public int _enemiesPerSpawn;    //количество спавнящихся противников за раз 

    private const float _spawnDelay= 0.5f; // задержка между спавнов противников
    private int _enemiesOnScreen;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
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

            yield return new WaitForSeconds(_spawnDelay);
            StartCoroutine(SpawnEnemy());
        }
    }

    public void RemoveEnemyFromScreen()
    {
        if (_enemiesOnScreen > 0)
        {
            _enemiesOnScreen--;
        }
    }
}
