
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
    public class CommentService:BaseService<Comment>,ICommentService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private ICommentRepository _commentRepository;
        private INotifyRepository _notifyRepository;

        #endregion
        #region Constructor
        public CommentService(ICommentRepository commentRepository,INotifyRepository notifyRepository) :base(commentRepository)
        {
            //ServiceResult = new ServiceResult();
            
            _commentRepository = commentRepository;
            _notifyRepository = notifyRepository;

        }
        #endregion
        #region Method
        #region
        /*public override ServiceResult Add(Comment Comment)
        {
           /* var CommentIds = "";
            var lengthComments = Comment.CommentList.Count;
            if (lengthComments > 0)
            {

                for (int i = 0; i < lengthComments - 1; i++)
                {
                    CommentIds += Comment.CommentList[i].CommentId + ",";
                }
                CommentIds += Comment.CommentList[lengthComments - 1].CommentId;
                Comment.CommentIds = CommentIds;
            }


            var subjectIds = "";
            var lengthSubjectList = Comment.SubjectList.Count;
            if (lengthSubjectList > 0)
            {

                for (int i = 0; i < lengthSubjectList - 1; i++)
                {
                    subjectIds += Comment.SubjectList[i].SubjectId + ",";
                }
                subjectIds += Comment.SubjectList[lengthSubjectList - 1].SubjectId;
                Comment.SubjectIds = subjectIds;
            }*//*
            var isValidate = base.Validate(Comment);
            if(isValidate)
            {
                var row = _CommentRepository.Add(Comment);
                *//*if (row > 0)
                {
                    var CommentCommentService = new CommentCommentService(_CommentCommentRepository);
                    var entity = _CommentRepository.GetCommentByCreatedDate();
                    for (int i = 0; i < Comment.CommentList.Count; i++)
                    {
                        CommentComment CommentComment = new CommentComment();
                        CommentComment.CommentId = entity.CommentId;
                        CommentComment.CommentId = Comment.CommentList[i].CommentId;
                        ServiceResult = CommentCommentService.Add(CommentComment);
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

        public ServiceResult GetCommentByProductAttributeID(Guid productAttributeID, int pageIndex, int pageSize)
        {
            var commentByProductID= _commentRepository.GetCommentByProductAttributeID(productAttributeID, pageIndex,pageSize);

            ServiceResult.Data = commentByProductID;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetComments(string? queryText, int pageIndex, int pageSize)
        {
            var commentByProductID= _commentRepository.GetComments(queryText, pageIndex,pageSize);

            ServiceResult.Data = commentByProductID;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public override ServiceResult Add(Comment comment)
        {
            comment.EntityState = Enum.EntityState.AddNew;
            var isValidate = Validate(comment);

            if (isValidate)
            {
                var data = _commentRepository.Add(comment);
                ServiceResult.Data = data;
                if ((int)ServiceResult.Data > 0)
                {
                    Notify notify = new Notify();
                    notify.ProductAttributeID = comment.ProductAttributeID;
                    notify.CustomerID= comment.CustomerID;
                    notify.Type = Enum.Notify.Comment;
                    var notifyResult = _notifyRepository.Add(notify);
                    ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
                    ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
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


        #endregion
    }
}
