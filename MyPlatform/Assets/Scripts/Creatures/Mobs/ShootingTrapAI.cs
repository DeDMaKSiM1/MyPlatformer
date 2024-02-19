using MyPlatform.Components.ColliderBased;
using MyPlatform.Components.GOBased;
using MyPlatform.Utils;
using UnityEngine;

namespace MyPlatform.Creatures.Mobs
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;


        [Header("Melee")]
        [SerializeField] private Cooldown _meleeCooldown;
        [SerializeField] private CheckCircleOverLap _meleeAttack;
        [SerializeField] private LayerCheck _meleeCanAttack;


        [Header("Range")]
        [SerializeField] private Cooldown _rangeCooldown;
        [SerializeField] private SpawnComponent _rangeAttack;

        
    }
}

