using System;
using UniRx;
using FairyGUI;
using UnityEngine;

    
namespace FGUI.Bindings
{
    [FGUIAttributes.UIBindTypeInfo(typeof(GComponent))]
    public struct GComponentExtension : IUnirxBind
    {
        GComponent _obj;
        UIBase _ui;

        public GComponentExtension(UIBase ui, GObject obj)
        {
            _ui = ui;
            _obj = obj.asCom;
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
            _obj.onClick.Add(() => {cmd.Execute(); });
        }
        
        public void OnClick(Action act)
        {
            _obj.onClick.Add(()=>act());
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
        
//        public void ObserverListAll<T, T1>(IReactiveCollection<T1> list, bool autoWidth = false) where T : UIBase where T1 : IViewModel
//        {
//            System.Type type = typeof(T);
//            bool isImpleIViewModelCtrl = typeof(T1).IsSubclassOf(typeof(IViewModelCtrl));
//            var g = gObject;
//            var u = uiBase;
//            var observeAdd = list.ObserveAdd();
//            var subAdd = observeAdd.Subscribe((o) =>
//            {
//                if (isImpleIViewModelCtrl)
//                {
//                    type = (o.Value as IViewModelCtrl).CtrlType;
//                }
//                var ctrl = System.Activator.CreateInstance(type, g, o.Value) as T;
//                ctrl.SetMainViewModel(o.Value as IViewModel);
//                ctrl.CreateUIInstance(false);
//                //g.AddSelection(g.numItems, true);
//                u.TempKv[o.Value] = ctrl;
//                if (autoWidth)
//                {
//                    adjustWidth(ctrl, g);
//                }
//                ctrl.AddToPanel(u);
//            });
//            var it = list.GetEnumerator();
//            while (it.MoveNext())
//            {
//                if (isImpleIViewModelCtrl)
//                {
//                    type = (it.Current as IViewModelCtrl).CtrlType;
//                }
//
//                var ctrl = System.Activator.CreateInstance(type, g, it.Current) as T;
//                ctrl.SetMainViewModel(it.Current as IViewModel);
//                ctrl.CreateUIInstance(false);
//                //g.AddSelection(g.numItems, true);
//                u.TempKv[it.Current] = ctrl;
//                if (autoWidth)
//                {
//                    adjustWidth(ctrl, g);
//                }
//                ctrl.AddToPanel(uiBase);
//            }
//
//            _ui.AddDisposable(subAdd);
//
//
//            var subRemove = list.ObserveRemove().Subscribe((o) =>
//            {
//                var item = g.GetChildAt(o.Index);
//                g.RemoveChildAt(o.Index, true);
//                //item.RemoveFromParent();
//                //item.Dispose();
//                if (u.TempKv.ContainsKey(o.Value))
//                {
//                    var uiBase1 = u.TempKv[o.Value] as UIBase;
//                    uiBase1.Close();
//                    u.TempKv.Remove(o.Value);
//                    if (autoWidth)
//                    {
//                        adjustWidth(uiBase1, g);
//                    }
//                }
//            });
//            _ui.AddDisposable(subRemove);
//
//            //TODO Maybe bug  try:g.container.GetChildAt
//            var subMove = list.ObserveMove().Subscribe((o) =>
//            {
//                var child = g.GetChildAt(o.OldIndex);
//                g.SetChildIndex(child, o.NewIndex);
//                //child.RemoveFromParent();
//                //if (o.NewIndex <= o.OldIndex)
//                //{
//                //    g.SetChildIndex(child, o.NewIndex);
//                //}
//                //else
//                //{
//                //    g.SetChildIndex(child, o.NewIndex + 1);
//                //}
//            });
//            _ui.AddDisposable(subMove);
//        }
          
        private static void adjustWidth(UIBase ctrl, GComponent g)
        {
            try
            {
                var width = ctrl.gObject.width;
                var containerWidth = g.viewWidth;
                var itemWidth = containerWidth / g.numChildren;
                for (int i = 0; i < g.numChildren; i++)
                {
                    g.GetChildAt(i).width = itemWidth;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("GComponent error:" + e.ToString());
            }
        }
        
    }
}