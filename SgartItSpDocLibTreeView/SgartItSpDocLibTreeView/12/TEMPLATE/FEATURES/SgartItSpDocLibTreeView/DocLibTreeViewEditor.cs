using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;

namespace SgartIt.Sp
{
  public class DocLibTreeViewEditor
    : System.Web.UI.WebControls.WebParts.EditorPart
  {
    protected TextBox txtDocLibUrl;
    protected DropDownList ddlDepth;
    protected CheckBox chkShowItemTitle;
    protected CheckBox chkShowIconCss;
    protected CheckBox chkShowIconEdit;

    private const int MAXLEVEL = 50;

    public DocLibTreeViewEditor()
    {
      this.Title = "Sgart.it - DocLibTreeView";
    }

    protected override void OnLoad(System.EventArgs e)
    {
      this.EnsureChildControls();
      string url = SPContext.Current.Web.ServerRelativeUrl;
      //script
      StringBuilder sb = new StringBuilder(1000);
      sb.AppendFormat(@"
function sgartItSpDocLibTreeViewEditor(ctrlId) {{
  var p = window.open('{0}/_layouts/SgartItSpDocLibTreeView/Editor.aspx?ctrlId=' + ctrlId,'SgartItHtmlParametric','resizable=1,height=550,width=700');
  p.focus();
}}", url == "/" ? "" : url);
      this.Page.ClientScript.RegisterClientScriptBlock(typeof(DocLibTreeViewEditor)
        , "sgartItSpDocLibTreeViewEditor", sb.ToString(), true);

      //if (Page.IsPostBack == false)
      //{
      //}
      //base.OnLoad(e);
    }

    private void LoadDepth()
    {
      ddlDepth.Items.Add(new ListItem("Unlimited", MAXLEVEL.ToString()));
      for (int i = 1; i < 10; i++)
      {
        string s = i.ToString();
        ddlDepth.Items.Add(s);
      }
    }

    protected override void CreateChildControls()
    {
      txtDocLibUrl = new TextBox();
      txtDocLibUrl.ID = "txtDocLibUrl";
      txtDocLibUrl.CssClass = "UserInput";
      txtDocLibUrl.Style.Add("width", "176px");
      this.Controls.Add(txtDocLibUrl);

      ddlDepth = new DropDownList();
      ddlDepth.ID = "ddlDepth";
      ddlDepth.CssClass = "UserInput";
      ddlDepth.Style.Add("width", "176px");
      LoadDepth();
      this.Controls.Add(ddlDepth);

      chkShowItemTitle = new CheckBox();
      chkShowItemTitle.ID = "chkShowItemTitle";
      chkShowItemTitle.CssClass = "UserInput";
      this.Controls.Add(chkShowItemTitle);

      chkShowIconCss = new CheckBox();
      chkShowIconCss.ID = "chkShowIconCss";
      chkShowIconCss.CssClass = "UserInput";
      this.Controls.Add(chkShowIconCss);

      chkShowIconEdit = new CheckBox();
      chkShowIconEdit.ID = "chkShowIconEdit";
      chkShowIconEdit.CssClass = "UserInput";
      this.Controls.Add(chkShowIconEdit);

      base.CreateChildControls();
    }

    public override bool ApplyChanges()
    {
      this.EnsureChildControls();
      DocLibTreeView wp = (DocLibTreeView)this.WebPartToEdit;
      wp.DocLibUrl = txtDocLibUrl.Text;
      int depth = MAXLEVEL;
      if (ddlDepth.SelectedIndex != -1)
      {
        int.TryParse(ddlDepth.SelectedItem.Value, out depth);
      }
      wp.Depth = depth;
      wp.ShowItemTitle = chkShowItemTitle.Checked;
      wp.ShowIconCss = chkShowIconCss.Checked;
      wp.ShowIconEdit = chkShowIconEdit.Checked;
      return true;
    }

    public override void SyncChanges()
    {
      this.EnsureChildControls();
      DocLibTreeView wp = (DocLibTreeView)this.WebPartToEdit;
      txtDocLibUrl.Text = wp.DocLibUrl;
      ddlDepth.SelectedValue = wp.Depth.ToString();
      if (ddlDepth.SelectedIndex == -1)
      {
        ddlDepth.SelectedIndex = 0;
      }
      chkShowItemTitle.Checked = wp.ShowItemTitle;
      chkShowIconCss.Checked = wp.ShowIconCss;
      chkShowIconEdit.Checked = wp.ShowIconEdit;
    }

    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
      writer.Write("<div class=\"ms-ToolPartSpacing\"></div>");
      writer.Write("<div class=\"ms-TPBody\">");

      writer.Write("<div class=\"UserSectionHead\">DocLib Url</div>");
      writer.Write("<div class=\"UserSectionBody\">");
      txtDocLibUrl.RenderControl(writer);
      writer.Write("</div>");

      writer.Write("<div style=\"width: 100%\" class=\"userdottedline\"></div>");

      writer.Write("<div class=\"UserSectionHead\">Level to show</div>");
      writer.Write("<div class=\"UserSectionBody\">");
      ddlDepth.RenderControl(writer);
      writer.Write("</div>");

      writer.Write("<div style=\"width: 100%\" class=\"userdottedline\"></div>");

      writer.Write("<div class=\"UserSectionHead\">");
      chkShowItemTitle.RenderControl(writer);
      writer.Write(" Show item title instead of name");
      writer.Write("</div>");

      writer.Write("<div class=\"UserSectionHead\">");
      chkShowIconCss.RenderControl(writer);
      writer.Write(" Show Css icon");
      writer.Write("</div>");

      writer.Write("<div class=\"UserSectionHead\">");
      chkShowIconEdit.RenderControl(writer);
      writer.Write(" Show Edit icon");
      writer.Write("</div>");

      //writer.Write("<div style=\"width: 100%\" class=\"userdottedline\"></div>");
      writer.Write("</div>");
    }
  }
}
