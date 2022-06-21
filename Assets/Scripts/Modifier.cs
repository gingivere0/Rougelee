using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier 
{

    public float damageMod;
    public float sizeMod;
    public int bulletMultiplier;



    // Start is called before the first frame update
    public Modifier()
    {
        damageMod = 1;
        sizeMod = 1;
        bulletMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
