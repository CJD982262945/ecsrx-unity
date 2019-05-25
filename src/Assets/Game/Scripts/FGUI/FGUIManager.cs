using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

using UIPackage = FairyGUI.UIPackage;
using GRoot = FairyGUI.GRoot;
using RelationType = FairyGUI.RelationType;
using ChildrenRenderOrder = FairyGUI.ChildrenRenderOrder;

using Unidux;
using FGUI.Bindings;

namespace FGUI
{
    public sealed class FGUIManager : SingletonMonoBehaviour<FGUIManager>
    {
        private Dictionary<Type, List<UIBase>> _uiMap = new Dictionary<Type, List<UIBase>>();
        private List<UIBase> _uiList = new List<UIBase>();
        private List<UIBase> _pendingUIList = new List<UIBase>();
        
    }
}