using System;
using System.Collections.Generic;
using UniRx;

using UIHANDLE = FairyGUI.GObject;

namespace FGUI.Bindings
{
    public class UIBase : IDisposable
    {
        protected bool _disposed = false;
        protected bool _isGoingClose = false;

        protected UIHANDLE _gobject = null;
        
        
        
        public UIHANDLE gObject
        {
            get
            {
                return _gobject;
            }
        }
        
        
        public UIBase(UIHANDLE gObjectToAttach = null, bool isPanel = true)
        {
            this._isPanel = isPanel;
            this._gObjectToAttach = gObjectToAttach;
        }
        
        private UIHANDLE _gObjectToAttach = null;
        public UIHANDLE gObjectToAttach
        {
            get
            {
                return _gObjectToAttach;
            }
        }
        

        private bool _isPanel = false;
        protected bool IsPanel
        {
            get { return _isPanel; }
        }
        
        public void AddToPanel(UIBase parent)
        {
            _parent = parent;

            if (parent._children == null)
            {
                parent._children = new List<UIBase>();
            }
            parent._children.Add(this);
        }
        
        private List<UIBase> _children = null;
        public List<UIBase> Children
        {
            get { return _children; }
        }
        public List<UIBase> ChildrenCopy
        {
            get { return new List<UIBase>(_children); }
        }
        
        UIBase _parent = null;

        public void Close(bool destroy = true)
        {
            _isGoingClose = true;
            if (IsPanel)
            {
                //TODO: 删除pannel
//                UIManager.Instance.ClosePanel(this);
            }
            else
            {
                if (_parent != null && _parent._children != null)
                {
                    _parent._children.Remove(this);
                }
                Dispose();
            }
        }
        
        
        Dictionary<object, IDisposable> _dispoableMap = null;
        protected Dictionary<object, IDisposable> DispoableMap
        {
            get
            {
                _dispoableMap = _dispoableMap ?? new Dictionary<object, IDisposable>();
                return _dispoableMap;
            }
        }
        public void AddDisposable(object key, IDisposable dp)
        {
            DispoableMap[key] = dp;
        }
        
        
        List<IDisposable> _disposableList = null;
        protected List<IDisposable> Disposables
        {
            get
            {
                if (_disposableList == null)
                {
                    _gobject.onRemovedFromStage.Add(() =>
                    {
                        Dispose();
                    });
                }
                _disposableList = _disposableList ?? new List<IDisposable>();
                return _disposableList;
            }
        }

        public void AddDisposable(IDisposable dp)
        {
            Disposables.Add(dp);
        }
        
        public void Dispose()
        {
            if (!_disposed)
            {
                //TODO:
                _disposed = true;
            }            
        }
    }
}