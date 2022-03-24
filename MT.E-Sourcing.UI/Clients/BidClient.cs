using MT.E_Sourcing.UI.ViewModel;
using MT.E_Sourcing.WebApp.Core.Common;
using MT.E_Sourcing.WebApp.Infrastructure.ResultModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MT.E_Sourcing.UI.Clients
{
    public class BidClient
    {
        public HttpClient _client { get; }

        public BidClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(CommonInfo.BaseAddress);
        }

        public async Task<Result<List<BidViewModel>>> GetAllBidsByAuctionId(string id)
        {
            var response = await _client.GetAsync("/Bid/GetBidByAuctionId?id=" + id);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<BidViewModel>>(responseData);

                if (result != null)
                {
                    return new Result<List<BidViewModel>>(true, ResultConstant.RecordFound, result);
                }
                return new Result<List<BidViewModel>>(false, ResultConstant.RecordNotFound);
            }
            return new Result<List<BidViewModel>>(false, ResultConstant.RecordNotFound);
        }

        public async Task<Result<string>> SendBid(BidViewModel model)
        {
            var dataAsString = JsonConvert.SerializeObject(model);

            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("/Bid", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
               

                
                    return new Result<string> (true, ResultConstant.RecordCreateSuccesfully, responseData);
               
            }
            return new Result<string>(true, ResultConstant.RecordCreateNotSuccesfully);
        }

   
    }
}
