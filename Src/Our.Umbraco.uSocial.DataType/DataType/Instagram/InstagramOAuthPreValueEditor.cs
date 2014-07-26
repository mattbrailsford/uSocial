using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Our.Umbraco.uSocial.Extensions;
using BaseDataType = umbraco.cms.businesslogic.datatype.BaseDataType;
using DBTypes = umbraco.cms.businesslogic.datatype.DBTypes;

namespace Our.Umbraco.uSocial.DataType.Instagram
{
    /// <summary>
    /// The PreValue Editor for the Instagram OAuth authentication data type
    /// </summary>
    public class InstagramOAuthPreValueEditor : AbstractJsonPrevalueEditor
    {
        /// <summary>
        /// The client id
        /// </summary>
        protected TextBox txtClientId;

        /// <summary>
        /// The client secret
        /// </summary>
        protected TextBox txtClientSecret;

        /// <summary>
        /// The data format to retreive the value as
        /// </summary>
        protected RadioButtonList rdoDataFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstagramOAuthPreValueEditor"/> class.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        public InstagramOAuthPreValueEditor(BaseDataType dataType) 
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
            var options = GetPreValueOptions<InstagramOAuthOptions>() ?? new InstagramOAuthOptions();

            // set the values
            txtClientId.Text = options.ClientId;
            txtClientSecret.Text = options.ClientSecret;
            rdoDataFormat.SelectedValue = options.DataFormat.ToString();
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            // add property fields
            writer.AddPrevalueRow("Client ID", "Enter the client id for the Instagram application to authenticate against.", txtClientId);
            writer.AddPrevalueRow("Client Secret", "Enter the client secret for the Instagram application to authenticate against.", txtClientSecret);
            writer.AddPrevalueRow("Data format", "Select the data format in which to store the value of this data type in.<br />XML if you intend to work with it in XSLT or JSON if you intend to work with it via Razor or C#.", rdoDataFormat);
        }

        /// <summary>
        /// Creates child controls for this control
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            // set-up child controls
            txtClientId = new TextBox { ID = "txtClientId", CssClass = "umbEditorTextField" };
            txtClientSecret = new TextBox { ID = "txtClientSecret", CssClass = "umbEditorTextField" };

            rdoDataFormat = new RadioButtonList { ID = "DataFormat" };
            rdoDataFormat.Items.Add(DataFormat.Xml.ToString());
            rdoDataFormat.Items.Add(DataFormat.Json.ToString());
            rdoDataFormat.RepeatDirection = RepeatDirection.Horizontal;

            // add the child controls
            Controls.AddPrevalueControls(txtClientId);
            Controls.AddPrevalueControls(txtClientSecret);
            Controls.AddPrevalueControls(rdoDataFormat);
        }

        /// <summary>
        /// Saves the data-type PreValue options.
        /// </summary>
        public override void Save()
        {
            // set the options
            var options = new InstagramOAuthOptions
            {
                ClientId = txtClientId.Text,
                ClientSecret = txtClientSecret.Text,
                DataFormat = (DataFormat)Enum.Parse(typeof(DataFormat), rdoDataFormat.SelectedValue)
            };

            // save the options as JSON
            SaveAsJson(options);
        }
    }
}
