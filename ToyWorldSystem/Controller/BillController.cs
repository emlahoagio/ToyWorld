﻿using Contracts;
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

            var bill = await _repository.Bill.GetBillById(bill_id, trackChanges: false);

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
                await _repository.TradingPost.UpdateTradingStatus(bill.TradingPostId, 1, trackChanges: false);
            }
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// Update bill to CANCEL or CLOSED (Role: Manager, member => Only seller can update)
        /// </summary>
        /// <param name="bill_id"></param>
        /// <param name="update_status">2: Close, 3: Cancel (2 and 3 ONLY)</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{bill_id}/status")]
        public async Task<IActionResult> UpdateBillStatus(int bill_id, int update_status)
        {
            var bill = await _repository.Bill.GetBillById(bill_id, trackChanges: false);

            if(bill.SellerId != _userAccessor.getAccountId())
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to change");
            }

            switch (update_status)
            {
                case 2:
                    {
                        _repository.Bill.UpdateBillStatus(bill, update_status);
                        await _repository.TradingPost.UpdateTradingStatus(bill.TradingPostId, 2, trackChanges: false);
                        break;
                    };
                case 3:
                    {
                        _repository.Bill.UpdateBillStatus(bill, update_status);
                        await _repository.TradingPost.UpdateTradingStatus(bill.TradingPostId, 0, trackChanges: false);
                        break;
                    }
                default:
                    {
                        throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid update status of bill");
                    }
            }
            await _repository.SaveAsync();
            return Ok("Save changes success");      
        }
    }
}
