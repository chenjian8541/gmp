using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Government.GuangDong.Request
{
    /// <summary>
    /// 经销商数据上传 请求参数
    /// </summary>
    public class GdAddShopRequest
    {
        /// <summary>
        ///经营店名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 经营许可证号
        /// </summary>
        public string jyxkz { get; set; }
        /// <summary>
        /// 发证日期
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 有效期至
        /// </summary>
        public string enddate { get; set; }
        /// <summary>
        /// 许可证图片(base64位编码)
        /// </summary>
        public string jyxkz_pic { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string headman { get; set; }
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string headphone { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string linkman { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string linkphone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 邮寄地址
        /// </summary>
        public string yjdz { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string postcode { get; set; }
        /// <summary>
        /// 是否试点(0:否;1:是)
        /// </summary>
        public int issd { get; set; }
        /// <summary>
        /// 店铺面积
        /// </summary>
        public string dpmj { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        public string jyfw { get; set; }
        /// <summary>
        /// 是否限用农药经销商(0:否;1:是)
        /// </summary>
        public int isxzshop { get; set; }
        /// <summary>
        /// 发证单位
        /// </summary>
        public string fzdw { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 街
        /// </summary>
        public string town { get; set; }
        /// <summary>
        /// 省代码
        /// </summary>
        public string provincecode { get; set; }
        /// <summary>
        /// 市代码
        /// </summary>
        public string citycode { get; set; }
        /// <summary>
        /// 区代码
        /// </summary>
        public string countrycode { get; set; }
        /// <summary>
        /// 街代码
        /// </summary>
        public string towncode { get; set; }
        /// <summary>
        /// 仓库面积
        /// </summary>
        public string ckmj { get; set; }
        /// <summary>
        /// 技术人员
        /// </summary>
        public string jsry { get; set; }
        /// <summary>
        /// 技术人员电话
        /// </summary>
        public string jsryphone { get; set; }
    }
}
