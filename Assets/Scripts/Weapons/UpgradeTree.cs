using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class UpgradeTree
    {

        public static float swordXP = 0;
        public static int swordLevel = 1;
        public static float swordNextLevelXP = 100;
        public static int swordXPBarIndex;

        public static bool persistentSword = false;
        public static bool movingSword = false;
        public static bool spinSword = false;
        public static bool iceSword = false;

        public static int swordProj = 36;



        public static float fireballXP = 0;
        public static int fireballLevel = 1;
        public static float fireballNextLevelXP = 100;
        public static int fireballXPBarIndex;

        public static bool aoeFire = false;



        public static float tornadoXP = 0;
        public static int tornadoLevel = 1;
        public static float tornadoNextLevelXP = 100;
        public static int tornadoXPBarIndex;

        public static bool bigTornado = false;
        public static bool multiTornado = false;



        public static float lightningXP = 0;
        public static int lightningLevel = 1;
        public static float lightningNextLevelXP = 100;
        public static int lightningXPBarIndex;



        public static float spikeXP = 0;
        public static int spikeLevel = 1;
        public static float spikeNextLevelXP = 100;
        public static int spikeXPBarIndex;



        public static float iceXP = 0;
        public static int iceLevel = 1;
        public static float iceNextLevelXP = 100;
        public static int iceXPBarIndex;

        public static bool manyIce = false;


        public static float nextLevelMult = 1.25f;

        public static int bossesKilled = 0;

        public static bool autoShoot = false;


    }
}