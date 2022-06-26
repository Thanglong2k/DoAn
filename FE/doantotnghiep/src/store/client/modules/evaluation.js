import EvaluationAPI from '@/apis/components/EvaluationApi.js'
export default{
    namespaced:true,
    state:{
        productRatingDetail:[],//Danh sách số lượng các mức đánh giá của sản phẩm
        evaluations:[],//Danh sách đánh giá theo sản phẩm
        valueRating:0,//giá trị đánh giá sản phẩm
        contentRating:'',//Nội dung đánh giá sản phẩm
        pageTotal:0,//số trang
        reviewTotal:0//số đánh giá
    },
    getters:{},
    mutations:{
        //set tổng số trang đánh giá sản phẩm
        setPageTotal(state,data){
            
            state.pageTotal=data
        },
        //set tổng số đánh giá
        setReviewTotal(state,data){
            
            state.reviewTotal=data
        },
        //set giá trị đánh giá sản phẩm
        setValueRating(state,data){
            
            state.valueRating=data
        },
        //set giá trị nội dung đánh giá sản phẩm
        setContentRating(state,data){
            
            state.contentRating=data
        },
        //set giá trị cho danh số lượng đánh giá từng mức của sản phẩm
        setProductRatingDetail(state,data){
            
            state.productRatingDetail=data
        },
        //set giá trị cho danh sách đánh giá của sản phẩm
        setEvaluationByProductID(state,data){
            
            state.evaluations=data
        },
        setEvaluations(state,data){
            
            state.evaluations=data
        },
    },
    actions:{
        /**
         * Lấy số lượng các mức đánh giá của sản phẩm
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 13/03/2022  
         */
        async getProductRatingDetail({commit},param){
            await EvaluationAPI.getProductRatingDetail(param).then((res)=>{
                commit('setProductRatingDetail',res.data.Data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Lấy danh sách đánh giá của sản phẩm
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 13/03/2022  
         */
        async getEvaluationByProductID({commit},param){
            await EvaluationAPI.getEvaluationByProductID(param).then((res)=>{
               commit('setEvaluationByProductID',res.data.Data.Data)
               commit('setPageTotal',res.data.Data.TotalPage)
               commit('setReviewTotal',res.data.Data.TotalRecord)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Thêm đánh giá cho sản phẩm
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 13/03/2022  
         */
        async insertReviewOfProuct({commit},param){
            return await new Promise((resolve,reject)=>{
                EvaluationAPI.insert(param).then((res)=>{
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
         async updateEvaluationOfProduct({commit},param){
            return await new Promise((resolve,reject)=>{
             EvaluationAPI.update(param.id,param.Evaluation).then((res)=>{
                resolve(true)
               
            }).catch((e)=>{
                reject(false)
            })
        })
        },
        async deleteEvaluation({commit},id){
            return await new Promise((resolve,reject)=>{
             EvaluationAPI.delete(id).then((res)=>{
                resolve(true)
               
            }).catch((e)=>{
                reject(false)
            })
        })
        },
        getEvaluationsPaging({commit},params){
            EvaluationAPI.getEvaluations(params).then((res)=>{
                commit("setEvaluations",res.data.Data.Data)
                commit('setPageTotal',res.data.Data.TotalPage)
                commit('setEvaluationTotal',res.data.Data.TotalRecord)
            })
        }
    }
}