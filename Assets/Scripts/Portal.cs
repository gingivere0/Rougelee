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
            myAnim.speed = 1;
        }

        // Update is called once per frame
        void Update()
        {
            if (opened)
            { 
                ProceduralGeneration.increaseLevel = true;
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            { 
                myAnim.SetTrigger("close");
                opened = true;
            }
        }
    }

}