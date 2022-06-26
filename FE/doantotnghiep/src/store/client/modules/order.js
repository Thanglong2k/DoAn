import OrderAPI from '@/apis/components/OrderApi.js'
export default{
    namespaced:true,
    state:{
        isCheckCustomerPurchasedProduct:[],//kiểm tra khách hàng đã mua sản phẩm chưa
        customerIDPurchased:[],//kiểm tra khách hàng đã mua sản phẩm chưa
        purchasedHistory:[],//Lịch sử mua hàng
        orderDetails:[],//Danh sách chi tiết đơn hàng khi xem chi tiết
        orders:[],//Danh sách đơn hàng
        orderTotal:0,//Tổng đơn hàng lấy ra
        pageTotal:0,//Tổng số trang
        orderDetailTotal:0,//Tổng đơn hàng lấy ra
        pageOrderDetailTotal:0,//Tổng số trang
        orderCustomer:null,//thông tin đơn hàng và khách hàng
        isSearchOrder:false,//đang tra cứu đơn hàng
        phoneNumber:''
    },
    getters:{},
    mutations:{
        setIsCheckCustomerPurchasedProduct(state,data){
            state.isCheckCustomerPurchasedProduct=data
        },
        setCustomerIDPurchased(state,data){
            state.customerIDPurchased=data
        },
        setPurchasedHistory(state,data){
            state.purchasedHistory=data
        },
        setOrders(state,data){
            state.orders=data
        },
        setOrderDetails(state,data){
            state.orderDetails=data
        },
        //set giá trị cho tổng đơn hàng lấy ra
        setOrderTotal(state,data){
            state.orderTotal=data
        },
        //set giá trị cho tổng trang
        setPageTotal(state,data){
            state.pageTotal=data
        },
        //set giá trị cho tổng đơn hàng lấy ra
        setOrderDetailTotal(state,data){
            state.orderDetailTotal=data
        },
        //set giá trị cho tổng trang
        setPageOrderDetailTotal(state,data){
            state.pageOrderDetailTotal=data
        },
        setOrderCustomer(state,data){
            state.orderCustomer=data
        },
        setIsSearchOrder(state,data){
            state.isSearchOrder=data
        },
        setPhoneNumber(state,data){
            state.phoneNumber=data
        }
        
    },
    actions:{
        
        /**
         * Kiểm tra khách hàng đã mua sản phẩm chưa
         * @author TTLONG
         * CreatedDate 19/03/2022
         */
        async checkCustomerPurchasedProduct({commit},param){
            await OrderAPI.checkCustomerPurchasedProduct(param).then((res)=>{
                const data=res.data.Data
                commit('setIsCheckCustomerPurchasedProduct',data.IsPurchased)
                commit('setCustomerIDPurchased',data.CustomerID)
            }).catch((e)=>{
                console.log(e)
            })
        },
        async insertOrder({commit},param){
            console.log(3);
            return await new Promise((resolve,reject)=>{
                OrderAPI.insertOrder(param).then((res)=>{
                   resolve(true)
                  
               }).catch((e)=>{
                   reject(false)
               })
           })
            
        },
        async getPurchasedHistory({commit},param){
            await OrderAPI.getPurchasedHistory(param).then((res)=>{
                commit('setPurchasedHistory',res.data.Data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        async getFollowOrder({commit},param){
            await OrderAPI.getFollowOrder(param).then((res)=>{
                commit('setPurchasedHistory',res.data.Data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Sửa
         * @param {*} param0 
         * @param {*} id 
         */
         async updateOrder({commit},params){
            return await new Promise((resolve,reject)=>{
                OrderAPI.updateOrder(params.id,params.params).then((res)=>{
                   resolve(true)
                  
               }).catch((e)=>{
                   reject(false)
               })
           })
           
        },
        async deleteOrder({commit},id){
            return await new Promise((resolve,reject)=>{
                OrderAPI.delete(id).then((res)=>{
                   resolve(true)
                  
               }).catch((e)=>{
                   reject(false)
               })
           })
            
        },
        detailOrder({commit},id){
            OrderAPI.getOrderDetailbyOrderID(id).then((res)=>{
                const data=res.data.Data
                commit('setOrderDetails',data.Data)
                commit('setOrderDetailTotal',data.TotalRecord)
                commit('setPageOrderDetailTotal',data.TotalPage)
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
         getOrdersPaging({commit},param){
            OrderAPI.getPaging(param).then((res)=>{
                const data=res?.data?.Data
                commit('setOrders',data.Data)
                commit('setOrderTotal',data.TotalRecord)
                commit('setPageTotal',data.TotalPage)
            }).catch((e)=>{
                console.log(e)
            })
        },
        async getCustomerOrder({commit},param){
            await OrderAPI.getCustomerOrder(param).then((res)=>{
                const data=res?.data?.Data
                commit('setOrderCustomer',data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        async updateIMEIByOrderDetailID({commit},params){
            return await new Promise((resolve,reject)=>{
                OrderAPI.updateIMEIByOrderDetailID(params).then((res)=>{
                   resolve(true)
                  
               }).catch((e)=>{
                   reject(false)
               })
           })
        }
        
    }
}