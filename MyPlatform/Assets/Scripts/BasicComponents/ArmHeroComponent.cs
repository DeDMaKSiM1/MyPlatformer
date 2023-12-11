using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyPlatform
{
    public class ArmHeroComponent : MonoBehaviour
    {
   

        public void ArmHero(GameObject go)
        {
           var hero = go.GetComponent<Hero>();
            if (hero != null)
            {
                hero.ArmHero();
            }
        }
    }
}

