using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.CacheBucket;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Enum;
using TTY.GMP.ICache;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 地区数据访问
    /// </summary>
    public class AreaDAL : BaseCacheDAL<AreaBucket>, IAreaDAL, IDbContext<ErpDbContext>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheProvider"></param>
        public AreaDAL(ICacheProvider cacheProvider) : base(cacheProvider)
        { }

        /// <summary>
        /// 获取数据访问上下文
        /// </summary>
        /// <returns></returns>
        public ErpDbContext GetDbContext()
        {
            return new ErpDbContext();
        }

        /// <summary>
        /// 从数据库中获取省、市、区信息
        /// 未删除、状态正常的数据
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected override async Task<AreaBucket> GetDb(params object[] keys)
        {
            var areas = await this.FindList<ErpDbContext, Area>(p =>
                 (p.Level == AreaLevelEnum.Province ||
                 p.Level == AreaLevelEnum.City ||
                 p.Level == AreaLevelEnum.District) &&
                 p.IsDeleted.Equals(0) &&
                 p.Status.Equals(1));
            return new AreaBucket() { Areas = areas };
        }

        /// <summary>
        /// 获取所有省、市、区信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<Area>> GetArea()
        {
            var areaBuckets = await GetCache();
            if (areaBuckets != null)
            {
                return areaBuckets.Areas;
            }
            return new List<Area>();
        }

        /// <summary>
        /// 获取所有省份信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<Area>> GetProvince()
        {
            var area = await GetArea();
            return area.Where(p => p.Level == AreaLevelEnum.Province).ToList();
        }

        /// <summary>
        /// 获取省下面所有市信息
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        public async Task<List<Area>> GetCity(long provinceId)
        {
            var area = await GetArea();
            return area.Where(p => p.Level == AreaLevelEnum.City && p.ParentId == provinceId).ToList();
        }

        /// <summary>
        /// 获取市下面所有区信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public async Task<List<Area>> GetDistrict(long cityId)
        {
            var area = await GetArea();
            return area.Where(p => p.Level == AreaLevelEnum.District && p.ParentId == cityId).ToList();
        }

        /// <summary>
        /// 获取街道、乡镇
        /// </summary>
        /// <param name="district"></param>
        /// <returns></returns>
        public async Task<List<Area>> GetStreet(long district)
        {
            return await this.FindList<ErpDbContext, Area>(p => p.Level == AreaLevelEnum.Street && p.ParentId == district);
        }

        /// <summary>
        /// 获取地区
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public async Task<Area> GetArea(long areaId)
        {
            return await this.Find<ErpDbContext, Area>(areaId);
        }

        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <param name="areaIds"></param>
        /// <returns></returns>
        public async Task<List<Area>> GetArea(List<long> areaIds)
        {
            return await this.FindList<ErpDbContext, Area>(p => areaIds.Exists(j => j == p.AreaId));
        }
    }
}