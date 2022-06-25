using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rougelee
{
    public class UIManager : MonoBehaviour
    {
        public float timeValue = 58;
        public TMPro.TextMeshProUGUI timeText;
        public TMPro.TextMeshProUGUI killsText;

        public static int minutes = 0;
        public static int seconds = 0;
        public static int kills = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            timeValue += Time.deltaTime;
            DisplayTime();
            DisplayKills();
        }

        void DisplayKills()
        {
            killsText.text = "Kills: " + kills;
        }

        void DisplayTime()
        {
            seconds = (int)timeValue % 60;
            minutes = (int)timeValue / 60;
            //timeText.text = minutes+":"+seconds;
            var timespan = TimeSpan.FromSeconds((int)timeValue);

            timeText.text = timespan.ToString(@"mm\:ss");
        }
    }
}