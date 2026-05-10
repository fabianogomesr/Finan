using Finan.Contracts.Request;
using Finan.Contracts.Response;
using Finan.Web.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Web.Clients
{
    public class GroupClient : BaseClient, IGroupClient
    {
        public GroupClient(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration)
        {
        }
        const string _baseUrl = "Group";
        public async Task<ApiResponse<List<GroupResponse>>> GetAllAsync() => await GetAsync<List<GroupResponse>>(_baseUrl);
        public async Task<ApiResponse<GroupResponse>> GetAsync(int id) => await GetAsync<GroupResponse>(_baseUrl + "/" + id);
        public async Task<ApiResponse<GroupResponse>> CreateAsync(GroupRequest Group) => await RequestAsync<GroupResponse>(_baseUrl, "", Group, RestSharp.Method.Post);
        public async Task<ApiResponse<GroupResponse>> UpdateAsync(GroupRequest Group) => await RequestAsync<GroupResponse>(_baseUrl, "", Group, RestSharp.Method.Put);
        public async Task DeleteAsync(int id) => await DeleteAsync(_baseUrl + "/" + id, "");
        public async Task<ApiResponse<PagedResponse<GroupResponse>?>> GetPageAsync(int pageNumber, int pagesize) => await GetAsync<PagedResponse<GroupResponse>>(_baseUrl + "/" + "Paged" + "/"+ pageNumber + "/" + pagesize);
        public async Task<ApiResponse<List<NatureResponse>?>> GetNatures() => await GetAsync<List<NatureResponse>>(_baseUrl + "/Natures");
        public Task<ApiResponse<List<GroupResponse>>> GetGroupsByNature(int typeId) => GetAsync<List<GroupResponse>>(_baseUrl + "/Nature/" + typeId);
    }
}
