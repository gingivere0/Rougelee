using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenAttack : MonoBehaviour
{
    GameObject playerObject;
    Rigidbody2D rb;

    float movespeed = 7f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
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