using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using G03Trinh_eQLBHDabiLocal.Common;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;
using Newtonsoft.Json;
using Ninject.Activation;
using RestSharp;
using G03Trinh_eQLBHDabiLocal.Models.QR_Code;
using System.Text;
using System.Net;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace G03Trinh_eQLBHDabiLocal.Services
{
    public class BankService
    {
        private EFDbContext context;

        public BankService(EFDbContext context) {
            this.context = context;
        }

        public Bank getBank()
        {
            bank entity = context.bank.FirstOrDefault();
            if(entity == null)
                return null;
            return new Bank()
            {
                id = entity.id,
                name = entity.name,
                content = entity.content,
                idBank = entity.idBank,
                number = entity.number
            };
        }

        public void setBank(Bank model)
        {
            bank entity = context.bank.FirstOrDefault();
            entity.idBank = model.idBank;
            entity.number = model.number;
            entity.name = model.name;
            entity.content = model.content;
            context.bank.AddOrUpdate(entity);
            context.SaveChanges();
        }

        public ApiResponse createQR(Bill bill)
        {
            bank entity = context.bank.FirstOrDefault();
            var apiRequest = new ApiRequest();
            apiRequest.acqId = Convert.ToInt32(entity.idBank);
            apiRequest.accountNo = entity.number.ToString();
            apiRequest.accountName = entity.name;
            product product = context.product.Where(p => p.id == bill.product).FirstOrDefault();
            apiRequest.amount = (int)(product.price * bill.quantity);
            apiRequest.addInfo = context.bank.FirstOrDefault().content;
            var jsonRequest = JsonConvert.SerializeObject(apiRequest);
            // use restsharp for request api.
            var client = new RestClient("https://api.vietqr.io/v2/generate");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Accept", "application/json");

            request.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);

            var response = client.Execute(request);
            string content = response.Content;
            ApiResponse dataResult = JsonConvert.DeserializeObject<ApiResponse>(content);
            return dataResult;
        }

        public ApiResponse createQR(String username)
        {
            List<cart> carts = context.cart.Where(c => c.customer1.username == username).ToList();
            int amount = 0;
            foreach (cart i in carts)
            {
                amount += (int)(i.quantity * i.product1.price);
                context.cart.Remove(i);
            }
            context.SaveChanges();
            bank entity = context.bank.FirstOrDefault();
            var apiRequest = new ApiRequest();
            apiRequest.acqId = Convert.ToInt32(entity.idBank);
            apiRequest.accountNo = entity.number.ToString();
            apiRequest.accountName = entity.name;
            apiRequest.addInfo = context.bank.FirstOrDefault().content;
            apiRequest.amount = amount;
            var jsonRequest = JsonConvert.SerializeObject(apiRequest);
            // use restsharp for request api.
            var client = new RestClient("https://api.vietqr.io/v2/generate");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Accept", "application/json");

            request.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);

            var response = client.Execute(request);
            string content = response.Content;
            ApiResponse dataResult = JsonConvert.DeserializeObject<ApiResponse>(content);
            return dataResult;
        }

        public List<System.Web.Mvc.SelectListItem> listBank()
        {
            List<System.Web.Mvc.SelectListItem> selectListItems = new List<System.Web.Mvc.SelectListItem>();
            using (WebClient client = new WebClient())
            {
                var htmlData = client.DownloadData("https://api.vietqr.io/v2/banks");
                var bankRawJson = Encoding.UTF8.GetString(htmlData);
                var listBankData = JsonConvert.DeserializeObject<DataBank>(bankRawJson);
                foreach(Datum item in listBankData.data)
                {
                    selectListItems.Add(new System.Web.Mvc.SelectListItem() { Text = item.shortName, Value = item.bin });
                }
            }
            return selectListItems;
        }
    }
}