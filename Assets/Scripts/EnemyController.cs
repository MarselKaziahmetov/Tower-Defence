using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int _target;
    public Transform _exit;
    public Transform[] _wayPoints;
    public float _navigation;

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
            LevelManager._instance.RemoveEnemyFromScreen();
            Destroy(gameObject);
        }
    }

}
