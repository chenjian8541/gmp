using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TTY.GMP.Authority
{
    /// <summary>
    /// 权限核心算法(支持大数处理,权限值只能是大于0的数)
    /// </summary>
    public class AuthorityCore
    {
        /// <summary>
        /// 权限权值总和
        /// </summary>
        private BigInteger _weightSum = 0;

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public AuthorityCore()
        { }

        /// <summary>
        /// 初始权限值
        /// </summary>
        /// <param name="weightSum"></param>
        public AuthorityCore(BigInteger weightSum)
        {
            _weightSum = weightSum;
        }

        /// <summary>
        /// 验证是否有权限
        /// </summary>
        /// <param name="weightSum">权限总权重</param>
        /// <param name="value">权限值</param>
        /// <returns>是否拥有权限</returns>
        public bool Validation(BigInteger weightSum, int value)
        {
            var valueWeight = BigInteger.Pow(2, value);
            return valueWeight == (weightSum & valueWeight);
        }

        /// <summary>
        /// 验证是否有权限
        /// </summary>
        /// <param name="value">权限值</param>
        /// <returns>是否拥有权限</returns>
        public bool Validation(int value)
        {
            return Validation(_weightSum, value);
        }

        /// <summary>
        /// 验证是否有组合权限
        /// </summary>
        /// <param name="values">权限值列表</param>
        /// <returns>是否拥有权限</returns>
        public bool CombineValidation(params int[] values)
        {
            BigInteger valueWeight = 0;
            for (int i = 0; i < values.Length; i++)
            {
                valueWeight += BigInteger.Pow(2, values[i]);
            }

            return valueWeight == (_weightSum & valueWeight);
        }

        /// <summary>
        /// 得到权限值的权重(输入范围为0 - 31之间)
        /// </summary>
        /// <param name="value">权限值</param>
        /// <returns>权重值</returns>
        public BigInteger GetWeigth(int value)
        {
            var result = BigInteger.Pow(2, value);
            return result;
        }

        /// <summary>
        /// 获得当前总权重值(持久化的凭证)
        /// </summary>
        /// <returns></returns>
        public BigInteger GetWeigth()
        {
            return _weightSum;
        }

        /// <summary>
        /// 注册权限值
        /// </summary>
        /// <param name="value">权限值(必须大于0)</param>
        /// <returns>返回真表示注册成功,返回假表示已经注册过此权限</returns>
        public bool RegisterAuthority(int value)
        {
            if (!Validation(value))
            {
                _weightSum += BigInteger.Pow(2, value);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 削弱权限
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WeakenAuthority(int value)
        {
            if (Validation(value))
            {
                _weightSum -= BigInteger.Pow(2, value);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 计算用户权限总值
        /// </summary>
        /// <param name="values">权限值数组</param>
        /// <returns></returns>
        public BigInteger AuthoritySum(params int[] values)
        {
            BigInteger weightSum = 0;
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
