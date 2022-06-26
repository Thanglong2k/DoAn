import ReceiptAPI from '@/apis/components/ReceiptApi.js'
export default{
    namespaced:true,
    state:{
        receiptDetails:[],//Danh sách chi tiết đơn hàng khi xem chi tiết
        receipts:[],//Danh sách đơn hàng
        receiptTotal:0,//Tổng đơn hàng lấy ra
        pageTotal:0,//Tổng số trang
        receiptDetailTotal:0,//Tổng đơn hàng lấy ra
        pageReceiptDetailTotal:0,//Tổng số trang
        receiptSuplier:null,//thông tin đơn hàng và khách hàng
        receipt:null,
    },
    getters:{},
    mutations:{
        setReceipt(state,data){
            state.receipt=data
        },
        setReceipts(state,data){
            state.receipts=data
        },
        setReceiptDetails(state,data){
            state.receiptDetails=data
        },
        //set giá trị cho tổng đơn hàng lấy ra
        setReceiptTotal(state,data){
            state.receiptTotal=data
        },
        //set giá trị cho tổng trang
        setPageTotal(state,data){
            state.pageTotal=data
        },
        //set giá trị cho tổng đơn hàng lấy ra
        setReceiptDetailTotal(state,data){
            state.receiptDetailTotal=data
        },
        //set giá trị cho tổng trang
        setPageReceiptDetailTotal(state,data){
            state.pageReceiptDetailTotal=data
        },
        setReceiptSuplier(state,data){
            state.receiptSuplier=data
        }
        
    },
    actions:{
        async insertReceipt({commit},param){
            return await new Promise((resolve,reject)=>{
                ReceiptAPI.insertReceipt(param).then((res)=>{
                    resolve(true)
                }).catch((e)=>{
                    reject(false)
                })
            })
            
        },
        /**
         * Sửa
         * @param {*} param0 
         * @param {*} id 
         */
         async  updateReceipt({commit},params){
            return await new Promise((resolve,reject)=>{
                ReceiptAPI.updateReceipt(params.id,params.params).then((res)=>{
                    resolve(true)
                }).catch((e)=>{
                    reject(false)
                })
            })
        },
        async deleteReceipt({commit},id){
            return await new Promise((resolve,reject)=>{
            ReceiptAPI.delete(id).then((res)=>{
                resolve(true)
            }).catch((e)=>{
                reject(false)
            })
        })
        },
        detailReceipt({commit},id){
            ReceiptAPI.getReceiptDetailbyReceiptID(id).then((res)=>{
                const data=res.data.Data
                commit('setReceiptDetails',data.Data)
                commit('setReceiptDetailTotal',data.TotalRecord)
                commit('setPageReceiptDetailTotal',data.TotalPage)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Lấy danh sách đơn hàng phân trang
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 23/02/2022  
         */
         getReceiptsPaging({commit},param){
            ReceiptAPI.getPaging(param).then((res)=>{
                const data=res?.data?.Data
                commit('setReceipts',data.Data)
                commit('setReceiptTotal',data.TotalRecord)
                commit('setPageTotal',data.TotalPage)
            }).catch((e)=>{
                console.log(e)
            })
        },
        getSuplierReceipt({commit},param){
            ReceiptAPI.getSuplierReceipt(param).then((res)=>{
                const data=res?.data?.Data
                commit('setReceiptSuplier',data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        async getReceiptByID({commit},id){
            return await new Promise((resolve,reject)=>{
                
                ReceiptAPI.getRecetptByID(id).then((res)=>{
                   commit("setReceipt",res.data.Data)
                    resolve(res.data.Data)
               }).catch((e)=>{
                   console.log(e)
                    reject(e)
               })
            })
            
        }
    }
}