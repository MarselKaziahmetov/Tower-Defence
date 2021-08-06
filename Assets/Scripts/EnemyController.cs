using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _exit;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private float _navigation;

    private int _target;
    private Transform _enemyPosition;
    private float _navigationTime;

    private void Start()
    {
        _enemyPosition = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_wayPoints != null)
        {
            _navigationTime += Time.deltaTime;

            if (_navigationTime > _navigation)
            {
                if (_target < _wayPoints.Length)
                {
                    _enemyPosition.position = Vector2.MoveTowards(_enemyPosition.position, _wayPoints[_target].position, _navigationTime);
                }
                else
                {
                    _enemyPosition.position = Vector2.MoveTowards(_enemyPosition.position, _exit.position, _navigationTime);
                }

                _navigationTime = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ControllPoint")
        {
            _target++;
        }
        else if (collision.tag == "Finish")
        {
            LevelManager._Instance.RemoveEnemyFromScreen();
            Destroy(gameObject);
        }
    }

}
