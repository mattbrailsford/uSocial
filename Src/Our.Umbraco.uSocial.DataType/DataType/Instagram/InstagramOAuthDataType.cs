using System;
using umbraco.cms.businesslogic.datatype;
using umbraco.interfaces;

namespace Our.Umbraco.uSocial.DataType.Instagram
{
    /// <summary>
    /// A Instagram OAuth authentication data type
    /// </summary>
    public class InstagramOAuthDataType : AbstractDataEditor
    {
        /// <summary>
        /// The Data Editor for the data-type.
        /// </summary>
        private InstagramOAuthDataEditor _dataEditor;

        /// <summary>
        /// The PreValue Editor for the data-type.
        /// </summary>
        private InstagramOAuthPreValueEditor _preValueEditor;

        /// <summary>
        /// The Data for the data-type.
        /// </summary>
        private InstagramOAuthData _data;

        /// <summary>
        /// Gets the id of the data-type.
        /// </summary>
        /// <value>
        /// The id of the data-type.
        /// </value>
        public override Guid Id
        {
            get { return new Guid("f528e07f-6fc7-4b3d-8aea-5274163566b4"); }
        }

        /// <summary>
        /// Gets the name of the data type.
        /// </summary>
        /// <value>
        /// The name of the data type.
        /// </value>
        public override string DataTypeName
        {
            get { return "Instagram OAuth Data Type"; }
        }

        /// <summary>
        /// Gets the prevalue editor.
        /// </summary>
        /// <value>
        /// The prevalue editor.
        /// </value>
        public override IDataEditor DataEditor
        {
            get { return _dataEditor ?? (_dataEditor = new InstagramOAuthDataEditor(Data, ((InstagramOAuthPreValueEditor)PrevalueEditor).GetPreValueOptions<InstagramOAuthOptions>(), DataTypeDefinitionId)); }
        }

        /// <summary>
        /// Gets the prevalue editor.
        /// </summary>
        /// <value>
        /// The prevalue editor.
        /// </value>
        public override IDataPrevalue PrevalueEditor
        {
            get { return _preValueEditor ?? (_preValueEditor = new InstagramOAuthPreValueEditor(this)); }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public override IData Data
        {
            get { return _data ?? (_data = new InstagramOAuthData(this, ((InstagramOAuthPreValueEditor)PrevalueEditor).GetPreValueOptions<InstagramOAuthOptions>())); }
        }
    }
}
