using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Sword : ShotProjectile
    {

       
        // Start is called before the first frame update
        void Start()
        {
            movespeed = 0;
            damage = 20;
        }
    }
}