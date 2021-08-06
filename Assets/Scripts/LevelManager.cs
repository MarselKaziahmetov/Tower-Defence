using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : LVLManagerLoader<LevelManager>
{
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject[] _enemiesForm;  //������������� ������
    [SerializeField] private int _maxEnemiesOnScreen;
    [SerializeField] private int _totalEnemies;
    [SerializeField] private int _enemiesPerSpawn;    //���������� ����������� ����������� �� ��� 

    private const float _spawnDelay= 0.5f; // �������� ����� ������� �����������
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
