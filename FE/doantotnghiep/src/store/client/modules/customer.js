import CustomerAPI from '@/apis/components/CustomerApi.js'
export default{
    namespaced:true,
    state:{
        customers:[],//Danh sách khách hàng
        customer:null//thông tin khách hàng
    },
    getters:{},
    mutations:{
        
        //set giá trị cho  thông tin khách hàng
        setCustomer(state,data){
            state.customer=data
        },
        //set giá trị cho danh sách thông tin khách hàng
        setCustomers(state,data){
            state.customers=data
        },
        setCustomerTotal(state, data) {
            state.customerTotal = data
        },
        setPageTotal(state, data) {
            state.pageTotal = data
        },
        
    },
    actions:{
        
        /**
         * Lấy danh sách khách hàng của
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 13/03/2022  
         */
        async getCustomerByPhoneNumber({commit},param){
            await CustomerAPI.getCustomerByPhoneNumber(param).then((res)=>{
                commit('setCustomer',res.data.Data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * thêm khách hàng
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 13/03/2022  
         */
        async insertCustomer({commit},param){
            console.log(5);
            return await new Promise((resolve,reject)=>{
                 CustomerAPI.insert(param).then((res)=>{
                    resolve(true)
                   
                }).catch((e)=>{
                    reject(false)
                })
            })
        },
        async updateCustomer({commit},param){
            console.log(4);
            return await new Promise((resolve,reject)=>{
                 CustomerAPI.update(param.id,param.customer).then((res)=>{
                    resolve(true)
                   
                }).catch((e)=>{
                    reject(false)
                })
            })
        },
        /**
         * Xóa khách hàng
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 13/03/2022  
         */
        async deleteCustomer({commit},param){
            return await new Promise((resolve,reject)=>{
                 CustomerAPI.delete(param).then((res)=>{
                    resolve(true)
                   
                }).catch((e)=>{
                    reject(false)
                })
            })
        },
        
        getCustomersPaging({commit},params){
            CustomerAPI.getPaging(params).then((res)=>{
                const data=res?.data?.Data
                commit('setCustomers',data.Data)
                commit('setCustomerTotal',data.TotalRecord)
                commit('setPageTotal',data.TotalPage)
            })
        },
        async getCustomerByID({commit},params){
            await CustomerAPI.getById(params.CustomerID).then((res)=>{
                const data=res?.data?.Data
                commit('setCustomer',data)
            })
        }
    }
}