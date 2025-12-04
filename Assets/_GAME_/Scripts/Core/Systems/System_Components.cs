using System;
using UnityEngine;
namespace Game.System.Components
{
	[DefaultExecutionOrder(-99)]
	public class Sys_Components : MonoBehaviour
	{
        #region Events
        public event Action OnWake, OnInit, OnTick;
        #endregion

        //Primary func of Unity
        #region Original
        private void Awake()
        {
            Wake();
            OnWake?.Invoke();
        }
        private void Start()
        {
            Init();
            OnInit?.Invoke();
        }
        private void Update()
        {
            Tick(Time.deltaTime); 
            RealTick(Time.unscaledDeltaTime);//for some efx like TimeStop
            OnTick?.Invoke();
        }
        private void FixedUpdate()
        {
            FixedTick(Time.fixedDeltaTime);
            RealFixedTick(Time.fixedUnscaledDeltaTime);//for some logic in TimeStop
            OnTick?.Invoke();
        }
        private void LateUpdate()
        {
            LateTick(Time.unscaledDeltaTime);
            OnTick?.Invoke();
        }
        private void OnEnable()
        {
            //no need
        }
        private void OnDisable()
        {
            
        }
        #endregion

        //more dynamic alternatives
        #region Alternative
        protected virtual void Wake() { }
        protected virtual void Init() { }
        protected virtual void Tick(float deltatime) { }
        protected virtual void RealTick(float rdeltatime) { }
        protected virtual void FixedTick(float fixedTime) { }
        protected virtual void RealFixedTick(float rfixedTime) { }
        protected virtual void LateTick(float deltatime) { }
        #endregion 
    }
}