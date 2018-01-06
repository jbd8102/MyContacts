using MyContacts.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MyContacts
{
    public partial class _Default : Page
    {
        #region Property

        private ContactManagerGoogle _contactManagerGoogle;
        private ContactManagerGoogle ContactManagerGoogle
        {
            get
            {
                if (_contactManagerGoogle == null)
                {
                    _contactManagerGoogle = new ContactManagerGoogle();
                }

                return _contactManagerGoogle;
            }
        }

        #endregion

        #region Methods

        private void GetAuthorisationFromGoogle()
        {
            string vUrl = string.Empty;

            try
            {
                vUrl = string.Format("{0}{1}&response_type=code&client_id={2}&scope={3}",
                    LocalConstants.GetGoogleAccountUrl, LocalConstants.GetRedirectUrl, LocalConstants.GetClientID, LocalConstants.GetGoogleScopeUrl);

                Response.Redirect(vUrl);
            }
            catch (Exception vException)
            {
                throw vException;
            }
        }

        // TODO : just un test à supprimer
        private List<EContact> GetListContactsMock()
        {
            List<EContact> vListOfContacts = new List<EContact>
            {
                new EContact {Email = "jbd8102@outlook.fr", FullName="1.Jawad BYAD", Photo=null},
                new EContact {Email = "jbd8102@outlook.fr", FullName="2.Jawad BYAD", Photo=null},
                new EContact {Email = "jbd8102@outlook.fr", FullName="3.Jawad BYAD", Photo=null}
            };
            
            rpContacts.DataSource = vListOfContacts;
            rpContacts.DataBind();
            imgClickExport.Visible = (vListOfContacts != null && vListOfContacts.Count > 0) ? true : false;

            return vListOfContacts;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            List<EContact> vListOfContacts = null;

            try
            {
                if (Request.QueryString["code"] != null)
                {
                    vListOfContacts = ContactManagerGoogle.GetContacts(Request.QueryString["code"]);
                    var vQuery = from contact in vListOfContacts orderby contact.FullName ascending select contact;
                    rpContacts.DataSource = vQuery;
                    rpContacts.DataBind();
                    imgClickExport.Visible = (vListOfContacts != null && vListOfContacts.Count > 0) ? true : false;
                }
                else
                {
                    GetAuthorisationFromGoogle();
                }
            }
            catch (Exception vException)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "alert('" + vException.Message.Replace("'", @"\'").Replace("`", @"\'").Replace("’", @"\'") + "');", true);
            }
        }              

        protected void rpContacts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            EContact vEContact = null;

            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    vEContact = (EContact)e.Item.DataItem;

                    (e.Item.FindControl("lblEmail") as Label).Text = vEContact.Email;
                    (e.Item.FindControl("lblFullName") as Label).Text = vEContact.FullName;
                }
            }
            catch (Exception vException)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "alert('" + vException.Message.Replace("'", @"\'").Replace("`", @"\'").Replace("’", @"\'") + "');", true);
            }
        }        

        protected void imgClickExport_Click(object sender, ImageClickEventArgs e)
        {            
            string vCsvContenu = string.Empty;
            StringBuilder vStringBuilder = new StringBuilder();
            HtmlTableRow vHtmlTableRow = null;
            string fileName = string.Empty;

            try
            {
                for (int i = 0; i <= rpContacts.Items.Count; i++)
                {
                    RepeaterItem item = (rpContacts.Controls[i] as RepeaterItem);
                    if(item is null)
                    {
                        return;
                    }
                    else
                    {
                        if(item.ItemType == ListItemType.Header)
                        {
                            // Get header
                            vHtmlTableRow = rpContacts.Controls[0].FindControl("trHeader") as HtmlTableRow;
                            // Column Email
                            vStringBuilder.Append(vHtmlTableRow.Cells[0].InnerText);
                            vStringBuilder.Append(LocalConstants.GetSeparator);
                            // Column FullName
                            vStringBuilder.Append(vHtmlTableRow.Cells[1].InnerText);
                            vStringBuilder.Append(LocalConstants.GetSeparator);
                            ////append new line
                            vStringBuilder.Append("\r\n");
                        }
                        else if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                        {
                            // Column Email
                            vStringBuilder.Append((rpContacts.Controls[i].FindControl("lblEmail") as Label).Text);
                            vStringBuilder.Append(LocalConstants.GetSeparator);
                            // Column FullName
                            vStringBuilder.Append((rpContacts.Controls[i].FindControl("lblFullName") as Label).Text);
                            vStringBuilder.Append(LocalConstants.GetSeparator);
                            ////append new line
                            vStringBuilder.Append("\r\n");
                        }
                    }
                }
                // Write string to CSV file
                GenericUtils.WriteToCSV(vStringBuilder.ToString(), LocalConstants.GetGoogleContactExportFileName);
            }
            catch (Exception vException)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "alert('" + vException.Message.Replace("'", @"\'").Replace("`", @"\'").Replace("’", @"\'") + "');", true);
            }
        }

        #endregion
    }
}