using MyPlatform.Components.ColliderBased;
using MyPlatform.Components.GOBased;
using MyPlatform.Utils;
using UnityEngine;

namespace MyPlatform.Creatures.Mobs
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] private ColliderCheck _vision;


        [Header("Melee")]
        [SerializeField] private Cooldown _meleeCooldown;
        [SerializeField] private CheckCircleOverLap _meleeAttack;
        [SerializeField] private ColliderCheck _meleeCanAttack;


        [Header("Range")]
        [SerializeField] private Cooldown _rangeCooldown;
        [SerializeField] private SpawnComponent _rangeAttack;

        private Animator _animator;

        private static readonly int RangeKey = Animator.StringToHash("range");
        private static readonly int MeleeKey = Animator.StringToHash("melee");


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_vision.IsTouchingLayer)
            {
                if (_meleeCanAttack.IsTouchingLayer)
                {
                    if (_meleeCooldown.IsReady)
                        MeleeAttack();
                    return;
                }

                if (_rangeCooldown.IsReady)
                {
                    RangeAttack();
                }
            }
        }

        private void MeleeAttack()
        {
            _meleeCooldown.Reset();
            _animator.SetTrigger(MeleeKey);
        }

        private void RangeAttack()
        {
            _rangeCooldown.Reset();
            _animator.SetTrigger(RangeKey);
        }
 
        public void OnMeleeAttack()
        {
            _meleeAttack.Check();
        }
        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }

    }
}

