using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Soap.Update
{
    public class UpdateManager : SingletonMonoBehaviour<UpdateManager>
    {
        protected override bool IsNeedDontDestoryOnLoad => true;

        public Action UpdateEvent;
        public Action FixedUpdateEvent;
        public Action LateUpdateEvent;

        private void Update()
        {
            UpdateEvent?.Invoke();
        }

        private void FixedUpdate()
        {
            FixedUpdateEvent?.Invoke();
        }

        private void LateUpdate()
        {
            LateUpdateEvent?.Invoke();
        }

        public void AddUpdate(Action _action)
        {
            UpdateEvent += _action;
        }

        public void RemoveUpdate(Action _action)
        {
            UpdateEvent -= _action;
        }

        public void AddFixedUpdate(Action _action)
        {
            FixedUpdateEvent += _action;
        }

        public void RemoveFixedUpdate(Action _action)
        {
            FixedUpdateEvent -= _action;
        }

        public void AddLateUpdate(Action _action)
        {
            LateUpdateEvent += _action;
        }

        public void RemoveLateUpdate(Action _action)
        {
            LateUpdateEvent -= _action;
        }
    }
}