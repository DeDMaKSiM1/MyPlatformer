using MyPlatform.Components.ColliderBased;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyPlatform.Creatures.Mobs.Patrolling
{
    public class PlatformPatrol : Patrol
    {
        [SerializeField] LayerCheck _platformCheck;
        [SerializeField] private float _treshold = 1f;
        [SerializeField] private float _direction = 1f;
        [SerializeField] private Creature _creature;

        private Vector2 vect;




        private void Awake()
        {
            _creature = GetComponent<Creature>();
            vect = new Vector2(_direction, 0);
        }

        public override IEnumerator DoPatrol()
        {

            while(enabled)
            {
                if (_platformCheck)
                {
                    _creature.SetDirection(vect);

                }
                else
                {
                    //OnPoint();
                    _direction = -_direction;
                    _creature.SetDirection(vect);
                }

            }

            yield return null;
        }

        //private bool OnPoint()
        //{
        //    return vect.magnitude < _treshold;
        //}

    }
}

