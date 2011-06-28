using System;
using System.Collections.Generic;
using System.Text;

namespace SgartIt.Sp
{
  public class STreeItem
  {
    #region Private properties

    private int id = 0;
    private string title = "";
    private string name = "";
    private string url = "";
    private string contentType = "";
    private string serverRelativeUrl = "";
    private string editServerRelativeUrl = "";
    private int depth;
    private STreeItem parent = null;
    private List<STreeItem> subItems = new List<STreeItem>();
    private Dictionary<string, string> fields = new Dictionary<string, string>();

    #endregion

    public int ID
    {
      get { return id; }
      set { id = value; }
    }

    public string Title
    {
      get { return title; }
      set { title = value; }
    }

    public string Name
    {
      get { return name; }
      set { name = value; }
    }


    public string Url
    {
      get { return url; }
      set { url = value; }
    }

    public string ContentType
    {
      get { return contentType; }
      set { contentType = value; }
    }


    public string ServerRelativeUrl
    {
      get { return serverRelativeUrl; }
      set { serverRelativeUrl = value; }
    }

    public string EditServerRelativeUrl
    {
      get { return editServerRelativeUrl; }
      set { editServerRelativeUrl = value; }
    }

    public int Depth
    {
      get { return depth; }
      set { depth = value; }
    }

    public STreeItem Parent
    {
      get { return parent; }
      set { parent = value; }
    }


    public List<STreeItem> SubItems
    {
      get { return subItems; }
      set { subItems = value; }
    }

    public string this[string name]
    {
      get { return fields[name]; }
      set { fields[name] = value; }
    }
	


  }
}
