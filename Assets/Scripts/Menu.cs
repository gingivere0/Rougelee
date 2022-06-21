using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Menu : MonoBehaviour
    {
        public GameObject StartMenu;

        PlayerControls controls;
        bool startPressed;

        void Awake()
        {
            controls = new PlayerControls();

            //pause the game with the start button
            controls.Gameplay.Start.performed += ctx => startPressed = !startPressed;
        }

        void Update()
        {
            StartCheck();
        }

        void StartCheck()
        {
            if (startPressed)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }

        public void Pause()
        {
            Time.timeScale = 0;
            StartMenu.SetActive(true);
        }


        //startPressed needs to be reset because the menu can call this method directly
        public void Unpause()
        {
            Time.timeScale = 1;
            startPressed = false;
            StartMenu.SetActive(false);
        }

        void OnEnable()
        {
            controls.Gameplay.Enable();
        }

        void OnDisable()
        {
            controls.Gameplay.Disable();
        }

        public void Exit()
        {
            Debug.Log("Quitting");
            Application.Quit();
        }
    }
}
