import CommentAPI from '@/apis/components/CommentApi.js'
export default{
    namespaced:true,
    state:{
        comments:[],//Danh sách bình luận theo sản phẩm
        contentComment:'',//Nội dung bình luận sản phẩm
        pageTotal:0,//số trang
        commentTotal:0//số bình luận
    },
    getters:{},
    mutations:{
        //set tổng số trang bình luận sản phẩm
        setPageTotal(state,data){
            
            state.pageTotal=data
        },
        //set tổng số bình luận
        setCommentTotal(state,data){
            
            state.commentTotal=data
        },
        //set giá trị nội dung bình luận sản phẩm
        setContentComment(state,data){
            
            state.contentComment=data
        },
        //set giá trị cho danh sách bình luận của sản phẩm
        setCommentByProductID(state,data){
            state.comments=data
        },
        //set giá trị cho danh sách bình luận của sản phẩm
        setComments(state,data){
            state.comments=data
        },
        
    },
    actions:{
        
        /**
         * Lấy danh sách bình luận của sản phẩm
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 13/03/2022  
         */
        async getCommentByProductID({commit},param){
            await CommentAPI.getCommentByProductID(param).then((res)=>{
                const data=res.data.Data.Data
                const dataNotParent=data.filter(item=>{
                    return !item.Comment.ParentCommentID
                })
                const dataHasParent=data.filter(item=>{
                    return item.Comment.ParentCommentID
                })
                const dataFinal=dataNotParent.map(item=>{
                    const childComment=dataHasParent.filter(child=>{
                        return child.Comment.ParentCommentID===item.Comment.CommentID
                    })
                    item.Comment.ChildComment=childComment
                    return item
                })
                commit('setCommentByProductID',dataFinal)
                commit('setPageTotal',res.data.Data.TotalPage)
                commit('setCommentTotal',res.data.Data.TotalRecord)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * thêm bình luận cho sản phẩm
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 13/03/2022  
         */
        async insertCommentOfProduct({commit},param){
            return await new Promise((resolve,reject)=>{
             CommentAPI.insertComment(param).then((res)=>{
                resolve(true)
               
            }).catch((e)=>{
                reject(false)
            })
        })
        },
        /**
         * sửa bình luận cho sản phẩm
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 13/03/2022  
         */
        async updateCommentOfProduct({commit},param){
            return await new Promise((resolve,reject)=>{
             CommentAPI.update(param.id,param.Comment).then((res)=>{
                resolve(true)
               
            }).catch((e)=>{
                reject(false)
            })
        })
        },
        async deleteComment({commit},id){
            return await new Promise((resolve,reject)=>{
             CommentAPI.delete(id).then((res)=>{
                resolve(true)
               
            }).catch((e)=>{
                reject(false)
            })
        })
        },
        getCommentsPaging({commit},params){
            CommentAPI.getComments(params).then((res)=>{
                commit("setComments",res.data.Data.Data)
                commit('setPageTotal',res.data.Data.TotalPage)
                commit('setCommentTotal',res.data.Data.TotalRecord)
            })
        }
    }
}