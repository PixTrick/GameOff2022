using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    BulletModifiers BulletModifiers { get; set; }
    void ApplyDamage(EnemyBehaviour enemy);

    void NaturalDamageBuff(int level);
}
