using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http.Cors;

namespace WebApi.Providers
{
    public class CorsPolicyProvider : ICorsPolicyProvider
    {
        /// <summary>
        /// Список сайтов для cors
        /// </summary>
        private List<string> siteForCors;

        /// <summary>
        /// Получает CorsPolicy с заполненными Origins
        /// </summary>
        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };

            if (siteForCors == null)
            {
                siteForCors = new List<string>();
            }
            if (siteForCors != null && !siteForCors.Any())
            {
                siteForCors = new List<string> { "http://localhost:58331" };
            }
            if (siteForCors != null)
            {
                foreach (var site in siteForCors)
                {
                    if (string.IsNullOrEmpty(site))
                    {
                        continue;
                    }
                    policy.Origins.Add(site);
                }
            }

            return Task.FromResult(policy);
        }
    }
}