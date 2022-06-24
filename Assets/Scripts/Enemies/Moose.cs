using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Moose : Enemy
    {
        private void Awake()
        {
            base.hp = 20;
            base.xpdrop = 20;
        }
    }
}
