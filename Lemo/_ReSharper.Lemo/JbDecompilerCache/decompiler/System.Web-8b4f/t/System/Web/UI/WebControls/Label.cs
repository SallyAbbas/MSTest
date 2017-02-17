// Type: System.Web.UI.WebControls.Label
// Assembly: System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Program Files\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Web.dll

using System.ComponentModel;
using System.Runtime;
using System.Web;
using System.Web.UI;

namespace System.Web.UI.WebControls
{
  [ControlValueProperty("Text")]
  [ControlBuilder(typeof (LabelControlBuilder))]
  [ParseChildren(false)]
  [DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  [Designer("System.Web.UI.Design.WebControls.LabelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  [ToolboxData("<{0}:Label runat=\"server\" Text=\"Label\"></{0}:Label>")]
  [DefaultProperty("Text")]
  public class Label : WebControl, ITextControl
  {
    [TypeConverter(typeof (AssociatedControlConverter))]
    [IDReferenceProperty]
    [DefaultValue("")]
    [WebCategory("Accessibility")]
    [WebSysDescription("Label_AssociatedControlID")]
    [Themeable(false)]
    public virtual string AssociatedControlID { get; set; }

    internal bool AssociatedControlInControlTree { get; set; }

    public override bool SupportsDisabledAttribute { get; }

    internal override bool RequiresLegacyRendering { get; }

    protected override HtmlTextWriterTag TagKey { get; }

    [Bindable(true)]
    [Localizable(true)]
    [WebCategory("Appearance")]
    [DefaultValue("")]
    [WebSysDescription("Label_Text")]
    [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
    public virtual string Text { get; set; }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public Label();

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    internal Label(HtmlTextWriterTag tag);

    protected override void AddAttributesToRender(HtmlTextWriter writer);

    protected override void AddParsedSubObject(object obj);

    protected override void LoadViewState(object savedState);

    protected internal override void RenderContents(HtmlTextWriter writer);
  }
}
