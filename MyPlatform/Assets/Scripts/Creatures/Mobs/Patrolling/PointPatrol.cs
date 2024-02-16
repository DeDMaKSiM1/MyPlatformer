using System.Collections;
using UnityEngine;
namespace MyPlatform.Creatures.Mobs.Patrolling
{
    public class PointPatrol : Patrol
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _treshold = 1f;

        private Creature _creature;
        private int _destanationPointIndex;

        private void Awake()
        {
            _creature = GetComponent<Creature>();
        }

        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (IsOnPoint())
                {
                    _destanationPointIndex = (int)Mathf.Repeat(_destanationPointIndex + 1, _points.Length);
                }
                var direction = _points[_destanationPointIndex].position - transform.position;
                direction.y = 0;
                _creature.SetDirection(direction.normalized);
                yield return null;
            }

        }

        private bool IsOnPoint()
        {
            return (_points[_destanationPointIndex].position - transform.position).magnitude < _treshold;
        }
    }
}

