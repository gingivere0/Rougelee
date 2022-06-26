using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rougelee
{
    public class Player : MonoBehaviour
    {

        public float movespeed;
        public GameObject[] projectileArray;
        //public GameObject lightning;
        public Animator myAnim;

        public int hp = 10;
        public int kills = 0;

        public float xp = 0;
        public int level = 1;
        float nextLevelXP = 100;
        float nextLevelMult = 1.25f;

        public XPBar xpbar;
        public TMPro.TextMeshProUGUI levelText;
        public GameObject startMenu;


        public Modifier mods;
        public GameObject levelup;

        

        Vector2 targetPos;
        Vector2 move;
        Vector2 aim;
        bool shootWeaponOne, shootWeaponTwo;

        Rigidbody2D rb;

        PlayerControls controls;

        GameObject crosshair;

        GameObject characterObject;
        public Character character;


        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    characterObject = transform.GetChild(i).gameObject;
                    myAnim = transform.GetChild(i).gameObject.GetComponent<Animator>();
                    break;
                }

            }
            character = characterObject.GetComponent<Character>();
            rb = GetComponent<Rigidbody2D>();
            controls = new PlayerControls();

            startMenu.SetActive(false);
            levelText.text = "Level " + level;
            levelup.SetActive(false);


            mods = new Modifier();

            crosshair = GameObject.FindGameObjectWithTag("Crosshair");
            crosshair.transform.localScale = crosshair.transform.localScale * .6f;

            //moves with left stick
            controls.Gameplay.MoveStick.performed += ctx => move = ctx.ReadValue<Vector2>();
            controls.Gameplay.MoveStick.canceled += ctx => move = Vector2.zero;

            //move/stopmoving up
            controls.Gameplay.MoveW.performed += ctx => move.y = 1;
            controls.Gameplay.MoveW.canceled += ctx => move.y = -1 * controls.Gameplay.MoveS.ReadValue<float>();

            //move/stopmoving down
            controls.Gameplay.MoveS.performed += ctx => move.y = -1;
            controls.Gameplay.MoveS.canceled += ctx => move.y = controls.Gameplay.MoveW.ReadValue<float>();

            //move/stopmoving left
            controls.Gameplay.MoveA.performed += ctx => move.x = -1;
            controls.Gameplay.MoveA.canceled += ctx => move.x = controls.Gameplay.MoveD.ReadValue<float>();

            //move/stopmoving right
            controls.Gameplay.MoveD.performed += ctx => move.x = 1;
            controls.Gameplay.MoveD.canceled += ctx => move.x = -1 * controls.Gameplay.MoveA.ReadValue<float>();

            //aims the crosshair with the right stick
            controls.Gameplay.AimStick.performed += ctx => aim = ctx.ReadValue<Vector2>();

            //shoots with the right trigger
            controls.Gameplay.ShootR.performed += ctx => shootWeaponOne = true;
            controls.Gameplay.ShootR.canceled += ctx => shootWeaponOne = false;

            //shoots with the left trigger
            controls.Gameplay.ShootL.performed += ctx => shootWeaponTwo = true;
            controls.Gameplay.ShootL.canceled += ctx => shootWeaponTwo = false;

        }


        void Shoot()
        {
            if (shootWeaponOne)
            {
                character.UseWeapon(0);
            }
            else if (shootWeaponTwo)
            {
                character.UseWeapon(1);
            }
        }

        public void ChangeSprite(string spriteName)
        {
            myAnim.Play(spriteName);
        }

        void OnEnable()
        {
            controls.Gameplay.Enable();
        }

        void OnDisable()
        {
            controls.Gameplay.Disable();
        }

        private void FixedUpdate()
        {
            Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime * movespeed;
            transform.Translate(m);

            character.facingLeft = move.x < 0;
            if(move.x != 0)
            {
                character.startRunning = true;
            }
            else
            {
                character.startRunning = false;
            }


            MoveCrosshair();
            //FaceCrosshair();
            
            Shoot();

            CheckLevel();
        }


        public void CheckLevel()
        {
            if (xp > nextLevelXP)
            {
                level++;
                mods.damageMod *= 1.10f;
                nextLevelXP *= nextLevelMult;
                xp = 0;
                Time.timeScale = 0;
                levelup.SetActive(true);
                levelup.GetComponent<UnityEngine.UI.Button>().Select();
                levelText.text = "Level "+level;

            }
            xpbar.SetXP(xp, nextLevelXP);

        }

/*
        public void FaceCrosshair()
        {
            targetPos = crosshair.transform.position;

            //gets the vector towards the target, figures out the angle, rotates towards that angle immediately;
            Vector3 vectorToTarget = targetPos - (Vector2)transform.position;
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            sp.transform.rotation = Quaternion.RotateTowards(sp.transform.rotation, q, 1000f);
        }*/

        public void MoveCrosshair()
        {
            aim.Normalize();
            aim.x *= 2f;
            aim.y *= 2f;
            crosshair.transform.localPosition = aim;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            hp -= 1;
            //sp.flipY = !sp.flipY;
            if (col != null && col.gameObject != null && col.gameObject.tag == "Enemy")
            {
                Destroy(col.gameObject, 0f);
            }
        }

        public void Dead()
        {
            Time.timeScale = 0;
        }

    }
}