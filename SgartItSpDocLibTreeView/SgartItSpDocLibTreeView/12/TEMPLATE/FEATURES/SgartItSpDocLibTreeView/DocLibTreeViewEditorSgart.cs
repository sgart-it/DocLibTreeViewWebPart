using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace SgartIt.Sp
{
  public class DocLibTreeViewEditorSgart 
    : System.Web.UI.WebControls.WebParts.EditorPart
  {
    protected HyperLink lnk;

    public DocLibTreeViewEditorSgart()
    {
      this.Title = "Info";
     
    }

    protected override void CreateChildControls()
    {
      lnk = new HyperLink();
      lnk.Target = "_blank";
      lnk.NavigateUrl = Helper.SGART_URL;
      lnk.Text = Helper.SGART_TITLE;
      this.Controls.Add(lnk);
      
      base.CreateChildControls();
    }

    public override bool ApplyChanges()
    {
      this.EnsureChildControls();
      return true;
    }

    public override void SyncChanges()
    {
      this.EnsureChildControls();
    }

    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
      writer.Write("<div class=\"ms-ToolPartSpacing\"></div>");
      writer.Write("<div class=\"ms-TPBody\">");

      writer.Write("<div class=\"UserSectionBody\">");
      lnk.RenderControl(writer);
      writer.Write("</div>");

      writer.Write("</div>");
    }
  }
}
