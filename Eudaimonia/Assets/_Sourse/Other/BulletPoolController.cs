using System.Collections.Generic;
using UnityEngine;

public class BulletPoolController : MonoBehaviour
{
    [SerializeField] private List<Bullet> _bullets = new List<Bullet>();
    private int _curBulletCount = 0;

    public Bullet GetBullet()
    {
        if (_curBulletCount == _bullets.Count)
            _curBulletCount = 1;

        _curBulletCount++;
        return _bullets[_curBulletCount - 1];
        
    }
}
