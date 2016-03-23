using Calculator.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Calculator
{
    public partial class frmCalculator : System.Web.UI.Page
    {
        
        #region Events
       
        protected void btnEqual_Click(object sender, EventArgs e)
        {
            if (validateOperation())
            {
                Operations op = new Operations(txtOperation.Text.Trim());
                txtOperation.Text = op.get_Result();
            }
        }

        protected void btnClean_Click(object sender, EventArgs e)
        {
            ClearOperation();
        }
        #endregion

        #region Methods

        //Reset fields
        private void ClearOperation()
        {
            txtOperation.Text = "";
            txtErrorMessage.Text = "";
            txtErrorMessage.Visible = false;
        }

        //Method to validate the input
       private bool  validateOperation()
        {
            bool isValidOperation = true;

           //Validae if is a blank
           if(txtOperation.Text.Trim() == "")
           {
               isValidOperation = false;
               txtErrorMessage.Text = "Please type a operation";
               txtErrorMessage.Visible = true;
           }
           else
           {
               txtErrorMessage.Text = "";
               txtErrorMessage.Visible = false;
           }

           return isValidOperation;
        }

        #endregion

      
    }
}