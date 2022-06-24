using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rougelee
{
    public class Treasure : MonoBehaviour
    {

        public GameObject treasureObject;

        UnityEngine.UI.Button button;

        PlayerControls controls;

        void Awake()
        {
            //controls = new PlayerControls();
            treasureObject.SetActive(false);
            
        }

        void Update()
        {
        }

        public void Activate()
        {
            treasureObject.SetActive(true);
            button = (UnityEngine.UI.Button)GameObject.Find("firstOption").GetComponent(typeof(UnityEngine.UI.Button));
            button.Select();

            Time.timeScale = 0;


        }

        public void firstOption()
        {
            Player player = (Player)GameObject.Find("witch_stand_0001").GetComponent(typeof(Player));
            player.mods.bulletMultiplier += 2;
            Time.timeScale = 1;
            treasureObject.SetActive(false);
        }

        public void secondOption()
        {
            Player player = (Player)GameObject.Find("witch_stand_0001").GetComponent(typeof(Player));
            player.mods.damageMod *= 1.5f;
            Time.timeScale = 1;
            treasureObject.SetActive(false);
        }
    }
}