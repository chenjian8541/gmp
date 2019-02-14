using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TTY.GMP.WebApi.Serializer
{
    /// <summary>
    /// 将string为null的类型转换成空字符串
    /// </summary>
    public class NullToEmptyStringValueProvider : IValueProvider
    {
        /// <summary>
        /// 属性
        /// </summary>
        private readonly PropertyInfo _memberInfo;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="memberInfo"></param>
        public NullToEmptyStringValueProvider(PropertyInfo memberInfo)
        {
            _memberInfo = memberInfo;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public object GetValue(object target)
        {
            object result = _memberInfo.GetValue(target);
            if (_memberInfo.PropertyType == typeof(string) && result == null)
            {
                return string.Empty;
            }
            return result;
        }

        /// <summary>
        ///设置值
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public void SetValue(object target, object value)
        {
            _memberInfo.SetValue(target, value);
        }
    }
}
