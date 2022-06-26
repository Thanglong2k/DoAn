
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Core.Const;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Service;
using static Core.Entities.BaseEntity;

namespace Core.Services
{
    public class OrderDetailService:BaseService<OrderDetail>,IOrderDetailService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private IOrderDetailRepository _orderDetailRepository;

        #endregion
        #region Constructor
        public OrderDetailService(IOrderDetailRepository orderDetailRepository) :base(orderDetailRepository)
        {
            //ServiceResult = new ServiceResult();

            _orderDetailRepository = orderDetailRepository;

        }
        #endregion
        #region Method
        #region
        /*public override ServiceResult Add(OrderDetail OrderDetail)
        {
           /* var OrderDetailIds = "";
            var lengthOrderDetails = OrderDetail.OrderDetailList.Count;
            if (lengthOrderDetails > 0)
            {

                for (int i = 0; i < lengthOrderDetails - 1; i++)
                {
                    OrderDetailIds += OrderDetail.OrderDetailList[i].OrderDetailId + ",";
                }
                OrderDetailIds += OrderDetail.OrderDetailList[lengthOrderDetails - 1].OrderDetailId;
                OrderDetail.OrderDetailIds = OrderDetailIds;
            }


            var subjectIds = "";
            var lengthSubjectList = OrderDetail.SubjectList.Count;
            if (lengthSubjectList > 0)
            {

                for (int i = 0; i < lengthSubjectList - 1; i++)
                {
                    subjectIds += OrderDetail.SubjectList[i].SubjectId + ",";
                }
                subjectIds += OrderDetail.SubjectList[lengthSubjectList - 1].SubjectId;
                OrderDetail.SubjectIds = subjectIds;
            }*//*
            var isValidate = base.Validate(OrderDetail);
            if(isValidate)
            {
                var row = _OrderDetailRepository.Add(OrderDetail);
                *//*if (row > 0)
                {
                    var OrderDetailOrderDetailService = new OrderDetailOrderDetailService(_OrderDetailOrderDetailRepository);
                    var entity = _OrderDetailRepository.GetOrderDetailByCreatedDate();
                    for (int i = 0; i < OrderDetail.OrderDetailList.Count; i++)
                    {
                        OrderDetailOrderDetail OrderDetailOrderDetail = new OrderDetailOrderDetail();
                        OrderDetailOrderDetail.OrderDetailId = entity.OrderDetailId;
                        OrderDetailOrderDetail.OrderDetailId = OrderDetail.OrderDetailList[i].OrderDetailId;
                        ServiceResult = OrderDetailOrderDetailService.Add(OrderDetailOrderDetail);
                    }
                }*//*

                ServiceResult.Data = row;
                ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
                ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            }
            else
            {
                return ServiceResult;
            }
            
            return ServiceResult;

        }*/
        #endregion
        public ServiceResult InsertOrderDetails(List<OrderDetail> orderDetails,Guid orderID)
        {
            var result = _orderDetailRepository.InsertOrderDetails(orderDetails, orderID);
            bool data = true;
            if(result!= orderDetails.Count)
            {
                data = false;
            }
            ServiceResult.Data = data;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult CheckCustomerPurchasedProduct(string phoneNumber,Guid productAttributeID)
        {
            var resultCheck = _orderDetailRepository.CheckCustomerPurchasedProduct(phoneNumber, productAttributeID);

            ServiceResult.Data = resultCheck;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult PurchasedHistoryByOrderID(Guid? orderID)
        {
            var resultCheck = _orderDetailRepository.PurchasedHistoryByOrderID(orderID);

            ServiceResult.Data = resultCheck;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetOrderDetailByOrderID(Guid orderID, int pageIndex, int pageSize)
        {
            var resultCheck = _orderDetailRepository.GetOrderDetailByOrderID(orderID,pageIndex,pageSize);

            ServiceResult.Data = resultCheck;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult UpdateIMEIByOrderID(string imeis, Guid orderDetailID)
        {
            var result = _orderDetailRepository.UpdateIMEIByOrderID(imeis, orderDetailID);

            ServiceResult.Data = result;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }



        #endregion
    }
}
