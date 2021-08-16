using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _exit;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private float _navigation;
    [SerializeField] private int _health;

    private int _target;
    private Transform _enemyPosition;
    private float _navigationTime;
    private bool _isDead = false;
    private Collider2D _enemyCollider;
    private Animator _anim;

    public bool _IsDead 
    {
        get { return _isDead; }
    }

    private void Start()
    {
        _enemyPosition = GetComponent<Transform>();
        _enemyCollider = GetComponent<Collider2D>();
        _anim = GetComponent<Animator>();

        LevelManager._Instance.RegisterEnemy(this);
    }

    private void Update()
    {
        if (_wayPoints != null && _isDead == false)
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
            LevelManager._Instance.UnregisterEnemy(this );
        }
        else if (collision.tag == "Projectile")
        {
            Projectile _newProj = collision.gameObject.GetComponent<Projectile>();
            EnemyHit(_newProj.AttackDamage);
            Destroy(collision.gameObject); 
        }
    }

    public void EnemyHit(int _hitPoints)
    {
        if(_health - _hitPoints > 0)
        {
            _health -= _hitPoints;

            _anim.Play("Hurt");
        }
        else
        {
            Death();

            _anim.SetTrigger("DidDie");
        }
    }

    public void Death()
    {
        _isDead = true;
        _enemyCollider.enabled = false;
    }

}
