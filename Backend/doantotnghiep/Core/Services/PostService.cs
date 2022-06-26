
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
    public class PostService:BaseService<Post>,IPostService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private IPostRepository _postRepository;
        private INotifyRepository _notifyRepository;

        #endregion
        #region Constructor
        public PostService(IPostRepository postRepository,INotifyRepository notifyRepository) :base(postRepository)
        {
            //ServiceResult = new ServiceResult();
            
            _postRepository = postRepository;
            _notifyRepository = notifyRepository;

        }
        #endregion
        #region Method
        #region
        /*public override ServiceResult Add(Post Post)
        {
           /* var PostIds = "";
            var lengthPosts = Post.PostList.Count;
            if (lengthPosts > 0)
            {

                for (int i = 0; i < lengthPosts - 1; i++)
                {
                    PostIds += Post.PostList[i].PostId + ",";
                }
                PostIds += Post.PostList[lengthPosts - 1].PostId;
                Post.PostIds = PostIds;
            }


            var subjectIds = "";
            var lengthSubjectList = Post.SubjectList.Count;
            if (lengthSubjectList > 0)
            {

                for (int i = 0; i < lengthSubjectList - 1; i++)
                {
                    subjectIds += Post.SubjectList[i].SubjectId + ",";
                }
                subjectIds += Post.SubjectList[lengthSubjectList - 1].SubjectId;
                Post.SubjectIds = subjectIds;
            }*//*
            var isValidate = base.Validate(Post);
            if(isValidate)
            {
                var row = _PostRepository.Add(Post);
                *//*if (row > 0)
                {
                    var PostPostService = new PostPostService(_PostPostRepository);
                    var entity = _PostRepository.GetPostByCreatedDate();
                    for (int i = 0; i < Post.PostList.Count; i++)
                    {
                        PostPost PostPost = new PostPost();
                        PostPost.PostId = entity.PostId;
                        PostPost.PostId = Post.PostList[i].PostId;
                        ServiceResult = PostPostService.Add(PostPost);
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

        public ServiceResult GetPostByProductID(Guid productID, int pageIndex, int pageSize)
        {
            var postByProductID= _postRepository.GetPostByProductID(productID, pageIndex,pageSize);

            ServiceResult.Data = postByProductID;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetPostByID(Guid postID, bool admin)
        {
            var postByProductID = _postRepository.GetPostByID(postID, admin);

            ServiceResult.Data = postByProductID;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetPosts(string? queryText, int pageIndex, int pageSize)
        {
            var postByProductID= _postRepository.GetPosts(queryText, pageIndex,pageSize);

            ServiceResult.Data = postByProductID;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
       


        #endregion
    }
}
