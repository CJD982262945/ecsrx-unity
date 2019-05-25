using System;
using System.Collections.Generic;
using UniRx;
using FairyGUI;

    
    
namespace FGUI.Bindings
{
    [FGUIAttributes.UIBindTypeInfo(typeof(GList))]
    public struct GListExtension : IUnirxBind
    {
        GList _obj;
        UIBase _ui;

        public GListExtension(UIBase ui, GObject obj)
        {
            _ui = ui;
            _obj = obj.asList;
        }
        
        public UIBase GetUiBase()
        {
            return _ui;
        }

        public GObject GetGObject()
        {
            return _obj;
        }
        
        public void OnClickItem(Action<int> onClick)
        {
            var g = _obj;
            g.onClickItem.Add((item) =>
            {
                var childIdx = g.GetChildIndex(item.data as GObject);
                if (onClick != null)
                {
                    onClick(childIdx);
                }
            });
        }
        
        private static void DiffSyncList(IList<int> selections, IList<int> selectedItemsIdx)
        {
            //删除中不在被选中的选项
            for (int i = selectedItemsIdx.Count - 1; i >= 0; i--)
            {
                if (!selections.Contains(selectedItemsIdx[i]))
                {
                    selectedItemsIdx.Remove(selectedItemsIdx[i]);
                }
            }
            //添加新被选中的选项
            for (int i = 0, c = selections.Count; i < c; i++)
            {
                if (!selectedItemsIdx.Contains(selections[i]))
                {
                    selectedItemsIdx.Add(selections[i]);
                }
            }
        }
        
        
        //同步选项信息到viewmodel 多选  manualState手动设置按钮状态 单选或者复选按钮 点下去 再移开 状态还是选中状态 但是list无法获取
        //manual 需要添加selection 控制器
        public void SelectedItemsIdx(IReactiveCollection<int> selectedItemsIdx, bool manualState = true)
        {
            var g = _obj;
            g.onClickItem.Add((item) =>
            {
                var selections = g.GetSelection();
                DiffSyncList(selections, selectedItemsIdx);
                if (manualState)
                {
                    for(int i = 0; i < g.numItems; i++)
                    {
                        var com = g.GetChildAt(i).asCom;
                        if (com != null)
                        {
                            var ctrl = com.GetController("selection");
                            if(ctrl != null)
                            {
                                ctrl.SetSelectedPage("up");
                            }
                        }
                    }
                    foreach (var idx in selectedItemsIdx)
                    {
                        var child = g.GetChildAt(g.ItemIndexToChildIndex(idx));
                        var com = g.GetChildAt(idx).asCom;
                        if (com != null)
                        {
                            var ctrl = child.asCom.GetController("selection");
                            if (ctrl != null)
                            {
                                ctrl.SetSelectedPage("down");
                            }
                        }
                    }
                }
                
            });
            {
                var selections = g.GetSelection();
                DiffSyncList(selections, selectedItemsIdx);
            }
        }
        
        //同步选项信息到viewmodel 单选
        public void SelectedItemIdx(IReactiveProperty<int> selectedItemIdx)
        {
            var g = _obj;
            g.onClickItem.Add((item) =>
            {
                selectedItemIdx.Value = g.selectedIndex;
            });
        }
        
        public void OnClick(ReactiveCommand cmd)
        {
            _obj.onClick.Add(() => {
                cmd.Execute();
                
            });
        }
        
        public void OnSelect(ReactiveCommand<EventContext> cmd)
        {
            _obj.onClickItem.Add((ctx) => {
                cmd.Execute(ctx);
            });

        }
        
        public void OnSelectIndex(ReactiveCommand<int> cmd)
        {
            var g = _obj;
            _obj.onClickItem.Add((ctx) => {
                var index = g.GetChildIndex(g.touchItem);
                cmd.Execute(index);
            });
        }
        
        public void SetSelectIndex(IObservable<int> selectedIdx)
        {
            var g = _obj;
            var sub = selectedIdx.Subscribe((idx) =>
            {
                g.selectedIndex = idx;
            });
            _ui.AddDisposable(sub);
        }
        
//        public void ObserverListAll<T, T1>(IReactiveCollection<T1> list, bool autoWidth = false) where T : UIBase where T1 : IViewModel
//        {
//        }

//        public void ObservePage<T, T1>(IObservable<IReactiveCollection<T1>> reactCollection, bool isDeepReact = false,
//            bool autoWidth = false) where T : UIBase where T1 : IViewModel
//        {
//        }
        
    }
}