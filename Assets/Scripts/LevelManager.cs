using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : LVLManagerLoader<LevelManager>
{
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject[] _enemiesForm;  //разновидность врагов
    [SerializeField] private int _maxEnemiesOnScreen;
    [SerializeField] private int _totalEnemies;
    [SerializeField] private int _enemiesPerSpawn;    //количество спавнящихся противников за раз 

    public List<EnemyController> EnemyList = new List<EnemyController>();

    private const float _spawnDelay= 0.5f; // задержка между спавнов противников

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
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
}
