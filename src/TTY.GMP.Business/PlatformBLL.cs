using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Platform.Response;
using TTY.GMP.IBusiness;
using TTY.GMP.Utility;

namespace TTY.GMP.Business
{
    /// <summary>
    /// O/B端平台业务
    /// </summary>
    public class PlatformBLL : IPlatformBLL
    {
        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public PfUser GetUserInfo(long userId)
        {
            var parms = new Dictionary<string, string>();
            parms.Add("user_id", userId.ToString());
            parms.Add("id", userId.ToString());
            var result = PlatformHelper.RequestApi<PfResponse<PfUser>>(PlatformApiConfig.GetUserInfo, parms);
            if (result == null || result.data == null || result.data.info == null)
            {
                return null;
            }
            return result.data.info;
        }

        /// <summary>
        /// 获取机构负责人信息
        /// </summary>
        /// <param name="orgIds"></param>
        /// <returns></returns>
        public List<PfFullName> GetFullNames(IEnumerable<long> orgIds)
        {
            var parms = new Dictionary<string, string>();
            parms.Add("org_ids", string.Join(",", orgIds));
            var result = PlatformHelper.RequestApi<PfResponse<List<PfFullName>>>(PlatformApiConfig.GetFullNames, parms);
            if (result == null || result.data == null)
            {
                return null;
            }
            return result.data.info;
        }

        /// <summary>
        /// 通过机构ID获取机构信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public PfOrganizationModel GetOrgInfo(long orgId)
        {
            var parms = new Dictionary<string, string>();
            parms.Add("org_id", orgId.ToString());
            var result = PlatformHelper.RequestApi<PfResponse<PfOrganizationModel>>(PlatformApiConfig.GetOrgInfo, parms);
            if (result == null || result.data == null)
            {
                return null;
            }
            return result.data.info;
        }

        /// <summary>
        /// 获取机构推荐人信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public PfOrganizationRecommendModel GetOrgRecommend(long orgId)
        {
            var parms = new Dictionary<string, string>();
            parms.Add("org_id", orgId.ToString());
            var result = PlatformHelper.RequestApi<PfResponse<PfOrganizationRecommendModel>>(PlatformApiConfig.GetOrgRecommend, parms);
            if (result == null || result.data == null)
            {
                return null;
            }
            return result.data.info;
        }
    }
}
