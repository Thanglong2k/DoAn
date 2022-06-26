
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
    public class ManufacturerService:BaseService<Manufacturer>,IManufacturerService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private IManufacturerRepository _manufacturerRepository;

        #endregion
        #region Constructor
        public ManufacturerService(IManufacturerRepository manufacturerRepository) :base(manufacturerRepository)
        {
            //ServiceResult = new ServiceResult();
            
            _manufacturerRepository = manufacturerRepository;

        }
        #endregion
        #region Method
        #region
        /*public override ServiceResult Add(Manufacturer Manufacturer)
        {
           /* var ManufacturerIds = "";
            var lengthManufacturers = Manufacturer.ManufacturerList.Count;
            if (lengthManufacturers > 0)
            {

                for (int i = 0; i < lengthManufacturers - 1; i++)
                {
                    ManufacturerIds += Manufacturer.ManufacturerList[i].ManufacturerId + ",";
                }
                ManufacturerIds += Manufacturer.ManufacturerList[lengthManufacturers - 1].ManufacturerId;
                Manufacturer.ManufacturerIds = ManufacturerIds;
            }


            var subjectIds = "";
            var lengthSubjectList = Manufacturer.SubjectList.Count;
            if (lengthSubjectList > 0)
            {

                for (int i = 0; i < lengthSubjectList - 1; i++)
                {
                    subjectIds += Manufacturer.SubjectList[i].SubjectId + ",";
                }
                subjectIds += Manufacturer.SubjectList[lengthSubjectList - 1].SubjectId;
                Manufacturer.SubjectIds = subjectIds;
            }*//*
            var isValidate = base.Validate(Manufacturer);
            if(isValidate)
            {
                var row = _ManufacturerRepository.Add(Manufacturer);
                *//*if (row > 0)
                {
                    var ManufacturerManufacturerService = new ManufacturerManufacturerService(_ManufacturerManufacturerRepository);
                    var entity = _ManufacturerRepository.GetManufacturerByCreatedDate();
                    for (int i = 0; i < Manufacturer.ManufacturerList.Count; i++)
                    {
                        ManufacturerManufacturer ManufacturerManufacturer = new ManufacturerManufacturer();
                        ManufacturerManufacturer.ManufacturerId = entity.ManufacturerId;
                        ManufacturerManufacturer.ManufacturerId = Manufacturer.ManufacturerList[i].ManufacturerId;
                        ServiceResult = ManufacturerManufacturerService.Add(ManufacturerManufacturer);
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
        

        #endregion
    }
}
