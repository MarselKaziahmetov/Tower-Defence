using UnityEngine;

public enum projectileType
{
    arrow,
    rock,
    fireball
};

public class Projectile : MonoBehaviour
{
    [SerializeField] private int _attackDamage;
    [SerializeField] private projectileType _projType;

    public int AttackDamage
    {
        get
        {
            return _attackDamage;
        }
    }
    public projectileType ProjectileType
    {
        get
        {
            return _projType;
        }
    }
}
