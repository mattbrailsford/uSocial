﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Our.Umbraco.uSocial.Extensions;
using umbraco.interfaces;
using umbraco.cms.businesslogic.datatype;

[assembly: WebResource("Our.Umbraco.uSocial.Resources.Styles.PrevalueEditor.css", "text/css")]

namespace Our.Umbraco.uSocial.DataType
{
    /// <summary>
    /// Abstract class for the PreValue Editor.
    /// </summary>
    public abstract class AbstractPrevalueEditor : WebControl, IDataPrevalue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractPrevalueEditor"/> class.
        /// </summary>
        protected AbstractPrevalueEditor()
            : base()
        {
        }

        /// <summary>
        /// Gets the editor.
        /// </summary>
        /// <value>The editor.</value>
        public virtual Control Editor
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public virtual void Save()
        {
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.EnsureChildControls();

            // Adds the client dependencies.
            this.RegisterEmbeddedClientResource(typeof(AbstractPrevalueEditor), "Our.Umbraco.uSocial.Resources.Styles.PrevalueEditor.css", ClientDependencyType.Css);
        }

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "PrevalueEditor");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            base.RenderBeginTag(writer);
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            base.RenderEndTag(writer);

            writer.RenderEndTag();
        }
    }
}