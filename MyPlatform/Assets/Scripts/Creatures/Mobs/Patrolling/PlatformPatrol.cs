using MyPlatform.Components.ColliderBased;
using System.Collections;
using UnityEngine;


namespace MyPlatform.Creatures.Mobs.Patrolling
{
    public class PlatformPatrol : Patrol
    {
        [SerializeField] LineCastCheck _platformCheck; 
        [SerializeField] LineCastCheck _otherThingsCheck; 
        [SerializeField] private Creature _creature;
        [SerializeField] private int _direction;

        public override IEnumerator DoPatrol()
        {
            while(enabled)
            {
                if (_platformCheck.IsTouchingLayer && !_otherThingsCheck.IsTouchingLayer )
                {
                    _creature.SetDirection(new Vector2(_direction, 0));
                }
                else
                {
                    
                    _direction = -_direction;
                    _creature.SetDirection(new Vector2(_direction, 0));
                }
                yield return null;
            }         
        }
    }
}

