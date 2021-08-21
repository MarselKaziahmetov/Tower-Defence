using UnityEngine;

public class SoundManager : LVLManagerLoader<SoundManager>
{
    [SerializeField] AudioClip _arrow;
    [SerializeField] AudioClip _death;
    [SerializeField] AudioClip _fireball;
    [SerializeField] AudioClip _gameover;
    [SerializeField] AudioClip _hit;
    [SerializeField] AudioClip _level;
    [SerializeField] AudioClip _newgame;
    [SerializeField] AudioClip _rock;
    [SerializeField] AudioClip _towerBuilt;

    public AudioClip Arrow
    {
        get
        {
            return _arrow;
        }
    }

    public AudioClip Death
    {
        get
        {
            return _death;
        }
    }

    public AudioClip Fireball
    {
        get
        {
            return _fireball;
        }
    }

    public AudioClip Gameover
    {
        get
        {
            return _gameover;
        }
    }

    public AudioClip Hit
    {
        get
        {
            return _hit;
        }
    }

    public AudioClip Level
    {
        get
        {
            return _level;
        }
    }

    public AudioClip Newgame
    {
        get
        {
            return _newgame;
        }
    }

    public AudioClip Rock
    {
        get
        {
            return _rock;
        }
    }

    public AudioClip TowerBuilt
    {
        get
        {
            return _towerBuilt;
        }
    }
}
