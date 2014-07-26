using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Our.Umbraco.uSocial.Extensions;
using BaseDataType = umbraco.cms.businesslogic.datatype.BaseDataType;
using DBTypes = umbraco.cms.businesslogic.datatype.DBTypes;

namespace Our.Umbraco.uSocial.DataType.Facebook
{
    /// <summary>
    /// The PreValue Editor for the Facebook OAuth authentication data type
    /// </summary>
    public class FacebookOAuthPreValueEditor : AbstractJsonPrevalueEditor
    {
        /// <summary>
        /// The app id
        /// </summary>
        protected TextBox txtAppId;

        /// <summary>
        /// The app secret
        /// </summary>
		protected TextBox txtAppSecret;

		/// <summary>
		/// The client token
		/// </summary>
		protected TextBox txtClientToken;

        /// <summary>
        /// The data format to retreive the value as
        /// </summary>
        protected RadioButtonList rdoDataFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookOAuthPreValueEditor"/> class.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        public FacebookOAuthPreValueEditor(BaseDataType dataType) 
            : base(dataType, DBTypes.Nvarchar)
        { }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // get PreValues, load them into the controls.
            var options = GetPreValueOptions<FacebookOAuthOptions>() ?? new FacebookOAuthOptions();

            // set the values
            txtAppId.Text = options.AppId;
			txtAppSecret.Text = options.AppSecret;
			txtClientToken.Text = options.ClientToken;
            rdoDataFormat.SelectedValue = options.DataFormat.ToString();
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            // add property fields
            writer.AddPrevalueRow("App ID", "Enter the app id for the Facebook application to authenticate against.", txtAppId);
            writer.AddPrevalueRow("App Secret", "Enter the app secret for the Facebook application to authenticate against.", txtAppSecret);
			writer.AddPrevalueRow("Client Token", "An optional client token to use for requests (Doesn't expire like regular access tokens, but has less permissions).", txtClientToken);
			writer.AddPrevalueRow("Data format", "Select the data format in which to store the value of this data type in.<br />XML if you intend to work with it in XSLT or JSON if you intend to work with it via Razor or C#.", rdoDataFormat);
        }

        /// <summary>
        /// Creates child controls for this control
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            // set-up child controls
            txtAppId = new TextBox { ID = "txtAppId", CssClass = "umbEditorTextField" };
            txtAppSecret = new TextBox { ID = "txtAppSecret", CssClass = "umbEditorTextField" };
			txtClientToken = new TextBox { ID = "txtClientToken", CssClass = "umbEditorTextField" };

            rdoDataFormat = new RadioButtonList { ID = "DataFormat" };
            rdoDataFormat.Items.Add(DataFormat.Xml.ToString());
            rdoDataFormat.Items.Add(DataFormat.Json.ToString());
            rdoDataFormat.RepeatDirection = RepeatDirection.Horizontal;

            // add the child controls
            Controls.AddPrevalueControls(txtAppId);
			Controls.AddPrevalueControls(txtAppSecret);
			Controls.AddPrevalueControls(txtClientToken);
            Controls.AddPrevalueControls(rdoDataFormat);
        }

        /// <summary>
        /// Saves the data-type PreValue options.
        /// </summary>
        public override void Save()
        {
            // set the options
            var options = new FacebookOAuthOptions
            {
                AppId = txtAppId.Text,
                AppSecret = txtAppSecret.Text,
				ClientToken = txtClientToken.Text,
                DataFormat = (DataFormat)Enum.Parse(typeof(DataFormat), rdoDataFormat.SelectedValue)
            };

            // save the options as JSON
            SaveAsJson(options);
        }
    }
}
