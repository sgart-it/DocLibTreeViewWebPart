using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;
using System.Text;

namespace SgartIt.Sp
{
  [Guid("8d010a5f-a1c2-4580-b092-f5d48b3ce351")]
  public class DocLibTreeView : System.Web.UI.WebControls.WebParts.WebPart
  {
    #region Private properties

    private string docLibUrl = "";
    private int depth = 2;
    private bool showIconCss = false;
    private bool showIconEdit = false;
    private bool showItemTitle = false;
    private string jsBody = "";

    protected LiteralControl lc;

    private SPWeb web;
    private string editUrl = "";

    #endregion


    public DocLibTreeView()
    {
      this.ExportMode = WebPartExportMode.All;
    }

    #region Public properties

    [WebBrowsable(false)]
    [Personalizable(PersonalizationScope.Shared)]
    [WebPartStorage(Storage.Shared)]
    public string DocLibUrl
    {
      get { return this.docLibUrl; }
      set { this.docLibUrl = value.Trim(); }
    }

    [WebBrowsable(false)]
    [Personalizable(PersonalizationScope.Shared)]
    [WebPartStorage(Storage.Shared)]
    public int Depth
    {
      get { return this.depth; }
      set { this.depth = value > 1 ? value : 1; }
    }

    [WebBrowsable(false)]
    [Personalizable(PersonalizationScope.Shared)]
    [WebPartStorage(Storage.Shared)]
    public bool ShowIconCss
    {
      get { return this.showIconCss; }
      set { this.showIconCss = value; }
    }

    [WebBrowsable(false)]
    [Personalizable(PersonalizationScope.Shared)]
    [WebPartStorage(Storage.Shared)]
    public bool ShowIconEdit
    {
      get { return this.showIconEdit; }
      set { this.showIconEdit = value; }
    }

    [WebBrowsable(false)]
    [Personalizable(PersonalizationScope.Shared)]
    [WebPartStorage(Storage.Shared)]
    public bool ShowItemTitle
    {
      get { return this.showItemTitle; }
      set { this.showItemTitle = value; }
    }

    [WebBrowsable(false)]
    [Personalizable(PersonalizationScope.Shared)]
    [WebPartStorage(Storage.Shared)]
    public string JsBody
    {
      get { return this.jsBody; }
      set { this.jsBody = value; }
    }
    #endregion

    #region Toolpane Editor

    public override EditorPartCollection CreateEditorParts()
    {
      List<EditorPart> parts = new List<EditorPart>();
      if (WebPartManager.Personalization.Scope == PersonalizationScope.Shared)
      {
        DocLibTreeViewEditor editor = new DocLibTreeViewEditor();
        editor.ID = string.Format("{0}_{1}", this.ID, Helper.KEY);
        parts.Add(editor);

      }

      DocLibTreeViewEditorSgart editorSgart = new DocLibTreeViewEditorSgart();
      editorSgart.ID = string.Format("{0}_{1}c", this.ID, Helper.KEY);
      parts.Add(editorSgart);

      return new EditorPartCollection(base.CreateEditorParts(), parts); ;
    }
    #endregion

    #region Load Tree

    protected override void CreateChildControls()
    {
      string s = "";
      if (string.IsNullOrEmpty(DocLibUrl))
      {
        s = string.Format(@"
Html Parametric by 
<a href=""{0}"" target=""_blank"">{1}</a>
<br />
<a id=""HtmlParametricWebPart_OpenToolPane_{2}"" href=""#"" onclick=""javascript:MSOTlPn_ShowToolPane2('Edit','{3}');"">Open the tool pane</a>
to configure this Web Part."
          , Helper.SGART_URL, Helper.SGART_TITLE
          , this.ClientID, this.ID);
      }
      else
      {
        try
        {
          web = SPContext.Current.Web;
          SPList list = web.GetList(docLibUrl);
          editUrl = list.Forms[PAGETYPE.PAGE_EDITFORM].ServerRelativeUrl;
          SPFolder folder = list.RootFolder;

          STreeItem root = GetNode(folder, null);

          LoadTree(folder, root);
          s = ShowTree(root);
        }
        catch (Exception ex)
        {
          s = string.Format("### Error: {0}", ex);
        }
      }

      lc = new LiteralControl(s);
      this.Controls.Add(lc);

      base.CreateChildControls();
    }

    private STreeItem GetNode(SPFolder folder, STreeItem parent)
    {
      STreeItem node = new STreeItem();
      SPListItem item = folder.Item;
      if (item == null && parent != null) return null;
      if (item == null)
        node.ID = 0;
      else
        node.ID = item.ID;
      if (parent == null)
      {
        node.Depth = 0;
      }
      else
      {
        node.Depth = parent.Depth + 1;
      }
      node.Title = folder.Name;
      node.Name = folder.Name;
      if (item != null)
      {
        node.Title = item.Title;
      }
      else
      {
        node.Title = folder.Name;
      }
      node.ServerRelativeUrl = folder.ServerRelativeUrl;
      if (showIconEdit == true && item != null
        && item.DoesUserHavePermissions(SPBasePermissions.EditListItems) == true)
      {
        if (string.IsNullOrEmpty(item.ContentType.EditFormUrl) == false)
        {
          node.EditServerRelativeUrl = string.Format("{0}?ID={1}"
            , item.ContentType.EditFormUrl, item.ID);
        }
        else
        {
          node.EditServerRelativeUrl = string.Format("{0}?ID={1}"
            , editUrl, item.ID);
        }
      }
      else
      {
        node.EditServerRelativeUrl = "";
      }
      node.Url = folder.Url;
      return node;
    }

    private void LoadTree(SPFolder parentFolder, STreeItem parent)
    {
      foreach (SPFolder folder in parentFolder.SubFolders)
      {
        STreeItem currentItem = GetNode(folder, parent);
        if (currentItem != null)
        {
          parent.SubItems.Add(currentItem);
          LoadTree(folder, currentItem);
        }
      }
    }

    #endregion

    #region Show tree

    protected override void Render(HtmlTextWriter writer)
    {
      writer.Write("<!-- begin: SgartIt.Sp.DocLibTreeVie -->");
      lc.RenderControl(writer);
      writer.Write("<!-- end: SgartIt.Sp.DocLibTreeView -->");
    }

    private string ShowTree(STreeItem currentItem)
    {
      if (depth > currentItem.Depth  && currentItem.SubItems.Count > 0)
      {
        StringBuilder sb = new StringBuilder(500);
        sb.AppendFormat("<ul class=\"sgartdltw\">");
        foreach (STreeItem item in currentItem.SubItems)
        {
          sb.AppendFormat("<li class=\"level{0}\">", currentItem.Depth);
          if (item.EditServerRelativeUrl != "")
          {
            sb.AppendFormat("<a href=\"javascript:GoToPageRelative('{1}');\"><img src=\"{0}\" border=\"0\" alt=\"Edit\" /></a>&#160;"
              , web.ServerRelativeUrl.TrimEnd('/') + Helper.IMG_EDIT
              , item.EditServerRelativeUrl);
          }
          sb.AppendFormat("<a href=\"{3}\" title=\"{0} - {1} - {2}\">{0}</a>"
            , showItemTitle ? item.Title : item.Name
            , item.ID, item.Depth
            , item.ServerRelativeUrl);
          //recurse
          sb.Append(ShowTree(item));
          sb.Append("</li>");
        }
        sb.AppendFormat("</ul>");
        return sb.ToString();
      }
      else
      {
        return null;
      }
    }

    #endregion
  }
}
