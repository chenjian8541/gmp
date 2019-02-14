using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TTY.GMP.Jobs
{
    /// <summary>
    /// 公共逻辑
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 数字
        /// </summary>
        public const string NumberPattern = "^[0-9]*$";

        /// <summary>
        /// 商品规格解析
        /// 通过规格，计算出单位商品的重量
        ///
        ///1、规格中第一个数字≥5 商品销售重量=商品规格最小单位重量（规格中第一个数字）*单据销售数量／1000判断；单位g
        ///2、规格中第一个数字小于5商品销售重量=商品规格最小单位重量（规格中第一个数字，数字≥5）*单据销售数量；单位kg
        ///3、如果为空，或者第一个出现的不是数字，则返回0 
        /// </summary>
        /// <param name="goodsSpec"></param>
        /// <returns>重量(单位kg)</returns>
        public static decimal GoodsSpecAnalysis(string goodsSpec)
        {
            if (string.IsNullOrEmpty(goodsSpec))
            {
                return 0;
            }
            var firstStr = goodsSpec.Substring(0, 1);
            if (!Regex.IsMatch(firstStr, NumberPattern))
            {
                return 0;
            }
            var index = 0;
            var pointIndex = 0;
            for (var i = 0; i < goodsSpec.Length; i++)
            {
                if (Regex.IsMatch(goodsSpec[i].ToString(), NumberPattern))
                {
                    index = i;
                    continue;
                }
                if (goodsSpec[i] == '.' && pointIndex == 0)
                {
                    pointIndex = i;
                    continue;
                }
                break;
            }
            var value = Convert.ToDecimal(goodsSpec.Substring(0, index + 1));
            if (value >= 5)
            {
                return value / 1000;
            }
            return value;
        }

        /// <summary>
        /// 解析商品折百比
        /// 1、最后一个字符必须是“%”，如果未出现则为0
        /// </summary>
        /// <param name="goodsContents"></param>
        /// <returns></returns>
        public static decimal GoodsContentsAnalysis(string goodsContents)
        {
            if (string.IsNullOrEmpty(goodsContents))
            {
                return 0;
            }
            if (goodsContents.LastIndexOf('%') < 0)
            {
                return 0;
            }
            var value = goodsContents.Substring(0, goodsContents.Length - 1);
            if (!decimal.TryParse(value, out var contents))
            {
                return 0;
            }
            return contents / 100;
        }
    }
}
