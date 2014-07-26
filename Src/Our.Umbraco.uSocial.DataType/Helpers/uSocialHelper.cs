using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Our.Umbraco.uSocial.DataType;
using umbraco.cms.businesslogic.datatype;

namespace Our.Umbraco.uSocial.Helpers
{
    internal static class uSocialHelper
    {
        /// <summary>
        /// Gets the pre value options by id.
        /// </summary>
        /// <param name="dtdId">The DateTypeDefinition id.</param>
        /// <returns></returns>
        internal static TOptionType GetPreValueOptionsById<TOptionType, TPreValueEditorType>(int dtdId)
            where TPreValueEditorType : AbstractJsonPrevalueEditor
        {
            var dtd = new DataTypeDefinition(dtdId);
            var pve = (TPreValueEditorType)dtd.DataType.PrevalueEditor;
            return pve.GetPreValueOptions<TOptionType>();
        }
    }
}