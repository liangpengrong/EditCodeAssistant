using Core.DefaultData.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.StaticMethod.Method.Utils {
   public static class EnumUtilsMet {
        /// <summary>
        /// 根据枚举获取对应的valueString值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(Enum value) {
            if (value == null) {
                throw new ArgumentException("value");
            }
            string description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            var attributes =
                (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }
        // 根据名称转换为默认命名枚举
        public static DefaultNameEnum GetStrToDefaultName(string name){ 
            foreach (DefaultNameEnum n in Enum.GetValues(typeof(DefaultNameEnum)))
            {
                if(GetDescription(n).Equals(name)) { 
                    return n;
                }
            }
            return DefaultNameEnum.NONE;
        }
        [AttributeUsage(AttributeTargets.Field,AllowMultiple = false)]  
        public sealed class EnumDescriptionAttribute : Attribute {
            public string Description { get; }
            public EnumDescriptionAttribute(string description)  
                : base()  
            {  
                Description = description;  
            }  
        }
   }
}
