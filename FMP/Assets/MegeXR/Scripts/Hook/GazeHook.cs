
/********************************************************************
     Copyright (c) XTech Cloud
     All rights reserved.
*********************************************************************/

using UnityEngine;

namespace XTC.FMP.APP.XR
{

    public class GazeHook : MonoBehaviour
    {
        public Transform eventSystem;
        public Transform canvas;
        public Transform reticlePointer;
        public Transform reticleTimer;

        private XBasePointer pointer { get; set; }
        private XReticleTimer timer_ = null;

        void Awake()
        {
            canvas.gameObject.AddComponent<XPointerGraphicRaycaster>();

            var pointerInput = eventSystem.gameObject.AddComponent<XPointerInputModule>();
            pointerInput.Setup();

            pointer = new XReticlePointer();
            (pointer as XReticlePointer).owner = reticlePointer;
            pointer.Setup();

            timer_ = new XReticleTimer();
            timer_.reticlePointer = pointer as XReticlePointer;
            timer_.owner = reticleTimer;
            timer_.Setup();
        }

        void OnEnable()
        {
            timer_.Enable();
        }

        void Update()
        {
            pointer.Update();
        }

        void OnDisable()
        {
            timer_.Disable();
        }

        void OnRenderObject()
        {
            timer_.RenderObject();
        }

        void OnApplicationPause(bool pause)
        {
            timer_.Pause(pause);
        }
    }
}//namespace
