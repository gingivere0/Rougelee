using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class HealthUI : MonoBehaviour
    {
        public void SetHP(int hp)
        {
            for(int i = 5 ; i > 0; i--)
            {
                Debug.Log("i : " + i+" hp: "+hp);
                if (hp < i)
                {
                    Debug.Log(transform.GetChild(i-1).name);
                    transform.GetChild(i-1).GetChild(0).gameObject.SetActive(false);
                }
            }
            for(int i = 0 ; i < hp; i++)
            {
                    transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}