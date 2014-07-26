using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Our.Umbraco.uSocial.Extensions;

namespace Our.Umbraco.uSocial.Models
{
    public class CallbackOptions
    {
        public int DtdId { get; set; }
        public string WrapperId { get; set; }

        public override string ToString()
        {
            var json = this.SerializeToJson();
            return json.Stash();
        }

        public static bool TryParse(string input, out CallbackOptions opts)
        {
            opts = null;

            try
            {
                var json = input.Unstash();
                opts = json.DeserializeJsonTo<CallbackOptions>();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static CallbackOptions Parse(string input)
        {
            CallbackOptions opts;
            CallbackOptions.TryParse(input, out opts);
            return opts;
        }
    }
}