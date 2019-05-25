using System;
using UniRx;
using FairyGUI;

namespace FGUI.Bindings
{
    public interface IUnirxBind {

        //void SubScribe(UniRx.ReactiveCommand cmd, System.Action<UniRx.Unit> onCmd);
        UIBase GetUiBase();
        GObject GetGObject();
    }

    public static class IUnirxBindExtension
    {
        public static void SubScribe(this IUnirxBind gobjectSub, ReactiveCommand cmd, System.Action<Unit> onCmd)
        {
            var sub = cmd.Subscribe(onCmd);
            gobjectSub.GetUiBase().AddDisposable(sub);
        }
        
        public static void SubScribe(this IUnirxBind gobjectSub, IReactiveCommand<Unit> cmd, System.Action<IUnirxBind> onCmd)
        {
            var sub = cmd.Subscribe((u)=>
            {
                onCmd(gobjectSub);
            });
            gobjectSub.GetUiBase().AddDisposable(sub);
        }
        
        public static void OnClose(this IUnirxBind gobjectSub)
        {
//            IViewStates viewStates = viewModel as IViewStates;
//            if (viewStates != null)
//            {
//                gobjectSub.OnClick(viewStates.WillClose);
//            }
        }
        
        public static void OnClick(this IUnirxBind gobjectSub, FairyGUI.EventCallback0 onClick)
        {
            gobjectSub.GetGObject().onClick.Add(() =>
            {
                onClick();
                
            });
        }
        
        public static void OnClick(this IUnirxBind gobjectSub, BoolReactiveProperty isClicked)
        {
            gobjectSub.GetGObject().onClick.Add(() => {
                isClicked.Value = true;
            });
        }
        
        public static void OnClick(this IUnirxBind gobjectSub, IReactiveProperty<bool> isClicked)
        {
            gobjectSub.GetGObject().onClick.Add(
                () => {
                    isClicked.Value = true;
                    
                }
            );
        }
        
        public static void Name(this IUnirxBind s, IObservable<string> o)
        {
            var sub = o.Subscribe((str) =>
            {
                s.GetGObject().name = str;
            });
            s.GetUiBase().AddDisposable(sub);
        }
        
        public static void Visible(this IUnirxBind s, IObservable<bool> o, bool publish = false, bool initValue = false)
        {
            var sub = o.Subscribe((str) =>
            {
                s.GetGObject().visible = str;
            });
            if (publish)
            {
                s.GetGObject().visible = initValue;
                //o.Publish(initValue);
            }
            s.GetUiBase().AddDisposable(sub);
        }
        
        
        public static void Alpha(this IUnirxBind s, IObservable<float> alpha)
        {
            var g = s.GetGObject();
            var sub = alpha.Subscribe((a) =>
            {
                g.alpha = a;
            });
            s.GetUiBase().AddDisposable(sub);
        }
        
        public static void Interactive(this IUnirxBind s, IObservable<bool> interactive)
        {
            var g = s.GetGObject();
            var sub = interactive.Subscribe((ia) =>
            {
                g.touchable = ia;
            });
            s.GetUiBase().AddDisposable(sub);
        }
        
        public static void Interactive(this IUnirxBind s, IObservable<bool> interactive, bool hasDefaultValue = false, bool defaultValue = false)
        {
            var g = s.GetGObject();
            var sub = interactive.Subscribe((b) =>
            {
                g.touchable = b;
            });
            if (hasDefaultValue)
            {
                g.touchable = defaultValue;
            }
            s.GetUiBase().AddDisposable(sub);
        }
        
        
        public static void State(this IUnirxBind s, string controllerName, IObservable<int> state, bool hasDefault = false, int defaultState = 0)
        {
            var g = s.GetGObject();
            var sub = state.Subscribe((st) =>
            {
                var ctrl = g.asCom.GetController(controllerName);
                UnityEngine.Assertions.Assert.IsNotNull(ctrl);
                ctrl.SetSelectedIndex(st);
            });
            if (hasDefault)
            {
                var ctrl = g.asCom.GetController(controllerName);
                UnityEngine.Assertions.Assert.IsNotNull(ctrl);
                ctrl.SetSelectedIndex(defaultState);
            }
            s.GetUiBase().AddDisposable(sub);
        }
        
        public static void State(this IUnirxBind s, string controllerName, IObservable<string> state, bool hasDefault = false, string defaultState = "")
        {
            var g = s.GetGObject();
            var sub = state.Subscribe((st) =>
            {
                var ctrl = g.asCom.GetController(controllerName);
                UnityEngine.Assertions.Assert.IsNotNull(ctrl);
                ctrl.SetSelectedPage(st);
            });
            if (hasDefault)
            {
                var ctrl = g.asCom.GetController(controllerName);
                UnityEngine.Assertions.Assert.IsNotNull(ctrl);
                ctrl.SetSelectedPage(defaultState);
            }
            s.GetUiBase().AddDisposable(sub);
        }
        
    }
}