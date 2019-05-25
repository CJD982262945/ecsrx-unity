using System;
using UniRx;
using FairyGUI;


namespace FGUI.Bindings
{
    [FGUIAttributes.UIBindTypeInfo(typeof(GGraph))]
    public struct GGraphExtension : IUnirxBind
    {
        GGraph _obj;
        UIBase _ui;

        public GGraphExtension(UIBase ui, GObject obj)
        {
            _ui = ui;
            _obj = obj.asGraph;
        }
        
        public UIBase GetUiBase()
        {
            return _ui;
        }

        public GObject GetGObject()
        {
            return _obj;
        }
        
        public void OnClick(ReactiveCommand cmd)
        {
            _obj.displayObject.onClick.Add(() => {cmd.Execute(); });
        }
        
        public void OnClick(Action cmd)
        {
            if (_obj.displayObject != null)
            {
                _obj.displayObject.onClick.Add(() => {
                    cmd();
                    
                });
                
            }
            else
            {
                _obj.onClick.Add(() => {
                    cmd();
                    
                });
            }
        }
        
        public void OnClick(Action<EventContext> cmd)
        {
            if (_obj.displayObject != null)
            {
                _obj.displayObject.onClick.Add(c => cmd(c));
                
            }
            else
            {
                _obj.onClick.Add(c => cmd(c));
            }
        }
        
        public void Visible(IObservable<bool> o)
        {
            var g = _obj;
            var sub = o.Subscribe((b) =>
            {
                g.visible = b;
            });
            _ui.AddDisposable(sub);
        }
        
        public void Touchable(IObservable<bool> o)
        {
            var g = _obj;
            var sub = o.Subscribe((b) =>
            {
                g.touchable = b;
            });
            _ui.AddDisposable(sub);
        }
        
    }
}