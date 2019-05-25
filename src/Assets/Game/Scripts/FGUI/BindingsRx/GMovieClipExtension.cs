

using System;
using UniRx;
using FairyGUI;
using UnityEngine;

    
    
namespace FGUI.Bindings
{
    [FGUIAttributes.UIBindTypeInfo(typeof(GMovieClip))]
    public struct GMovieClipExtension : IUnirxBind
    {
        GMovieClip _obj;
        UIBase _ui;

        public GMovieClipExtension(UIBase ui, GObject obj)
        {
            _ui = ui;
            _obj = obj.asMovieClip;
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
        
        public void OnClick(System.Action cmd)
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
        
        public void PlayVisible(IObservable<bool> o)
        {
            var g = _obj;
            var sub = o.Subscribe((b) =>
            {
                g.visible = b;
                if (g.visible)
                {
                    g.playing = true;
                }
                else
                {
                    g.playing = false;
                    g.frame = -1;
                }
            });
            _ui.AddDisposable(sub);
        }
        
        public void PlayClip(IObservable<bool> o,float delay)
        {
            var g = _obj;
            g.SetPlaySettings(0, -1, 1, -1);
            g.playing = false;
            IDisposable subInner = null;
            var sub = o.Subscribe((b) =>
            {
                g.visible = b;
                if (g.visible)
                {
                    var d = Observable.Timer(TimeSpan.FromSeconds(delay));
                   
                    subInner = d.Subscribe(num =>
                    {
                        g.playing = true;
                        g.frame = 0;
                        Debug.Log("Play Clip");
                    });
                    
                }
                else
                {
                    g.playing = false;
                    g.frame = -1;
                }
            });
            _ui.AddDisposable(subInner);
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

        public void SpriteIdx(IObservable<int> index)
        {
            var g = _obj;
            var sub = index.Subscribe((idx) =>
            {
                g.frame = idx;
            });
            _ui.AddDisposable(sub);
        }
        
    }
}