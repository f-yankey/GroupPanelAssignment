using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Utils
{
    public class KebabCaseParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            if (value == null) return null;
            return Regex.Replace(value.ToString(),
            "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}
