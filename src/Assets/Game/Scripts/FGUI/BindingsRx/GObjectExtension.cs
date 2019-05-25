
using System;
using UniRx;
using FairyGUI;

     

    
namespace FGUI.Bindings
{
    [FGUIAttributes.UIBindTypeInfo(typeof(GObject))]
    public struct GObjectExtension : IUnirxBind
    {
        GObject _obj;
        UIBase _ui;

        public GObjectExtension(UIBase ui, GObject obj)
        {
            _ui = ui;
            _obj = obj;
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
        
        public void Text(IObservable<string> text)
        {
            var g = _obj;
            var sub = text.Subscribe((str) =>
            {
                g.text = str;
            });
            _ui.AddDisposable(sub);
        }
        
        public void OnClick(ReactiveCommand cmd)
        {
            var g = _obj;
            var sub = cmd.Subscribe((u) =>
            {
                g.onClick.Add(() => cmd.Execute());
                
            });
            _ui.AddDisposable(sub);
        }
        
        public void OnClick(System.Action cmd)
        { 
            _obj.onClick.Add(
                () => {
                    cmd();
                }
            );     
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

        public void Gray(IObservable<bool> gray)
        {
            var g = _obj;
            var sub = gray.Subscribe((v) =>
            {
                g.grayed = v;
            });
            _ui.AddDisposable(sub);
        }
        
    }
}