using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class CameraMovement : MonoBehaviour
    {

        public float movespeed = 5f;
        float xInput, yInput;

        Vector2 targetPos;

        Rigidbody2D rb;

        SpriteRenderer sp;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            sp = GetComponent<SpriteRenderer>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 10f;

            if (Input.GetMouseButtonDown(0))
            {
                targetPos = mousePos;
            }

            //transform.position = mousePos;
        }

        private void FixedUpdate()
        {
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");

            transform.Translate(xInput * movespeed, yInput * movespeed, 0);

            PlatformerMove();
        }

        void PlatformerMove()
        {
            rb.velocity = new Vector2(movespeed * xInput, rb.velocity.y);

        }



    }

}