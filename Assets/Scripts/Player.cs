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

        


        Gun[] guns = new Gun[2];

        Vector2 targetPos;
        Vector2 move;
        Vector2 aim;
        bool shootFire, shootLightning;

        Rigidbody2D rb;

        SpriteRenderer sp;

        PlayerControls controls;

        GameObject crosshair;



        enum Projectile
        {
            fireball,
            lightning,
            tornado
        }


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            sp = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            myAnim = transform.GetChild(0).gameObject.GetComponent<Animator>();
            controls = new PlayerControls();

            startMenu.SetActive(false);
            levelText.text = "Level " + level;
            levelup.SetActive(false);


            mods = new Modifier();

            guns[0] = new FireGun(projectileArray[0]);
            //guns[1] = new LightningGun(projectileArray[1]);
            guns[1] = new TornadoGun(projectileArray[1]);

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
            controls.Gameplay.ShootR.performed += ctx => shootFire = true;
            controls.Gameplay.ShootR.canceled += ctx => shootFire = false;

            //shoots with the left trigger
            controls.Gameplay.ShootL.performed += ctx => shootLightning = true;
            controls.Gameplay.ShootL.canceled += ctx => shootLightning = false;

        }


        void Shoot()
        {
            if (shootFire)
            {
                ChangeSprite("witchfire");
                guns[0].Shoot();
            }
            else if (shootLightning)
            {
                ChangeSprite("witchlightning");
                guns[1].Shoot();
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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime * movespeed;
            transform.Translate(m);


            MoveCrosshair();
            FaceCrosshair();
            
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


        public void FaceCrosshair()
        {
            targetPos = crosshair.transform.position;

            //gets the vector towards the target, figures out the angle, rotates towards that angle immediately;
            Vector3 vectorToTarget = targetPos - (Vector2)transform.position;
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            sp.transform.rotation = Quaternion.RotateTowards(sp.transform.rotation, q, 1000f);
        }

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