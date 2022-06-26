import SuplierAPI from '@/apis/components/SuplierApi.js'
export default{
    namespaced:true,
    state:{       
        supliers:[],//Danh sách đơn hàng
        suplierTotal:0,
        pageTotal:0
    },
    getters:{},
    mutations:{
        setSupliers(state,data){
            state.supliers=data
        },
        //set giá trị cho tổng sản phẩm lấy ra
        setSuplierTotal(state,data){
            state.suplierTotal=data
        },
        //set giá trị cho tổng trang
        setPageTotal(state,data){
            state.pageTotal=data
        }
    },
    actions:{
        async insertSuplier({commit},param){
            debugger
            return await new Promise((resolve,reject)=>{
                SuplierAPI.insert(param).then((res)=>{
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
         async updateSuplier({commit},params){
            return await new Promise((resolve,reject)=>{
                SuplierAPI.update(params.id,params.params).then((res)=>{
                    resolve(true)
                }).catch((e)=>{
                    reject(false)
                })
            })
            
        },
        async deleteSuplier({commit},id){
            return await new Promise((resolve,reject)=>{
                SuplierAPI.delete(id).then((res)=>{
                    resolve(true)
                }).catch((e)=>{
                    reject(false)
                })
            })
            
        },

        /**
         * Lấy danh sách đơn hàng phân trang
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 23/02/2022  
         */
         getSupliers({commit},param){
            SuplierAPI.getAll(param).then((res)=>{
                const data=res?.data?.Data
                commit('setSupliers',data)
               
            }).catch((e)=>{
                console.log(e)
            })
        },
        getSupliersPaging({commit},params){
            SuplierAPI.getPaging(params).then((res)=>{
                const data=res?.data?.Data
                commit('setSupliers',data.Data)
                commit('setSuplierTotal',data.TotalRecord)
                commit('setPageTotal',data.TotalPage)
            })
        },
        
        
    }
}