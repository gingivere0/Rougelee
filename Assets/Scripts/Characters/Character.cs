using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Character : MonoBehaviour
    {

        public GameObject[] projectileArray;
        public GameObject[] upgradeArray;
        public bool facingLeft = false;
        public bool startRunning = false;
        protected bool isRunning = false;
        protected bool isAttacking = false;
        public Gun[] weapons = new Gun[4];
        protected SpriteRenderer sp;
        protected Animator myAnim;
        GameObject playerObject;
        Player player;

        bool setup = false;

        protected GameObject crosshair;

        private void Awake()
        {
            foreach(GameObject proj in projectileArray) {
                proj.GetComponent<ShotProjectile>().Reset();
            }
            weapons[0] = new Gun(projectileArray[0]);
            //weapons[1] = new Gun(projectileArray[1]);
            sp = GetComponent<SpriteRenderer>();
            myAnim = GetComponent<Animator>();
            crosshair = transform.parent.GetChild(3).gameObject;
            playerObject = transform.parent.gameObject;
            player = playerObject.GetComponent<Player>();

        }

        public void Setup()
        {
            for (int i = 0; i < player.XPBarUIs.Length; i++)
            {
                if (weapons[i] != null)
                {
                    player.XPBarUIs[i].SetActive(true);

                    
                    
                    if (weapons[i].projectile.GetComponent<ShotProjectile>().GetType().Name == "Sword")
                    {
                        UpgradeTree.swordXPBarIndex = i;
                        player.weaponXPBars[i].SetXP(UpgradeTree.swordXP, UpgradeTree.swordNextLevelXP);

                        player.weaponXPBars[i].transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("" + UpgradeTree.swordLevel);

                    }
                    else if (weapons[i].projectile.GetComponent<ShotProjectile>().GetType().Name == "Fireball")
                    {
                        UpgradeTree.fireballXPBarIndex = i;
                        player.weaponXPBars[i].SetXP(UpgradeTree.fireballXP, UpgradeTree.fireballNextLevelXP);
                        player.weaponXPBars[i].transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("" + UpgradeTree.fireballLevel);
                    }
                    else if (weapons[i].projectile.GetComponent<ShotProjectile>().GetType().Name == "Tornado")
                    {
                        UpgradeTree.tornadoXPBarIndex = i;
                        player.weaponXPBars[i].SetXP(UpgradeTree.tornadoXP, UpgradeTree.tornadoNextLevelXP);
                        player.weaponXPBars[i].transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("" + UpgradeTree.tornadoLevel);
                    }
                    else if (weapons[i].projectile.GetComponent<ShotProjectile>().GetType().Name == "Spike")
                    {
                        UpgradeTree.spikeXPBarIndex = i;
                        player.weaponXPBars[i].SetXP(UpgradeTree.spikeXP, UpgradeTree.spikeNextLevelXP);
                        player.weaponXPBars[i].transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("" + UpgradeTree.spikeLevel);
                    }
                    else if (weapons[i].projectile.GetComponent<ShotProjectile>().GetType().Name == "Lightning")
                    {
                        UpgradeTree.lightningXPBarIndex = i;
                        player.weaponXPBars[i].SetXP(UpgradeTree.lightningXP, UpgradeTree.lightningNextLevelXP);
                        player.weaponXPBars[i].transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("" + UpgradeTree.lightningLevel);
                    }
                    else if (weapons[i].projectile.GetComponent<ShotProjectile>().GetType().Name == "Ice")
                    {
                        UpgradeTree.iceXPBarIndex = i;
                        player.weaponXPBars[i].SetXP(UpgradeTree.iceXP, UpgradeTree.iceNextLevelXP);
                        player.weaponXPBars[i].transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("" + UpgradeTree.iceLevel);
                    }
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (!setup)
            {
                Setup();
                setup = true;
            }
        }

        private void FixedUpdate()
        {
            
        }

 

        public virtual bool UseWeapon(int weapon)
        {
            bool shot = false;
            if (weapons[weapon] != null)
            {
                shot = weapons[weapon].Shoot();
            }
            return shot;
        }

        public void IsAttacking(int isAttacking)
        {
            this.isAttacking = isAttacking==1;
        }

    }
}