using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGroundMoveEnemy : EnemyBase
{
    void Update()
    {
       Turn();
    }

    void FixedUpdate()
    {
        Move();
    }
}
