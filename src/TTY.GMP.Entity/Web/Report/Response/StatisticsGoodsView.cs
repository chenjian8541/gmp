using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Web.Report.Response
{
    /// <summary>
    /// 统计的商品信息
    /// </summary>
    public abstract class StatisticsGoodsView
    {
        /// <summary>
        /// 除草剂
        /// </summary>
        private decimal _herbicide;

        /// <summary>
        /// 除草剂
        /// </summary>
        public decimal Herbicide
        {
            get
            {
                return _herbicide;
            }
            set { _herbicide = Convert.ToDecimal(value.ToString("F2")); }
        }

        /// <summary>
        /// 杀菌剂
        /// </summary>
        private decimal _fungicide;

        /// <summary>
        /// 杀菌剂
        /// </summary>
        public decimal Fungicide
        {
            get { return _fungicide; }
            set
            {
                _fungicide = Convert.ToDecimal(value.ToString("F2"));
            }
        }

        /// <summary>
        /// 杀虫剂
        /// </summary>
        private decimal _insecticide;

        /// <summary>
        /// 杀虫剂
        /// </summary>
        public decimal Insecticide
        {
            get { return _insecticide; }
            set { _insecticide = Convert.ToDecimal(value.ToString("F2")); }
        }

        /// <summary>
        /// 杀螨剂
        /// </summary>
        private decimal _acaricide;

        /// <summary>
        /// 杀螨剂
        /// </summary>
        public decimal Acaricide
        {
            get { return _acaricide; }
            set
            {
                _acaricide = Convert.ToDecimal(value.ToString("F2"));
            }
        }

        /// <summary>
        /// 植物生长调节剂
        /// </summary>
        private decimal _plantGrowthRegulator;

        /// <summary>
        /// 植物生长调节剂
        /// </summary>
        public decimal PlantGrowthRegulator
        {
            get { return _plantGrowthRegulator; }
            set { _plantGrowthRegulator = Convert.ToDecimal(value.ToString("F2")); }
        }

        /// <summary>
        /// 卫生杀虫剂
        /// </summary>
        private decimal _hygienicInsecticide;

        /// <summary>
        /// 卫生杀虫剂
        /// </summary>
        public decimal HygienicInsecticide
        {
            get { return _hygienicInsecticide; }
            set
            {
                _hygienicInsecticide = Convert.ToDecimal(value.ToString("F2"));
            }
        }

        /// <summary>
        /// 其它
        /// </summary>
        public decimal Other { get; set; }

        /// <summary>
        /// 合计
        /// </summary>
        private decimal _sum;

        /// <summary>
        /// 合计
        /// </summary>
        public decimal Sum
        {
            get { return _sum; }
            set { _sum = Convert.ToDecimal(value.ToString("F2")); }
        }
    }
}
