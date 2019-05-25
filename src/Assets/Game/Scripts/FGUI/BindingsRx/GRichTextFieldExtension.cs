
using System;
using UniRx;
using FairyGUI;

    
namespace FGUI.Bindings
{
    [FGUIAttributes.UIBindTypeInfo(typeof(GRichTextField))]
    public struct GRichTextFieldExtension : IUnirxBind
    {
        GRichTextField _obj;
        UIBase _ui;

        public GRichTextFieldExtension(UIBase ui, GObject obj)
        {
            _ui = ui;
            _obj = obj.asRichTextField;
        }
        
        public UIBase GetUiBase()
        {
            return _ui;
        }

        public GObject GetGObject()
        {
            return _obj;
        }
        
        //注解生成
        [FGUIAttributes.UIBindPropertyInfoAttribute(typeof(string), "text")]
        public void Text(IObservable<string> text, bool BindToInputTextField = false, bool BindToRichTextField = false)
        {
            var g = _obj;
            var sub = text.Subscribe((str) =>
            {
                str = string.IsNullOrEmpty(str) ? string.Empty : str;
                if (BindToInputTextField)
                {
                    g.text = str;
                }
                else if (BindToRichTextField)
                {
                    g.asRichTextField.text = str;
                }
                else
                {
                    g.text = str;
                }
            });
            _ui.AddDisposable(sub);
        }
        
        public void FetchText(ReactiveProperty<string> text)
        {
            var g = _obj;
            var textInput = g.asTextInput;
            if(textInput != null)
            {
                textInput.onChanged.Add((evt) =>
                {
                    var sender = evt.sender as GTextInput;
                    sender.editable = false;
                    text.Value = sender.text;
                    sender.editable = true;
                });
            }
        }
        
        public void Editable(IObservable<bool> editable)
        {
            var g = _obj;
            var sub = editable.Subscribe((b) =>
            {
                g.richTextField.touchable = true;
            });
            _ui.AddDisposable(sub);
        }
        
        public void Text(string text)
        {
            _obj.text = text;
        }

        public void OnClick(ReactiveCommand cmd)
        {
            _obj.onClick.Add(() => {cmd.Execute(); });
        }
        
    }
}