using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdventureWorks.DAL;
using AdventureWorks.Models;

namespace AdventureWorks
{
    public partial class MainForm1 : System.Web.UI.Page
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvKupci.DataSource = unitOfWork.Kupci.Get();
                gvKupci.DataBind();
                IEnumerable<Drzava> drzave = unitOfWork.Drzave.Get();
                ddlDrzava.Items.Add("-- odaberi --");
                drzave.ToList().ForEach(d => ddlDrzava.Items.Add(new ListItem(d.Naziv)));
                ddlDrzava.SelectedIndex = 0;
            }
        }

        protected void ddlPageSize_SelectedIndexChanged1(object sender, EventArgs e)
        {
            gvKupci.PageSize = int.Parse(ddlPageSize.SelectedValue);
            FilterByCity();
        }

        protected void ddlDrzava_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlDrzava.SelectedIndex != 0)
            {
                ddlGrad.Items.Clear();
                IEnumerable<Grad> gradovi = unitOfWork.Gradovi.Get(g => g.Drzava.Naziv == ddlDrzava.SelectedValue);
                ddlGrad.Items.Add("-- odaberi --");
                gradovi.ToList().ForEach(g => ddlGrad.Items.Add(new ListItem(g.Naziv)));
                ddlGrad.SelectedIndex = 0;
            }
        }

        protected void ddlGrad_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterByCity();
        }

        private void FilterByCity(string sortExpression = null)
        {
            string oldSortExpression = null;
            if(ViewState["sortExpression"] != null)
            {
                oldSortExpression = (string)ViewState["sortExpression"];
            }
            if (sortExpression != null)
            {
                ViewState["sortExpression"] = sortExpression;
            }
            bool sortOrder = false;
            if (ViewState["sortOrder"] != null)
            {
                sortOrder = (bool)ViewState["sortOrder"];
            }
            if (oldSortExpression == null && sortExpression == null)
            {
                gvKupci.DataSource = ddlGrad.SelectedIndex != 0 ?
                    unitOfWork.Kupci.Get(k => k.Grad.Naziv == ddlGrad.SelectedValue) :
                    unitOfWork.Kupci.Get();
                gvKupci.DataBind();
            }
            else
            {
                if(oldSortExpression != null && oldSortExpression == sortExpression)
                {
                    sortOrder = !sortOrder;
                    ViewState["sortOrder"] = sortOrder;
                }
                if(sortExpression == null)
                {
                    sortExpression = oldSortExpression;
                }
                if(sortExpression == "Ime")
                {
                    gvKupci.DataSource = ddlGrad.SelectedIndex != 0 ? 
                        unitOfWork.Kupci.Get(k => k.Grad.Naziv == ddlGrad.SelectedValue, q => sortOrder ? q.OrderBy(k => k.Ime) : q.OrderByDescending(k => k.Ime)) :
                        unitOfWork.Kupci.Get(null, q => sortOrder ? q.OrderBy(k => k.Ime) : q.OrderByDescending(k => k.Ime));
                    gvKupci.DataBind();
                }
                else if(sortExpression == "Prezime")
                {
                    gvKupci.DataSource = ddlGrad.SelectedIndex != 0 ? 
                        unitOfWork.Kupci.Get(k => k.Grad.Naziv == ddlGrad.SelectedValue, q => sortOrder ? q.OrderBy(k => k.Prezime) : q.OrderByDescending(k => k.Prezime)) :
                        unitOfWork.Kupci.Get(null, q => sortOrder ? q.OrderBy(k => k.Prezime) : q.OrderByDescending(k => k.Prezime));
                    gvKupci.DataBind();
                }

            }
        }

        protected void gvKupci_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow oldRow = gvKupci.Rows[e.NewEditIndex];
            string oldText = oldRow.Cells[4].Controls.OfType<Label>().First().Text;

            gvKupci.EditIndex = e.NewEditIndex;

            FilterByCity();

            GridViewRow row = gvKupci.Rows[e.NewEditIndex];
            DropDownList ddl = (DropDownList)row.FindControl("ddlSviGradovi");
            ddl.DataSource = unitOfWork.Gradovi.Get();
            ddl.DataBind();
            ddl.SelectedValue = oldText;

        }

        protected void gvKupci_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvKupci.EditIndex = -1;
            FilterByCity();
        }

        protected void gvKupci_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvKupci.Rows[e.RowIndex];
            int idKupac = (int)gvKupci.DataKeys[e.RowIndex].Value;

            string ime = row.Cells[0].Controls.OfType<TextBox>().First().Text;
            string prezime = row.Cells[1].Controls.OfType<TextBox>().First().Text;
            string email = row.Cells[2].Controls.OfType<TextBox>().First().Text;
            string telefon = row.Cells[3].Controls.OfType<TextBox>().First().Text;
            string gradText = row.Cells[4].Controls.OfType<DropDownList>().First().SelectedValue;

            Grad grad = unitOfWork.Gradovi.Get(g => g.Naziv == gradText).FirstOrDefault();
            Kupac kupac = unitOfWork.Kupci.GetByID(idKupac);

            kupac.Ime = ime;
            kupac.Prezime = prezime;
            kupac.Email = email;
            kupac.Telefon = telefon;
            kupac.Grad = grad;

            unitOfWork.Kupci.Update(kupac);
            unitOfWork.Commit();

            gvKupci.EditIndex = -1;
            FilterByCity();

        }

        protected void gvKupci_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvKupci.PageIndex = e.NewPageIndex;
            FilterByCity();
        }

        protected void gvKupci_Sorting(object sender, GridViewSortEventArgs e)
        {
            FilterByCity(e.SortExpression);
        }
        protected void gvKupci_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (e.NewSelectedIndex != -1)
            {
                GridViewRow row = gvKupci.Rows[e.NewSelectedIndex];
                int idKupac = (int)gvKupci.DataKeys[e.NewSelectedIndex].Value;

                Response.Redirect($"Racun/Get/{idKupac}");
            }
        }
    }
}