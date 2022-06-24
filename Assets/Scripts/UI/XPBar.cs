using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBar : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SetXP(5f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetXP(float xp, float nextLvlXP)
    {
        gameObject.transform.localScale = new Vector2(xp / nextLvlXP, 1f);
    }
}
