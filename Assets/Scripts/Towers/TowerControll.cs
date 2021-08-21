using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControll : MonoBehaviour
{
    [SerializeField] private float _timeBetweenAttacks;
    [SerializeField] private float _attackRadius;
    [SerializeField] private Projectile _projectile;
    private EnemyController _enemyTarget;
    private float _attackCounter;
    private bool _isAttaking = false;

    private void Update()
    {
        if (_isAttaking == true)
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        _attackCounter -= Time.deltaTime;

        if (_enemyTarget == null || _enemyTarget._IsDead)
        {
            EnemyController _nearestEnemy = GetNearestEnemy();
            if (_nearestEnemy != null && Vector2.Distance(transform.localPosition, _nearestEnemy.transform.localPosition) <= _attackRadius)
            {
                _enemyTarget = _nearestEnemy;
            }
        }
        else
        {
            if (_attackCounter <= 0)
            {
                _isAttaking = true;

                _attackCounter = _timeBetweenAttacks;
            }
            else
            {
                _isAttaking = false;
            }

            if (Vector2.Distance(transform.localPosition, _enemyTarget.transform.localPosition) > _attackRadius)
            {
                _enemyTarget = null;
            }
        }
    }

    public void Attack()
    {
        _isAttaking = false;

        Projectile _newProjectile = Instantiate(_projectile) as Projectile;
        _newProjectile.transform.localPosition = transform.localPosition;

        if (_newProjectile.ProjectileType == projectileType.arrow)
        {
            LevelManager._Instance.AudioSource.PlayOneShot(SoundManager._Instance.Arrow);
        }
        else if (_newProjectile.ProjectileType == projectileType.fireball)
        {
            LevelManager._Instance.AudioSource.PlayOneShot(SoundManager._Instance.Fireball);
        }
        else if (_newProjectile.ProjectileType == projectileType.rock)
        {
            LevelManager._Instance.AudioSource.PlayOneShot(SoundManager._Instance.Rock);
        }

        if (_enemyTarget == null)
        {
            Destroy(_newProjectile);
        }
        else
        {
            //move proj to enemy
            StartCoroutine(MoveProjectile(_newProjectile));
        }
    }

    IEnumerator MoveProjectile(Projectile _projectile)
    {
        while (GetTargetDistance(_enemyTarget) > 0.2f && _projectile != null && _enemyTarget != null)
        {
            var _dir = _enemyTarget.transform.localPosition - transform.localPosition;
            var _angleDirection = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
            _projectile.transform.rotation = Quaternion.AngleAxis(_angleDirection, Vector3.forward);
            _projectile.transform.localPosition = Vector2.MoveTowards(_projectile.transform.localPosition, _enemyTarget.transform.localPosition, 5f * Time.deltaTime);
            yield return null;
        }

        if (_projectile != null || _enemyTarget == null)
        {
            Destroy(_projectile);
        }
    }

    private float GetTargetDistance(EnemyController _thisEnemy)
    {
        if (_thisEnemy == null)
        {
            _thisEnemy = GetNearestEnemy();
            if (_thisEnemy == null)
            {
                return 0f;
            }
        }

        return Mathf.Abs(Vector2.Distance(transform.localPosition, _thisEnemy.transform.localPosition));
    }

    private List<EnemyController> GetEnemiesInRange()
    {
        List<EnemyController> enemiesInRange = new List<EnemyController>();

        foreach (EnemyController _enemy in LevelManager._Instance.EnemyList)
        {
            if (Vector2.Distance(transform.localPosition, _enemy.transform.localPosition) <= _attackRadius)
            {
                enemiesInRange.Add(_enemy);
            }
        }

        return enemiesInRange;
    }

    private EnemyController GetNearestEnemy()
    {
        EnemyController _nearestEnemy = null;
        float _smallestDistance = float.PositiveInfinity;

        foreach (EnemyController _enemy in GetEnemiesInRange())
        {
            if (Vector2.Distance(transform.localPosition, _enemy.transform.localPosition) < _smallestDistance)
            {
                _smallestDistance = Vector2.Distance(transform.localPosition, _enemy.transform.localPosition);
                _nearestEnemy = _enemy;
            }
        }

        return _nearestEnemy;
    }
}
