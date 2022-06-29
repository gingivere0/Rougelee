using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Chest : MonoBehaviour
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
                Treasure treasure = (Treasure)GameObject.Find("Canvas").GetComponent(typeof(Treasure));
                ChestDrop cd = new ChestDrop();
                treasure.Activate(cd);
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (myAnim == null)
            {
                myAnim = GetComponent<Animator>();
            }
            if (col != null && col.gameObject != null && col.gameObject.tag == "Player")
            {

                myAnim.speed = 1;
                opened = true;
            }
        }

        bool AnimatorIsPlaying()
        {
            if (myAnim != null)
            {
                return myAnim.GetCurrentAnimatorStateInfo(0).length >
                       myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            }
            return false;
        }

    }
}