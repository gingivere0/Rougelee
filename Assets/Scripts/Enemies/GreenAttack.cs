using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenAttack : MonoBehaviour
{
    GameObject playerObject;
    Rigidbody2D rb;

    float movespeed = 12f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (playerObject != null)
        {
            bool tooFar = Vector3.Distance(playerObject.transform.position, transform.position) > 32;
            if (tooFar)
            {
                Destroy(gameObject);
            }
        }
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public void Shoot(Vector2 targetDir)
    {
        float lookAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));
        transform.Rotate(0,0,180);

        gameObject.GetComponent<Rigidbody2D>().velocity = targetDir.normalized * new Vector3(movespeed,movespeed);
    }
}