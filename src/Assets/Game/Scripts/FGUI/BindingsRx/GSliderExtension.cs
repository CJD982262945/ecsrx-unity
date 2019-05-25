using System;
using UniRx;
using FairyGUI;

    
    
namespace FGUI.Bindings
{
    [FGUIAttributes.UIBindTypeInfo(typeof(GSlider))]
    public struct GSliderExtension : IUnirxBind
    {
        GSlider _obj;
        UIBase _ui;

        public GSliderExtension(UIBase ui, GObject obj)
        {
            _ui = ui;
            _obj = obj.asSlider;
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
        
        public void OnClick(ReactiveCommand cmd)
        {
            var g = _obj;
            var sub = cmd.Subscribe((u) =>
            {
                g.onClick.Add(() => cmd.Execute());
            });
            _ui.AddDisposable(sub);
        }
        
        public void OnChanged(Action<double> changed)
        {
            var g = _obj;
            _obj.onChanged.Add(()=>
            {
                changed(g.value);
            });
        }
        
        public void OnDragStart(Action<double> changed)
        {
            var g = _obj;
            _obj.onDragStart.Add(() =>
            {
                changed(g.value);
            });
        }
        
        public void OnDragEnd(Action<double> changed)
        {
            var g = _obj;
            _obj.onDragEnd.Add(() =>
            {
                changed(g.value);
            });
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

        public void Value(IObservable<float> value, bool publish = false)
        {
            var g = _obj;
            var sub = value.Subscribe((v) =>
            {
                g.value = v;
            });
            if (publish)
            {
                value.Publish();
            }
            _ui.AddDisposable(sub);
        }
        
        //TODO view属性变化 让viewmodel获取 双向绑定时使用要注意防止死循环 需要处理一下
        public void OnFetchValue(FloatReactiveProperty value)
        {
            var g = _obj;
            g.onChanged.Add((ctx) =>
            {
                var sl = ctx.sender as FairyGUI.GSlider;
                value.Value = (float)sl.value;
            });
        }

        public void Max(IObservable<float> max)
        {
            var g = _obj;
            var sub = max.Subscribe((m) =>
            {
                g.max = m;
            });
            _ui.AddDisposable(sub);
        }
        
    }
}