
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
using Core.Enum;

namespace Core.Services
{
    public class OrderService:BaseService<Order>,IOrderService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private IOrderRepository _orderRepository;
        private IOrderDetailService _orderDetailService;
        private INotifyRepository _notifyRepository;
        private ICustomerRepository _customerRepository;
        private IMailService _mailService;

        #endregion
        #region Constructor
        public OrderService(IOrderRepository orderRepository, IOrderDetailService orderDetailService, INotifyRepository notifyRepository, IMailService mailService, ICustomerRepository customerRepository) :base(orderRepository)
        {
            //ServiceResult = new ServiceResult();
            
            _orderRepository = orderRepository;
            _orderDetailService = orderDetailService;
            _notifyRepository = notifyRepository;
            _mailService = mailService;
            _customerRepository = customerRepository;

        }
        #endregion
        #region Method
        #region
        /*public override ServiceResult Add(Order Order)
        {
           /* var OrderIds = "";
            var lengthOrders = Order.OrderList.Count;
            if (lengthOrders > 0)
            {

                for (int i = 0; i < lengthOrders - 1; i++)
                {
                    OrderIds += Order.OrderList[i].OrderId + ",";
                }
                OrderIds += Order.OrderList[lengthOrders - 1].OrderId;
                Order.OrderIds = OrderIds;
            }


            var subjectIds = "";
            var lengthSubjectList = Order.SubjectList.Count;
            if (lengthSubjectList > 0)
            {

                for (int i = 0; i < lengthSubjectList - 1; i++)
                {
                    subjectIds += Order.SubjectList[i].SubjectId + ",";
                }
                subjectIds += Order.SubjectList[lengthSubjectList - 1].SubjectId;
                Order.SubjectIds = subjectIds;
            }*//*
            var isValidate = base.Validate(Order);
            if(isValidate)
            {
                var row = _OrderRepository.Add(Order);
                *//*if (row > 0)
                {
                    var OrderOrderService = new OrderOrderService(_OrderOrderRepository);
                    var entity = _OrderRepository.GetOrderByCreatedDate();
                    for (int i = 0; i < Order.OrderList.Count; i++)
                    {
                        OrderOrder OrderOrder = new OrderOrder();
                        OrderOrder.OrderId = entity.OrderId;
                        OrderOrder.OrderId = Order.OrderList[i].OrderId;
                        ServiceResult = OrderOrderService.Add(OrderOrder);
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
        public ServiceResult GetOrderByPhoneNumber(string phoneNumber)
        {
            var Order = _orderRepository.GetOrderByPhoneNumber(phoneNumber);

            ServiceResult.Data = Order;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult CheckCustomerPurchasedProduct(string phoneNumber,Guid productAttributeID)
        {
            var resultCheck = _orderRepository.CheckCustomerPurchasedProduct(phoneNumber, productAttributeID);

            ServiceResult.Data = resultCheck;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }

        public ServiceResult Add(OrderInfo orderInfo)
        {
            
            var orderID = _orderRepository.Add(orderInfo.order);
            if (orderID != Guid.Empty)
            {

                ServiceResult = _orderDetailService.InsertOrderDetails(orderInfo.orderDetails, orderID);
                Entities.Notify notify = new Entities.Notify();
                notify.CustomerID = orderInfo.order.CustomerID;
                notify.OrderID = orderID;
                notify.Type = Enum.Notify.Order;
                var notifyResult = _notifyRepository.Add(notify);
                var customer=_customerRepository.GetById(orderInfo.order.CustomerID);

                if (customer.Email != "")
                {

                    MailRequest mailRequest = new MailRequest();
                mailRequest.Subject = "Đặt hàng thành công";
                mailRequest.ToEmail = customer.Email;
                mailRequest.Body = "<html><body>TTL Store kính chào quý khách hàng " + customer.CustomerName + ",</br> Đơn hàng có mã " + orderID + " có tổng giá là " + orderInfo.order.TotalPrice + " VND của quý khách đã đặt hàng thành công.</br> Cảm ơn quý khách đã tin tưởng và ủng hộ cửa hàng!</br> Chúc quý khách có một ngày tuyệt vời.</br> TTL Store</body></html>";
                _mailService.SendEmailAsync(mailRequest);
                }
            }

            
            return ServiceResult;
        }
        public override ServiceResult Update(Order order, Guid id)
        {
            order.EntityState = Enum.EntityState.Update;
            var isValidate = Validate(order);

            if (isValidate)
            {
                var orderOld= _orderRepository.GetById(id);
                ServiceResult.Data = _orderRepository.Update(order, id);
                if ((int)ServiceResult.Data > 0)
                {
                    ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
                    ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
                    var customer = _customerRepository.GetById(order.CustomerID);

                    if (customer.Email != "")
                    {

                        MailRequest mailRequest = new MailRequest();
                        mailRequest.Subject = "Cập nhật đơn hàng";
                        mailRequest.ToEmail = customer.Email;
                        mailRequest.Body = "";
                        if (orderOld.PaymentStatus!= order.PaymentStatus)
                        {
                            string paymentStatus = order.PaymentStatus ? "Đã thanh toán" : "Chưa thanh toán";

                            mailRequest.Body = "TTL Store kính chào quý khách hàng " + customer.CustomerName + 
                                ",</br> Đơn hàng có mã " + order.OrderID + " có tổng giá là " + order.TotalPrice + 
                                " VND của quý khách đã được cập nhật trạng thái thanh toán."+
                                "</br>Trạng thái thanh toán đơn hàng hiện tại của quý khách là."+ paymentStatus +
                                "</br> Cảm ơn quý khách đã tin tưởng và ủng hộ cửa hàng!"+
                                "</br> Chúc quý khách có một ngày tuyệt vời.</br> TTL Store";
                        }else if(orderOld.OrderStatus != order.OrderStatus)
                        {
                            string orderStatus = "";
                            switch (order.OrderStatus)
                            {
                                case (int)OrderStatus.WaitConfirm:
                                    orderStatus = "Chờ xác nhận";
                                    break;
                                case (int)OrderStatus.WaitGoods:
                                    orderStatus = "Chờ lấy hàng";
                                    break;
                                case (int)OrderStatus.Delivering:
                                    orderStatus = "Đang giao hàng";
                                    break;
                                case (int)OrderStatus.Delivered:
                                    orderStatus = "Đã giao hàng";
                                    break;
                                case (int)OrderStatus.Cancelled:
                                    orderStatus = "Đã hủy";
                                    break;
                            }
                            
                            mailRequest.Body = "TTL Store kính chào quý khách hàng " + customer.CustomerName +
                                ",</br> Đơn hàng có mã " + order.OrderID + " có tổng giá là " + order.TotalPrice +
                                " VND của quý khách đã được cập nhật trạng thái đơn hàng." +
                                "</br>Trạng thái đơn hàng hiện tại của quý khách là." + orderStatus +
                                "</br> Cảm ơn quý khách đã tin tưởng và ủng hộ cửa hàng!" +
                                "</br> Chúc quý khách có một ngày tuyệt vời.</br> TTL Store";
                        }
                        _mailService.SendEmailAsync(mailRequest);
                    }
                }
                else
                {
                    ServiceResult.MISACode = Core.Const.Const.MISACodeInValid;
                    ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_NotValid);
                }
            }
            else
            {
                return ServiceResult;
            }
            return ServiceResult;
        }
        public ServiceResult PurchasedHistory(string phoneNumber)
        {
            List<OrderCart> orderInfos = new List<OrderCart>();
            var orders = _orderRepository.PurchasedHistory(phoneNumber);
            foreach (var order in orders)
            {
                var orderDetails = _orderDetailService.PurchasedHistoryByOrderID(order.OrderID);
                orderInfos.Add(new OrderCart { Order = order, OrderDetails = (List<OrderDetailProductAttribute>)orderDetails.Data });
            }
            ServiceResult.Data = orderInfos;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult FollowOrder(string phoneNumber, int status)
        {
            List<OrderCart> orderInfos = new List<OrderCart>();
            var orders = _orderRepository.FollowOrder(phoneNumber, status);
            foreach (var order in orders)
            {
                var orderDetails = _orderDetailService.PurchasedHistoryByOrderID(order.OrderID);
                orderInfos.Add(new OrderCart { Order = order, OrderDetails = (List<OrderDetailProductAttribute>)orderDetails.Data });
            }
            ServiceResult.Data = orderInfos;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetAllPaging(int pageIndex, int pageSize, string? queryText, DateTime? startTime, DateTime? endTime, int status)
        {
            var orders = _orderRepository.GetAllPaging(pageIndex, pageSize, queryText, startTime, endTime, status);
            
            ServiceResult.Data = orders;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetCustomerOrder(Guid orderID)
        {
            var orderCustomer = _orderRepository.GetCustomerOrder(orderID);
            
            ServiceResult.Data = orderCustomer;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult UpdateIMEIByOrderID(string imeis, Guid orderDetailID)
        {
            var orderCustomer = _orderDetailService.UpdateIMEIByOrderID(imeis, orderDetailID);
            
            ServiceResult.Data = orderCustomer;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }

        #endregion
    }
}
