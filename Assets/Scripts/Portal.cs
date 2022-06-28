using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Portal : MonoBehaviour
    {
        public Animator myAnim;

        bool opened = false;


        // Start is called before the first frame update
        void Start()
        {
            myAnim = GetComponent<Animator>();
            myAnim.speed = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (opened && !AnimatorIsPlaying())
            {
                //ProceduralGeneration proceduralGeneration = (proceduralGeneration)GameObject.Find("ProceduralGeneration").GetComponent(typeof(ProceduralGeneration));
                //proceduralGeneration.GenerateDungeon(0, 0);
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col != null && col.gameObject != null && col.gameObject.tag == "Player")
            {

                myAnim.speed = 1;
                opened = true;
            }
        }

        bool AnimatorIsPlaying()
        {
            return myAnim.GetCurrentAnimatorStateInfo(0).length >
                   myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

    }
}