using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Soap.Utils
{
    public class FPS : MonoBehaviour
    {
        [SerializeField] private float showRate = 1f;
        private int frameCount = 0;
        private float deltaTime = 0;
        private float fps;
        
        private void OnGUI()
        {
            GUI.Label(new Rect(Screen.width / 2, 0, 100, 30), fps.ToString("F1"),new GUIStyle
            {
                fontSize = 50,
                normal = new GUIStyleState
                {
                    textColor = Color.white
                }
            });
        }

        private void Update()
        {
            frameCount++;

            deltaTime += Time.deltaTime;

            if (deltaTime >= showRate)
            { 
                fps = frameCount / deltaTime;
                deltaTime = 0;
                frameCount = 0;
            }
        }
    }
}
