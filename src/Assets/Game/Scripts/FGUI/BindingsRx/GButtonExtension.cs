using System;
using UniRx;
using FairyGUI;
using UnityEngine;

    
namespace FGUI.Bindings
{
    [FGUIAttributes.UIBindTypeInfo(typeof(GButton))]
    public struct GButtonExtension : IUnirxBind
    {
        GButton _obj;
        UIBase _ui;

        public GButtonExtension(UIBase ui, GObject obj)
        {
            _ui = ui;
            _obj = obj.asButton;
        }
        
        public UIBase GetUiBase()
        {
            return _ui;
        }

        public GObject GetGObject()
        {
            return _obj;
        }
        
        public void Title(IObservable<string> title){
            var o = _obj;
            var dis = title.Subscribe((str) =>
            {
                o.title = str;
            });
            _ui.AddDisposable(dis);
        }
        
        public void Color(IObservable<UnityEngine.Color> color)
        {
            var g = _obj;
            var sub = color.Subscribe((c) =>
            {
                g.titleColor = c;
            });
            _ui.AddDisposable(sub);
        }
        
        public void ColorImage(IObservable<Color> color)
        {
            var g = _obj;
            var sub = color.Subscribe((c) =>
            {
                //g.SetButtonColor(c);
                g.color = c;
            });
            _ui.AddDisposable(sub);
        }
        
        public void Text(IObservable<string> text,string childName)
        {
            var g = _obj;
            var sub = text.Subscribe((c) =>
            {
                var tf = g.GetChild(childName).asTextField;
                if (tf!=null)
                {
                    tf.text = c;
                }
            });
            _ui.AddDisposable(sub);
        }
        
        public void OnClick(ReactiveCommand cmd)
        {
            _obj.displayObject.onClick.Add(
                () => {
                    cmd.Execute();
                }
            );
        }
        
        public void OnClick(Action cmd)
        {
            _obj.displayObject.onClick.Add(
                () =>
                {
                    cmd.Invoke();
                });
        }
        
        public void OnClickBtn(Action cmd)
        {
            _obj.onClick.Clear();
            _obj.onClick.Add(
                () =>
                {
                    cmd.Invoke();
                });
        }
        
        public void OnTouchBegin(ReactiveCommand cmd)
        {
            _obj.displayObject.onTouchBegin.Add(() => {cmd.Execute();});
        }
        
        public void OnTouchEnd(ReactiveCommand cmd)
        {
            _obj.displayObject.onTouchEnd.Add(() => {cmd.Execute();});
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
        
        public void OnChanged(ReactiveCommand cmd)
        {
            _obj.onChanged.Add(() =>
            {
                cmd.Execute();
            });
        }
        
        public void Controller(string name, IObservable<int> selectedIndex)
        {
            var g = _obj;
            var sub = selectedIndex.Subscribe((index) =>
            {
                g.GetController(name).SetSelectedIndex(index);
            });
            _ui.AddDisposable(sub);
        }
        
        
        public void StateIsDisabled(IObservable<bool> isDisabled)
        {
            var g = _obj;
            var sub = isDisabled.Subscribe((b) =>
            {
                var btnCtrl = g.GetController("button");
                if(btnCtrl != null)
                {
                    btnCtrl.SetSelectedIndex(b?2:0);
                }
            });
            _ui.AddDisposable(sub);
        }
        
        public void StateIsSelectedDisabled(IObservable<bool> isDisabled)
        {
            var g = _obj;
            var sub = isDisabled.Subscribe((b) =>
            {
                if (b)
                {
                    g.relatedController.SetSelectedPage("selectedDisabled");
                }
            });
            _ui.AddDisposable(sub);
        }
        
        public void Interactive(IObservable<bool> interactive, bool hasDefaultValue = false, bool defaultValue = false)
        {
            var g = _obj;
            var sub = interactive.Subscribe((b) =>
            {
                g.touchable = b;
            });
            if (hasDefaultValue)
            {
                g.touchable = defaultValue;
            }
            _ui.AddDisposable(sub);
        }
        
        public void Gray(IObservable<bool> gray, bool hasDefaultValue = false, bool defaultValue = false, bool grayChildren = false)
        {
            var g = _obj;
            var sub = gray.Subscribe((b) =>
            {
                if (grayChildren)
                {
                    for (int i = 0, c = g.numChildren; i < c; i++)
                    {
                        var child = g.GetChildAt(i);
                        if (child != null)
                        {
                            child.grayed = b;
                        }
                    }
                }
                else
                {
                    g.grayed = b;
                }
            });
            if (hasDefaultValue)
            {
                if (grayChildren)
                {
                    for (int i = 0, c = g.numChildren; i < c; i++)
                    {
                        var child = g.GetChildAt(i);
                        if (child != null)
                        {
                            child.grayed = defaultValue;
                        }
                    }
                }
                else
                {
                    g.grayed = defaultValue;
                }
            }
            _ui.AddDisposable(sub);
        }
        
    }
}