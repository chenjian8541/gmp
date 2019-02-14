using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Platform.Response;

namespace TTY.GMP.IBusiness
{
    /// <summary>
    /// O/B端平台业务
    /// </summary>
    public interface IPlatformBLL
    {
        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        PfUser GetUserInfo(long userId);

        /// <summary>
        /// 获取机构负责人信息
        /// </summary>
        /// <param name="orgIds"></param>
        /// <returns></returns>
        List<PfFullName> GetFullNames(IEnumerable<long> orgIds);

        /// <summary>
        /// 通过机构ID获取机构信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        PfOrganizationModel GetOrgInfo(long orgId);

        /// <summary>
        /// 获取机构推荐人信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        PfOrganizationRecommendModel GetOrgRecommend(long orgId);
    }
}
