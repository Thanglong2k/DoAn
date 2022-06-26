import ManufacturerAPI from '@/apis/components/ManufacturerApi.js'
export default{
    namespaced:true,
    state:{
        manufacturers:[],//Danh sách hãng sản xuất
        manufacturerTotal:0,//Tổng số bản ghi
        pageTotal:0,//Tổng số trang
        manufacturer:null
    },
    getters:{},
    mutations:{
        //set giá trị cho danh sách hãng sản xuất
        setManufacture(state,data){
            
            state.manufacturer=data
        },
        //set giá trị cho danh sách hãng sản xuất
        setManufacturers(state,data){
            
            state.manufacturers=data
        },
        //set giá trị cho tổng sản phẩm lấy ra
        setManufacturerTotal(state,data){
            state.manufacturerTotal=data
        },
        //set giá trị cho tổng trang
        setPageTotal(state,data){
            state.pageTotal=data
        }
        
    },
    actions:{
        /**
         * Lấy danh sách hãng sản xuất
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 23/02/2022  
         */
        async getManufacturers({commit},param){
            await ManufacturerAPI.getAll().then((res)=>{
                const data=res.data.Data.map(item=>{
                    item.IsCheck=false
                    return item
                })
                commit('setManufacturers',data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Lấy danh sách hãng sản xuất phân trang
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 23/02/2022  
         */
         async getManufacturersPaging({commit},param){
            await ManufacturerAPI.getPaging(param).then((res)=>{
                const data=res?.data?.Data
                commit('setManufacturers',data.Data)
                commit('setManufacturerTotal',data.TotalRecord)
                commit('setPageTotal',data.TotalPage)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Thêm hãng sản xuất
         * @param {*} param0 
         * @param {*} params 
         */
         async insertManufacturer({commit},params){
            return await new Promise((resolve,reject)=>{
                ManufacturerAPI.insert(params).then((res)=>{
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
         async  updateManufacturer({commit},params){
            return await new Promise((resolve,reject)=>{
                ManufacturerAPI.update(params.id,params.params).then((res)=>{
                   resolve(true)
                  
               }).catch((e)=>{
                   reject(false)
               })
           })
            
        },
        async deleteManufacturer({commit},id){
            return await new Promise((resolve,reject)=>{
                ManufacturerAPI.delete(id).then((res)=>{
                   resolve(true)
                  
               }).catch((e)=>{
                   reject(false)
               })
           })
            
        },
        async getManufacturerByID({commit},id){
            await ManufacturerAPI.getById(id).then((res)=>{
                commit("setManufacture",res.data.Data)
            }).catch((e)=>{
                console.log(e)
            })
        }
    }
}