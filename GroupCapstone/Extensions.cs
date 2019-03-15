using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace GroupCapstone
{
    public static class Extensions
    {
           public static string GetDescription(this Enum e)
           {
            var attribute =
                e.GetType()
                    .GetTypeInfo()
                    .GetMember(e.ToString())
                    .FirstOrDefault(member => member.MemberType == MemberTypes.Field)
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .SingleOrDefault()
                    as System.ComponentModel.DescriptionAttribute;

            return attribute?.Description ?? e.ToString();
            }
    }
}