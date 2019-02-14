using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Authority
{
    /// <summary>
    /// 权限核心算法(仅支持1-31的权限值)
    /// </summary>
    public class AuthorityCore2
    {
        /// <summary>
        /// 权限权值总和
        /// </summary>
        private static uint _weightSum;

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public AuthorityCore2()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="weightSum">初始权限值</param>
        public AuthorityCore2(uint weightSum)
        {
            _weightSum = weightSum;
        }

        /// <summary>
        /// 验证是否有权限
        /// </summary>
        /// <param name="weightSum">权限总权重</param>
        /// <param name="value">权限值</param>
        /// <returns>是否拥有权限</returns>
        public bool Validation(uint weightSum, uint value)
        {
            uint valueWeight = (uint)Math.Pow(2, value);
            return valueWeight == (weightSum & valueWeight);
        }

        /// <summary>
        /// 验证是否有权限
        /// </summary>
        /// <param name="value">权限值</param>
        /// <returns>是否拥有权限</returns>
        public bool Validation(uint value)
        {
            return Validation(_weightSum, value);
        }

        /// <summary>
        /// 验证是否有组合权限
        /// </summary>
        /// <param name="values">权限值列表</param>
        /// <returns>是否拥有权限</returns>
        public bool CombineValidation(params uint[] values)
        {
            uint valueWeight = 0;
            for (int i = 0; i < values.Length; i++)
            {
                valueWeight += (uint)Math.Pow(2, values[i]);
            }

            return valueWeight == (_weightSum & valueWeight);
        }

        /// <summary>
        /// 得到权限值的权重(输入范围为0 - 31之间)
        /// </summary>
        /// <param name="value">权限值</param>
        /// <returns>权重值</returns>
        public uint GetWeigth(uint value)
        {
            var result = (int)Math.Pow(2, value);
            return (uint)result;
        }

        /// <summary>
        /// 获得当前总权重值(持久化的凭证)
        /// </summary>
        /// <returns></returns>
        public uint GetWeigth()
        {
            return _weightSum;
        }

        /// <summary>
        /// 注册权限值
        /// </summary>
        /// <param name="value">权限值</param>
        /// <returns>返回真表示注册成功,返回假表示已经注册过此权限</returns>
        public bool RegisterAuthority(uint value)
        {
            if (!Validation(value))
            {
                _weightSum += (uint)Math.Pow(2, (int)value);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 计算用户权限总值
        /// </summary>
        /// <param name="values">权限值数组</param>
        /// <returns></returns>
        public uint AuthoritySum(params uint[] values)
        {
            uint weightSum = 0;
            for (var i = 0; i < values.Length; i++)
            {
                if (Validation(values[i]))
                {
                    continue;
                }
                weightSum += GetWeigth(values[i]);
            }
            return weightSum;
        }
    }
}
