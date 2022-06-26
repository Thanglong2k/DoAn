import CategoryAPI from '@/apis/components/CategoryApi.js'
export default{
    namespaced:true,
    state:{
        categories:[],//Danh sách danh mục
        categoryAll:[],//Danh sách danh mục
        categoryTotal:0,//Tổng số bản ghi
        pageTotal:0.//Tổng số trang
    },
    getters:{},
    mutations:{
        //set giá trị cho danh mục sản phẩm
        setCategorys(state,data){
            
            state.categories=data
        },
        //set giá trị cho danh mục sản phẩm
        setCategoryAll(state,data){
            
            state.categoryAll=data
        },
        //set giá trị cho tổng sản phẩm lấy ra
        setCategoryTotal(state,data){
            state.categoryTotal=data
        },
        //set giá trị cho tổng trang
        setPageTotal(state,data){
            state.pageTotal=data
        }
    },
    actions:{
        /**
         * Lấy danh sách sản phẩm
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 23/02/2022  
         */
        async getCategories({commit},param){
            await CategoryAPI.getAll().then((res)=>{
                
                //lấy ra Danh mục ko có ID cha
                const dataNotParent=res.data.Data.filter(item=>{
                    return !item.CategoryParentID
                })
                //lấy ra Danh mục có ID cha
                const data=res.data.Data.filter(item=>{
                    return item.CategoryParentID
                })
                //duyệt trên list Danh mục lấy được tìm vị trí Danh mục cha và gán nó thành con
                data.forEach(element => {
                    const index=dataNotParent.map(item=>item.CategoryID).indexOf(element.CategoryParentID)
                    if(!dataNotParent[index].Children){
                        dataNotParent[index].Children=[]
                    }
                    dataNotParent[index].Children.push(element)
                });
                commit('setCategorys',dataNotParent)
                commit('setCategoryAll',res.data.Data)
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
         async getCategoriesPaging({commit},param){
            await CategoryAPI.getPaging(param).then((res)=>{
                const data=res?.data?.Data
                commit('setCategoryAll',data.Data)
                commit('setCategoryTotal',data.TotalRecord)
                commit('setPageTotal',data.TotalPage)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Thêm
         * @param {*} param0 
         * @param {*} params 
         */
         async  insertCategory({commit},params){
            return await new Promise((resolve,reject)=>{
                CategoryAPI.insert(params).then((res)=>{
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
        async updateCategory({commit},params){
            return await new Promise((resolve,reject)=>{
            CategoryAPI.update(params.id,params.params).then((res)=>{
                resolve(true)
            }).catch((e)=>{
                reject(false)
            })
        })
        },
        async deleteCategory({commit},id){
            return await new Promise((resolve,reject)=>{
                CategoryAPI.delete(id).then((res)=>{
                    resolve(true)
                }).catch((e)=>{
                    reject(false)
                })
            })
        }
    }
}