using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Menu : MonoBehaviour
    {
        public GameObject StartMenu;

        PlayerControls controls;
        bool startPressed = false, startPressedAgain = false;

        UnityEngine.UI.Button button;

        void Awake()
        {
            controls = new PlayerControls();

            //pause the game with the start button
            //controls.Gameplay.Start.performed += ctx => startPressed = !startPressed;
            controls.Gameplay.Start.performed += ctx => StartPressed();
            
        }

        //checks if start was pressed
        void StartCheck()
        {
            if (startPressed && !startPressedAgain)
            {
                Pause();
            }
            if(startPressed && startPressedAgain)
            {
                Unpause();
            }
        }

        //pauses the game and opens the start menu
        public void Pause()
        {
            Time.timeScale = 0;
            StartMenu.SetActive(true);

            button = (UnityEngine.UI.Button)GameObject.Find("ResumeButton").GetComponent(typeof(UnityEngine.UI.Button));
            button.Select();

        }


        //startPressed needs to be reset because the menu can call this method directly
        public void Unpause()
        {
            startPressed = false;
            startPressedAgain = false;
            Time.timeScale = 1;
            StartMenu.SetActive(false);
        }

        public void StartPressed()
        {
            if (startPressed)
            {
                startPressedAgain = true;
            }
            startPressed = true;
            StartCheck();
        }

        public void Finish()
        {
            GameObject levelUpScreen = GameObject.Find("LevelUp");
            Time.timeScale = 1;
            levelUpScreen.SetActive(false);
        }

        //required to activate the controls
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
