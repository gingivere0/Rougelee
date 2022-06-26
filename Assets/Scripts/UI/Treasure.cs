using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rougelee
{
    public class Treasure : MonoBehaviour
    {

        public GameObject treasureObject;

        UnityEngine.UI.Button buttonOne;
        UnityEngine.UI.Button buttonTwo;

        ChestDrop cd;

        PlayerControls controls;

        void Awake()
        {
            //controls = new PlayerControls();
            treasureObject.SetActive(false);
            
        }

        void Update()
        {
        }

        public void Activate(ChestDrop cd)
        {
            treasureObject.SetActive(true);
            buttonOne = (UnityEngine.UI.Button)GameObject.Find("firstOption").GetComponent(typeof(UnityEngine.UI.Button));
            buttonOne.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = cd.GetText(0);
            buttonOne.Select();

            buttonTwo = (UnityEngine.UI.Button)treasureObject.transform.GetChild(1).GetComponent<UnityEngine.UI.Button>();
            buttonTwo.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = cd.GetText(1);

            this.cd = cd;
            Time.timeScale = 0;


        }

        public void firstOption()
        {
            //Player player = (Player)GameObject.Find("player").GetComponent(typeof(Player));

            cd.ReceiveTreasure(0);
            
            //player.mods.bulletMultiplier += 2;
            Time.timeScale = 1;
            treasureObject.SetActive(false);
        }

        public void secondOption()
        {
            cd.ReceiveTreasure(1);
            Time.timeScale = 1;
            treasureObject.SetActive(false);
        }
    }
}