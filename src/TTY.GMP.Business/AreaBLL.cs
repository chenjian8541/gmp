using System.Collections.Generic;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 地区业务访问
    /// </summary>
    public class AreaBLL : IAreaBLL
    {
        /// <summary>
        /// 地区数据访问
        /// </summary>
        private readonly IAreaDAL _areaDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="areaDal"></param>
        public AreaBLL(IAreaDAL areaDal)
        {
            this._areaDal = areaDal;
        }

        /// <summary>
        /// 获取所有省、市、区信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<Area>> GetArea()
        {
            return await _areaDal.GetArea();
        }

        /// <summary>
        /// 获取所有省份信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<Area>> GetProvince()
        {
            return await _areaDal.GetProvince();
        }

        /// <summary>
        /// 获取省下面所有市信息
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        public async Task<List<Area>> GetCity(long provinceId)
        {
            return await _areaDal.GetCity(provinceId);
        }

        /// <summary>
        /// 获取市下面所有区信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public async Task<List<Area>> GetDistrict(long cityId)
        {
            return await _areaDal.GetDistrict(cityId);
        }

        /// <summary>
        /// 获取街道、乡镇
        /// </summary>
        /// <param name="district"></param>
        /// <returns></returns>
        public async Task<List<Area>> GetStreet(long district)
        {
            return await _areaDal.GetStreet(district);
        }

        /// <summary>
        /// 获取地区
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public async Task<Area> GetArea(long areaId)
        {
            return await this._areaDal.GetArea(areaId);
        }

        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <param name="areaIds"></param>
        /// <returns></returns>
        public async Task<List<Area>> GetArea(List<long> areaIds)
        {
            if (areaIds == null || areaIds.Count == 0)
            {
                return new List<Area>();
            }
            return await this._areaDal.GetArea(areaIds);
        }
    }
}