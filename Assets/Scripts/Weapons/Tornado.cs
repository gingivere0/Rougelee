using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Tornado : ShotProjectile
    {
        bool startCircle = false;
        Vector3 circlePos;
        float timeAlive = 7;
        private void Awake()
        {
            damage = 3;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            //if bullet gets too far away, it disappears
            if (Vector3.Distance(player.transform.position, transform.position) > 40)
            {
                Destroy(gameObject);
            }

            /*if (Vector3.Distance(player.transform.position, transform.position) > 5 && !startCircle)
            {
                startCircle = true;
                circlePos = transform.position;
            }*/
            if (startCircle)
            {
                Circle();
            }
            timeAlive -= Time.deltaTime;
            if(timeAlive < 0f)
            {
                Destroy(gameObject);
            }
            transform.rotation = Quaternion.identity;
        }

        new void OnTriggerEnter2D(Collider2D col)
        {
            base.OnTriggerEnter2D(col);
            if (!startCircle)
            {
                //defines circling position as 2 units towards the player from the position of the tornado
                circlePos = player.transform.position - transform.position;
                circlePos.Normalize();
                circlePos = transform.position + new Vector3(circlePos.x * CircleRadius, circlePos.y * CircleRadius);
                angle = Mathf.Atan2(transform.position.y - circlePos.y, transform.position.x - circlePos.x);// * Mathf.Rad2Deg;
                
            }
            startCircle = true;
        }


        float CircleRadius = 1;

        private Vector3 positionOffset;
        private float angle;

        private void Circle()
        {
            positionOffset.Set(Mathf.Cos(angle) * CircleRadius, Mathf.Sin(angle) * CircleRadius,1 );
            transform.position = circlePos + positionOffset;
            angle += Time.deltaTime * movespeed*.8f;
        }
    }
}