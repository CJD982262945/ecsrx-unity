using System;
using UniRx;
using FairyGUI;


    
namespace FGUI.Bindings
{
    [FGUIAttributes.UIBindTypeInfo(typeof(GGroup))]
    public struct GGroupExtension : IUnirxBind
    {
        GGroup _obj;
        UIBase _ui;

        public GGroupExtension(UIBase ui, GObject obj)
        {
            _ui = ui;
            _obj = obj.asGroup;
        }
        
        public UIBase GetUiBase()
        {
            return _ui;
        }

        public GObject GetGObject()
        {
            return _obj;
        }
        
        public void Name(IObservable<string> o){
            var g = _obj;
            var sub = o.Subscribe((str) =>
            {
                g.name = str;
            });
            _ui.AddDisposable(sub);
        }
        
        public void OnClick(UniRx.ReactiveCommand cmd)
        {
            var g = _obj;
            var sub = cmd.Subscribe((u) =>
            {
                g.onClick.Add(() => cmd.Execute());
            });
            _ui.AddDisposable(sub);
        }
        
        public void Alpha(IObservable<float> alpha)
        {
            var g = _obj;
            var sub = alpha.Subscribe((a) =>
            {
                g.alpha = a;
            });
            _ui.AddDisposable(sub);
        }
        
        public void Visible(IObservable<bool> visible)
        {
            var g = _obj;
            var sub = visible.Subscribe((a) =>
            {
                g.visible = a;
            });
            _ui.AddDisposable(sub);
        }
        
        public void VisibleDelay(IObservable<float> delay)
        {
            var g = _obj;
            g.visible = false;
            IDisposable subInner = null;
            var sub = delay.Subscribe(x =>
            {
                var d = Observable.Timer(TimeSpan.FromSeconds(x));
                   
                subInner = d.Subscribe(num =>
                {
                    g.visible = true;
                    UnityEngine.Debug.Log("Make it visible at:"+x);
                });
            });
            _ui.AddDisposable(subInner);
            _ui.AddDisposable(sub);
        }

//        public void ObserverListAll<T, T1>(UniRx.IReactiveCollection<T1> list, bool autoWidth = false)
//            where T : UIBase where T1 : IViewModel
//        {
//        }
    }
}