using System;
using umbraco.cms.businesslogic.datatype;
using umbraco.interfaces;

namespace Our.Umbraco.uSocial.DataType.Facebook
{
    /// <summary>
    /// A Facebook OAuth authentication data type
    /// </summary>
    public class FacebookOAuthDataType : AbstractDataEditor
    {
        /// <summary>
        /// The Data Editor for the data-type.
        /// </summary>
        private FacebookOAuthDataEditor _dataEditor;

        /// <summary>
        /// The PreValue Editor for the data-type.
        /// </summary>
        private FacebookOAuthPreValueEditor _preValueEditor;

        /// <summary>
        /// The Data for the data-type.
        /// </summary>
        private FacebookOAuthData _data;

        /// <summary>
        /// Gets the id of the data-type.
        /// </summary>
        /// <value>
        /// The id of the data-type.
        /// </value>
        public override Guid Id
        {
            get { return new Guid("45B80582-9BFF-4E47-9568-3E0FDB272D5C"); }
        }

        /// <summary>
        /// Gets the name of the data type.
        /// </summary>
        /// <value>
        /// The name of the data type.
        /// </value>
        public override string DataTypeName
        {
            get { return "Facebook OAuth Data Type"; }
        }

        /// <summary>
        /// Gets the prevalue editor.
        /// </summary>
        /// <value>
        /// The prevalue editor.
        /// </value>
        public override IDataEditor DataEditor
        {
            get { return _dataEditor ?? (_dataEditor = new FacebookOAuthDataEditor(Data, ((FacebookOAuthPreValueEditor)PrevalueEditor).GetPreValueOptions<FacebookOAuthOptions>(), DataTypeDefinitionId)); }
        }

        /// <summary>
        /// Gets the prevalue editor.
        /// </summary>
        /// <value>
        /// The prevalue editor.
        /// </value>
        public override IDataPrevalue PrevalueEditor
        {
            get { return _preValueEditor ?? (_preValueEditor = new FacebookOAuthPreValueEditor(this)); }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public override IData Data
        {
            get { return _data ?? (_data = new FacebookOAuthData(this, ((FacebookOAuthPreValueEditor)PrevalueEditor).GetPreValueOptions<FacebookOAuthOptions>())); }
        }
    }
}
