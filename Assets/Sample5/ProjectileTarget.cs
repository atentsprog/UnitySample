using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample5.Projectile
{
    public class ProjectileTarget : MonoBehaviour
    {
        public static ProjectileTarget instance;
        private void Awake()
        {
            instance = this;
        }
    }
}