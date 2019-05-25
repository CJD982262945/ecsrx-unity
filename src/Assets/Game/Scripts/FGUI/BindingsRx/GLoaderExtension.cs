using System;
using UniRx;
using FairyGUI;
using UnityEngine;


    
namespace FGUI.Bindings
{
    [FGUIAttributes.UIBindTypeInfo(typeof(GLoader))]
    public struct GLoaderExtension : IUnirxBind
    {
        GLoader _obj;
        UIBase _ui;

        public GLoaderExtension(UIBase ui, GObject obj)
        {
            _ui = ui;
            _obj = obj.asLoader;
        }
        
        public UIBase GetUiBase()
        {
            return _ui;
        }

        public GObject GetGObject()
        {
            return _obj;
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
        
        public void Visible(IObservable<bool> o, bool publish = false, bool initValue = false)
        {
            var g = _obj;
            var sub = o.Subscribe((str) =>
            {
                g.visible = str;
            });
            if (publish)
            {
                g.visible = initValue;
                //o.Publish(initValue);
            }
            _ui.AddDisposable(sub);
        }
        
        public void ImageFromRes(IObservable<string> imageSrc, IObservable<string> imageAlphaSrc = null)
        {
            var g = _obj;
            if (imageAlphaSrc == null)
            {
                var sub = imageSrc.Subscribe((str) =>
                {
#if _FGUI_LOAD_FROM_AB_

#else
                    var img = Resources.Load<Texture2D>(str);
                    if (img != null)
                    {
                        g.texture = new NTexture(img);
                    }
#endif
                });
                GetUiBase().AddDisposable(sub);
            }
            else
            {
                var subAlpha = imageSrc.CombineLatest(imageAlphaSrc,(a,b)=> new Tuple<string,string>(a,b)).Subscribe((str) =>
                {
#if _FGUI_LOAD_FROM_AB_

#else
                    var img = Resources.Load<Texture2D>(str.Item1);
                    UnityEngine.Assertions.Assert.IsNotNull(img, "## img at " + str.Item1 + " not found...");
                    var imgAlpha = Resources.Load<Texture2D>(str.Item2);
                    UnityEngine.Assertions.Assert.IsNotNull(img, "## imgAlpha at " + str.Item2 + " not found...");
#endif
                    NTexture texture = null;
                    if (img != null && imgAlpha == null)
                    {
                        texture = new NTexture(img);
                    }
                    else
                    {
                        texture = new NTexture(img,imgAlpha,1,1);
                    }
                    g.texture = texture;
                });
            }
        }
        
        public void URL(IObservable<string> url)
        {
            var g = _obj;
            var sub = url.Subscribe((str) =>
            {
                g.url = str;
            });
            _ui.AddDisposable(sub);
        }
        
        public void OnClick(ReactiveCommand cmd)
        {
            _obj.onClick.Add(() => {cmd.Execute(); });
        }

    }
}