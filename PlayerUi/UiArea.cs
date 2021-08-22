using System;
using UnityEngine;

namespace Master.QSpaceCode.PlayerUi
{
    public abstract class UiArea : MonoBehaviour
    {
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }
        
        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnEnable()
        {
            
        }

        protected virtual void OnDisable()
        {
            
        }
    }
}