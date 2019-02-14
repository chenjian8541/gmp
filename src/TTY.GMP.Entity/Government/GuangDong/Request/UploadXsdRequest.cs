using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Government.GuangDong.Request
{
    /// <summary>
    /// 销售记录上传参数
    /// </summary>
    public class UploadXsdRequest
    {
        /// <summary>
        /// 销售单号
        /// </summary>
        public string no { get; set; }
        /// <summary>
        /// 销售单明细
        /// </summary>
        public string detailno { get; set; }
        /// <summary>
        /// 农药产品名称
        /// </summary>
        public string product_info_name { get; set; }
        /// <summary>
        /// 农药产品id
        /// </summary>
        public string product_info_id { get; set; }
        /// <summary>
        /// 农药产品代码
        /// </summary>
        public string product_info_code { get; set; }
        /// <summary>
        /// 农药剂型
        /// </summary>
        public string nyjx { get; set; }
        /// <summary>
        /// 农药毒性
        /// </summary>
        public string nydx { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public string scrq { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public string yxq { get; set; }
        /// <summary>
        /// 农药批准文
        /// </summary>
        public string pzwh { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string specification { get; set; }
        /// <summary>
        /// 追溯码
        /// </summary>
        public string zsm { get; set; }
        /// <summary>
        /// 购买客户单位地址
        /// </summary>
        public string toaddress { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string topost { get; set; }
        /// <summary>
        /// 购买单位
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string toheadmen { get; set; }
        /// <summary>
        /// 购买人身份证
        /// </summary>
        public string toidcard { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string toprovince { get; set; }
        /// <summary>
        /// 购买人电话
        /// </summary>
        public string tophone { get; set; }
        /// <summary>
        /// 生产企业编码
        /// </summary>
        public string gldx_company_code { get; set; }
        /// <summary>
        /// 生产企业名称
        /// </summary>
        public string gldx_company_name { get; set; }
        /// <summary>
        /// 登记证
        /// </summary>
        public string djh { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string batchno { get; set; }
        /// <summary>
        /// 销售时间
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 销售数量
        /// </summary>
        public string total { get; set; }
        /// <summary>
        /// 销售单价
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 销售金额
        /// </summary>
        public string totalprice { get; set; }
        /// <summary>
        /// 销售数量单位
        /// </summary>
        public string totalunit { get; set; }
        /// <summary>
        /// 是否限制农药经销商
        /// </summary>
        public int isxzshop { get; set; }
        /// <summary>
        /// 是否限制农药
        /// </summary>
        public int isxz { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string info { get; set; }
    }
}
