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
        public Gun[] weapons = new Gun[2];
        protected SpriteRenderer sp;
        protected Animator myAnim;
        GameObject playerObject;
        Player player;

        bool setup = false;

        protected GameObject crosshair;

        private void Awake()
        {
            projectileArray[0].GetComponent<ShotProjectile>().Reset();
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

                    Debug.Log(player.XPBarUIs[i].name + " : " + i + " : " + projectileArray[i].GetComponent<ShotProjectile>().GetType().Name);
                    if (projectileArray[i].GetComponent<ShotProjectile>().GetType().Name == "Sword")
                    {
                        UpgradeTree.swordXPBarIndex = i;
                        player.weaponXPBars[i].SetXP(UpgradeTree.swordXP, UpgradeTree.swordNextLevelXP);

                        player.weaponXPBars[i].transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("" + UpgradeTree.swordLevel);

                    }
                    else if (projectileArray[i].GetComponent<ShotProjectile>().GetType().Name == "Fireball")
                    {
                        UpgradeTree.fireballXPBarIndex = i;
                        player.weaponXPBars[i].SetXP(UpgradeTree.fireballXP, UpgradeTree.fireballNextLevelXP);
                        player.weaponXPBars[i].transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("" + UpgradeTree.fireballLevel);
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