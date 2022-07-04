using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class XPBar : MonoBehaviour
    {
        bool setup = true;

        // Start is called before the first frame update
        void Start()
        {
            SetXP(5f, 10f);
        }

        // Update is called once per frame
        void Update()
        {
            if (setup)
            {
                transform.localScale = new Vector2(0, 1f);
                setup = false;
            }
        }

        public void SetXP(float xp, float nextLvlXP)
        {
            transform.localScale = new Vector2(xp / nextLvlXP, 1f);
        }
    }
}