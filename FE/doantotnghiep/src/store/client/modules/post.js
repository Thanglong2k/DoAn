import PostAPI from '@/apis/components/PostApi.js'
export default{
    namespaced:true,
    state:{
        posts:[],//Danh sách bài viết theo sản phẩm
        contentPost:'',//Nội dung bài viết sản phẩm
        pageTotal:0,//số trang
        postTotal:0,//số bài viết
        post:{}
    },
    getters:{},
    mutations:{
        //set tổng số trang bài viết sản phẩm
        setPageTotal(state,data){
            
            state.pageTotal=data
        },
        //set tổng số bài viết
        setPostTotal(state,data){
            
            state.postTotal=data
        },
        //set giá trị nội dung bài viết sản phẩm
        setContentPost(state,data){
            
            state.contentPost=data
        },
        //set giá trị cho danh sách bài viết của sản phẩm
        setPostByProductID(state,data){
            state.posts=data
        },
        //set giá trị cho danh sách bài viết của sản phẩm
        setPosts(state,data){
            state.posts=data
        },
        //set giá trị cho bài viết của sản phẩm
        setPost(state,data){
            state.post=data
        },
        
    },
    actions:{
        
        /**
         * Lấy danh sách bài viết của sản phẩm
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 13/03/2022  
         */
        async getPostByProductID({commit},param){
            await PostAPI.getPostByProductID(param).then((res)=>{
                
                commit('setPostByProductID',res.data.Data.Data)
                commit('setPageTotal',res.data.Data.TotalPage)
                commit('setPostTotal',res.data.Data.TotalRecord)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * thêm bài viết cho sản phẩm
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 13/03/2022  
         */
        async insertPostOfProduct({commit},param){
            return await new Promise((resolve,reject)=>{
             PostAPI.insertPostOfProduct(param).then((res)=>{
                resolve(true)
               
            }).catch((e)=>{
                reject(false)
            })
        })
        },
        /**
         * sửa bài viết cho sản phẩm
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 13/03/2022  
         */
        async updatePostOfProduct({commit},param){
            return await new Promise((resolve,reject)=>{
             PostAPI.updatePostOfProduct(param.id,param.formData).then((res)=>{
                resolve(true)
               
            }).catch((e)=>{
                reject(false)
            })
        })
        },
        async deletePost({commit},id){
            return await new Promise((resolve,reject)=>{
             PostAPI.delete(id).then((res)=>{
                resolve(true)
               
            }).catch((e)=>{
                reject(false)
            })
        })
        },
        getPostsPaging({commit},params){
            PostAPI.getPosts(params).then((res)=>{
                commit("setPosts",res.data.Data.Data)
                commit('setPageTotal',res.data.Data.TotalPage)
                commit('setPostTotal',res.data.Data.TotalRecord)
            })
        },
        async getPostDetailByID({commit},params){
            await PostAPI.getPostDetailByID(params).then((res)=>{
                commit("setPost",res.data.Data)
                
            })
        }
    }
}