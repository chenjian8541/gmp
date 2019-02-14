using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 地区数据访问
    /// </summary>
    public interface IAreaDAL
    {
        /// <summary>
        /// 获取所有省、市、区信息
        /// </summary>
        /// <returns></returns>
        Task<List<Area>> GetArea();

        /// <summary>
        /// 获取所有省份信息
        /// </summary>
        /// <returns></returns>
        Task<List<Area>> GetProvince();

        /// <summary>
        /// 获取省下面所有市信息
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        Task<List<Area>> GetCity(long provinceId);

        /// <summary>
        /// 获取市下面所有区信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        Task<List<Area>> GetDistrict(long cityId);

        /// <summary>
        /// 获取街道、乡镇
        /// </summary>
        /// <param name="district"></param>
        /// <returns></returns>
        Task<List<Area>> GetStreet(long district);

        /// <summary>
        /// 获取地区
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        Task<Area> GetArea(long areaId);

        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <param name="areaIds"></param>
        /// <returns></returns>
        Task<List<Area>> GetArea(List<long> areaIds);
    }
}
