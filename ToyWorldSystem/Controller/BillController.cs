using Contracts;
using Entities.ErrorModel;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/bills")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public BillController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Get bill detail in the chat (Role: Manager, Member)
        /// </summary>
        /// <param name="bill_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{bill_id}/details")]
        public async Task<IActionResult> GetBillDetail(int bill_id)
        {
            var detail = await _repository.Bill.GetBillDetail(bill_id, trackChanges: false);

            if (detail == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No bill matches with id send");

            return Ok(detail);
        }

        /// <summary>
        /// Create bill (Role: Manager, Member (Seller))
        /// </summary>
        /// <param name="newBill"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateBill(NewBillParameters newBill)
        {
            var current_userId = _userAccessor.getAccountId();

            var findTime = DateTime.Now;

            _repository.Bill.Create(new Entities.Models.Bill
            {
                ToyOfSellerName = newBill.ToyOfSellerName,
                ToyOfBuyerName = newBill.ToyOfBuyerName,
                IsExchangeByMoney = newBill.IsExchangeByMoney,
                ExchangeValue = newBill.ExchangeValue,
                SellerId = current_userId,
                BuyerId = newBill.BuyerId,
                TradingPostId = newBill.TradingPostId,
                Status = 0,
                CreateTime = findTime
            });

            await _repository.SaveAsync();
            var bill_id = _repository.Bill.GetIdOfCreatedBill(findTime, trackChanges: false);

            return Ok(new {BillId = bill_id});
        }

        /// <summary>
        /// Accept bill (Role: Manager, Member (Buyer))
        /// </summary>
        /// <param name="bill_id"></param>
        /// <param name="accept_or_deny">0: deny, 1: accept</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{bill_id}/accept_or_deny")]
        public async Task<IActionResult> AcceptOrDenyBill(int bill_id, int accept_or_deny)
        {
            var current_user_id = _userAccessor.getAccountId();

            var bill = await _repository.Bill.GetAcceptOrDeny(bill_id, trackChanges: false);

            if (bill == null)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid bill");

            if (bill.BuyerId != current_user_id)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Not buyer to accept");

            if (bill.Status != 0)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "This bill is already accepted");

            if (accept_or_deny == 0)
            {
                _repository.Bill.DenyBill(bill);
            }
            else
            {
                _repository.Bill.AcceptBill(bill);
                await _repository.TradingPost.UpdateTradingStatus(bill.TradingPostId, trackChanges: false);
            }
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }

        //[HttpPut]
        //[Route("{bill_id}/status")]
        //public async Task<IActionResult> UpdateBillStatus(int bill_id, int update_status)
        //{
        //    var bill = _repository.Bill.GetAcceptOrDeny(bill_id);
        //    switch (update_status)
        //    {
        //        case 2:
        //            {
        //                await _repository.Bill.ClosedBill();
        //                break;
        //            }
        //    }
        //}
    }
}
