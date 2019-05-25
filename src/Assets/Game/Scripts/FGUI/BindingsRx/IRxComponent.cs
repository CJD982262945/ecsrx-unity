using System;
using UniRx;
using FairyGUI;

namespace FGUI.Bindings
{
    public interface IRxComponent
    {
        UIBase UiBase { get;}
        GObject GObject { get;}
    }
    
    public static class IRxComponentExtension
    {
        //对按钮控制器进行状态绑定
        public static void StateBinding(this IRxComponent rxCom, IObservable<int> state)
        {
            StateBinding(rxCom, "button", state);
        }
        
        public static void StateBinding(this IRxComponent rxCom,string controllerName, IObservable<int> state)
        {
            var sub = state.Subscribe((s) =>
            {
                var controller = rxCom.GObject.asCom.GetController(controllerName);
                if (controller != null)
                {
                    controller.SetSelectedIndex(s);
                }
            });
            rxCom.UiBase.AddDisposable(sub);
        }
        
        public static void StateBinding(this IRxComponent s, string controllerName, IObservable<string> state, bool hasDefault = false, string defaultState = "")
        {
            var g = s.GObject;
            var sub = state.Subscribe((st) =>
            {
                g.asCom.GetController(controllerName).SetSelectedPage(st);
            });
            if (hasDefault)
            {
                g.asCom.GetController(controllerName).SetSelectedPage(defaultState);
            }
            s.UiBase.AddDisposable(sub);
        }
        
        public static void Interactive(this IRxComponent s, IObservable<bool> interactive, bool hasDefaultValue = false, bool defaultValue = false)
        {
            var g = s.GObject;
            var ui = s.UiBase;
            var objSub = new GObjectExtension(ui,g);
            objSub.Interactive(interactive, hasDefaultValue, defaultValue);
        }
        
    }
}