
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
    public class SuplierService:BaseService<Suplier>,ISuplierService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private ISuplierRepository _suplierRepository;

        #endregion
        #region Constructor
        public SuplierService(ISuplierRepository suplierRepository) :base(suplierRepository)
        {
            //ServiceResult = new ServiceResult();
            
            _suplierRepository = suplierRepository;

        }
        #endregion
        #region Method
        #region
        /*public override ServiceResult Add(Suplier Suplier)
        {
           /* var SuplierIds = "";
            var lengthSupliers = Suplier.SuplierList.Count;
            if (lengthSupliers > 0)
            {

                for (int i = 0; i < lengthSupliers - 1; i++)
                {
                    SuplierIds += Suplier.SuplierList[i].SuplierId + ",";
                }
                SuplierIds += Suplier.SuplierList[lengthSupliers - 1].SuplierId;
                Suplier.SuplierIds = SuplierIds;
            }


            var subjectIds = "";
            var lengthSubjectList = Suplier.SubjectList.Count;
            if (lengthSubjectList > 0)
            {

                for (int i = 0; i < lengthSubjectList - 1; i++)
                {
                    subjectIds += Suplier.SubjectList[i].SubjectId + ",";
                }
                subjectIds += Suplier.SubjectList[lengthSubjectList - 1].SubjectId;
                Suplier.SubjectIds = subjectIds;
            }*//*
            var isValidate = base.Validate(Suplier);
            if(isValidate)
            {
                var row = _SuplierRepository.Add(Suplier);
                *//*if (row > 0)
                {
                    var SuplierSuplierService = new SuplierSuplierService(_SuplierSuplierRepository);
                    var entity = _SuplierRepository.GetSuplierByCreatedDate();
                    for (int i = 0; i < Suplier.SuplierList.Count; i++)
                    {
                        SuplierSuplier SuplierSuplier = new SuplierSuplier();
                        SuplierSuplier.SuplierId = entity.SuplierId;
                        SuplierSuplier.SuplierId = Suplier.SuplierList[i].SuplierId;
                        ServiceResult = SuplierSuplierService.Add(SuplierSuplier);
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
        public ServiceResult GetSuplierByPhoneNumber(string phoneNumber)
        {
            var suplier = _suplierRepository.GetSuplierByPhoneNumber(phoneNumber);
           
            ServiceResult.Data = suplier;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }


        #endregion
    }
}
