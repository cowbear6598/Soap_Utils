using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Soap.Utils
{
    public class FPS : MonoBehaviour
    {
        [SerializeField] private float showRate = 0.2f;
        private float lastUpdateFrameTime = 0;
        private int frameCount = 0;
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

        private void Awake()
        {
            lastUpdateFrameTime = Time.realtimeSinceStartup;
        }

        private void Update()
        {
            frameCount++;

            if (Time.realtimeSinceStartup - lastUpdateFrameTime >= showRate)
            {
                fps = frameCount / (Time.realtimeSinceStartup - lastUpdateFrameTime);
                frameCount = 0;
                lastUpdateFrameTime = Time.realtimeSinceStartup;
            }
        }
    }
}
