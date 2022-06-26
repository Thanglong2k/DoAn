
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
    public class CustomerService:BaseService<Customer>,ICustomerService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private ICustomerRepository _customerRepository;

        #endregion
        #region Constructor
        public CustomerService(ICustomerRepository customerRepository) :base(customerRepository)
        {
            //ServiceResult = new ServiceResult();
            
            _customerRepository = customerRepository;

        }
        #endregion
        #region Method
        #region
        /*public override ServiceResult Add(Customer Customer)
        {
           /* var CustomerIds = "";
            var lengthCustomers = Customer.CustomerList.Count;
            if (lengthCustomers > 0)
            {

                for (int i = 0; i < lengthCustomers - 1; i++)
                {
                    CustomerIds += Customer.CustomerList[i].CustomerId + ",";
                }
                CustomerIds += Customer.CustomerList[lengthCustomers - 1].CustomerId;
                Customer.CustomerIds = CustomerIds;
            }


            var subjectIds = "";
            var lengthSubjectList = Customer.SubjectList.Count;
            if (lengthSubjectList > 0)
            {

                for (int i = 0; i < lengthSubjectList - 1; i++)
                {
                    subjectIds += Customer.SubjectList[i].SubjectId + ",";
                }
                subjectIds += Customer.SubjectList[lengthSubjectList - 1].SubjectId;
                Customer.SubjectIds = subjectIds;
            }*//*
            var isValidate = base.Validate(Customer);
            if(isValidate)
            {
                var row = _CustomerRepository.Add(Customer);
                *//*if (row > 0)
                {
                    var CustomerCustomerService = new CustomerCustomerService(_CustomerCustomerRepository);
                    var entity = _CustomerRepository.GetCustomerByCreatedDate();
                    for (int i = 0; i < Customer.CustomerList.Count; i++)
                    {
                        CustomerCustomer CustomerCustomer = new CustomerCustomer();
                        CustomerCustomer.CustomerId = entity.CustomerId;
                        CustomerCustomer.CustomerId = Customer.CustomerList[i].CustomerId;
                        ServiceResult = CustomerCustomerService.Add(CustomerCustomer);
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
        public ServiceResult GetCustomerByPhoneNumber(string phoneNumber)
        {
            var customer = _customerRepository.GetCustomerByPhoneNumber(phoneNumber);
           
            ServiceResult.Data = customer;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }


        #endregion
    }
}
