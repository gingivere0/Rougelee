using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Upgrade
    {
        string text;
        Action upgrade;

        public Upgrade(string text, Action upgrade)
        {
            this.text = text;
            this.upgrade = upgrade;
        }

        public string GetText()
        {
            return text;
        }
        public void SetText(string text)
        {
            this.text = text;
        }

        public void PerformUpgrade()
        {
            upgrade();
        }

        public void SetUpgrade(Action performUpgrade)
        {
            this.upgrade = performUpgrade;
        }


    }
}