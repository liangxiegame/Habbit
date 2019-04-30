using System;
using System.Collections.Generic;
using EGO.Util;
using UnityEngine;

namespace EGO.Framework
{
    public abstract class View : IView
    {
        public class EventRecord
        {
            public int Key;

            public Action<object> OnEvent;
        }
        
        protected List<EventRecord> mEventRecords { get; } = new List<EventRecord>();

        protected void RegisterEvent<T>(T key, Action<object> onEvent) where T : IConvertible
        {
            EventDispatcher.Register(key, onEvent);

            mEventRecords.Add(new EventRecord
            {
                Key = key.ToInt32(null),
                OnEvent = onEvent
            });
        }

        protected void UnRegisterAll()
        {
            mEventRecords.ForEach(record => { EventDispatcher.UnRegister(record.Key, record.OnEvent); });

            mEventRecords.Clear();
        }

        protected void SendEvent<T>(T key, object arg) where T : IConvertible
        {
            EventDispatcher.Send(key, arg);
        }

        public bool Visible { get; set; } = true;
                
        private List<GUILayoutOption> mLayoutOptions { get; } = new List<GUILayoutOption>();
        
        protected GUILayoutOption[] LayoutStyles { get; private set; }
        
        public GUIStyle Style { get;protected set; } = new GUIStyle();

        public Color BackgroundColor { get; set; } = GUI.backgroundColor;

        public void RefreshNextFrame()
        {
            this.PushCommand(Refresh);
        }

        public void AddLayoutOption(GUILayoutOption option)
        {
            mLayoutOptions.Add(option);
        }
        
        public void Show()
        {
            Visible = true;
            OnShow();
        }
        
        protected virtual void OnShow(){}

        public void Hide()
        {
            Visible = false;
            OnHide();
        }

        protected virtual void OnHide(){}
        

        private Color mPreviousBackgroundColor;

        public void DrawGUI()
        {
            BeforeDraw();

            if (Visible)
            {
                mPreviousBackgroundColor = GUI.backgroundColor;
                GUI.backgroundColor = BackgroundColor;
                OnGUI();
                GUI.backgroundColor = mPreviousBackgroundColor;
            }
        }

        private bool mBeforeDrawCalled = false;
        void BeforeDraw()
        {
            if (!mBeforeDrawCalled)
            {
                OnBeforeDraw();
                
                LayoutStyles = mLayoutOptions.ToArray();
                
                mBeforeDrawCalled = true;
            }
        }

        protected virtual void OnBeforeDraw()
        {
            
        }
        
        public ILayout Parent { get; set; }

        public void RemoveFromParent()
        {
            Parent.RemoveChild(this);
        }

        public virtual void Refresh()
        {
            OnRefresh();
        }

        protected virtual void OnRefresh(){}

        protected abstract void OnGUI();

        public void Dispose()
        {
            UnRegisterAll();
            OnDisposed();
        }

        protected virtual void OnDisposed()
        {
            
        }
    }
}