using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //DateTime aa = Convert.ToDateTime("17-08-15");

        //CultureInfo provider = CultureInfo.InvariantCulture;
        //DateTime aa = DateTime.ParseExact("17-08-15", "yyyy-MM-dd", provider);



        string DateString = "24-01-13";
        //IFormatProvider culture = new CultureInfo("en-US", true);
        DateTime dateVal = DateTime.ParseExact(DateString, "dd-MM-yy", CultureInfo.InvariantCulture);

    }
}