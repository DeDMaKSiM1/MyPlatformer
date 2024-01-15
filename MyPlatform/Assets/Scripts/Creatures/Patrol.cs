using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyPlatform.Creatures
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }
}

