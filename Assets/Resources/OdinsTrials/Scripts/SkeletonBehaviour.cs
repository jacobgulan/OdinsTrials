using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OdinsTrials
{
    public class SkeletonBehaviour : EnemyBehaviour
    {
        void Update()
        {
            agent.SetDestination(target.position);
            FlipDirection();
        }
    }
}