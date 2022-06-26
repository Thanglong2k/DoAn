import ProductAPI from '@/apis/components/ProductApi.js'
export default{
    namespaced:true,
    state:{
        products:[],//Danh sách sản phẩm
        product:null,//Sản phẩm
        newProducts:[],//Danh sách sản phẩm mới
        bestSellingProducts:[],//Danh sách sản phẩm bạn nhất
        productTotal:0,//số lượng sản phẩm lấy ra
        pageTotal:0,//số lượng trang
        relatedProducts:[],//Danh sách sản phẩm liên quan cùng hãng SX
        filter:[],//bộ lọc sản phẩm
        compareProducts:[],//Dánh sách sản phẩm so sánh
        recommoandProducts:[],//Dánh sách sản phẩm gợi ý so sánh
        productDetailTotal:0,
        productDetails:[],
        queryText:'',
        newProductPosts:[],
        newProductTotal:0,
        newPageTotal:0
    },
    getters:{},
    mutations:{
        //set giá trị bộ filter
        setFilter(state,data){
            state.filter=data
        },
        //set giá trị danh sách sản phẩm so sánh
        setCompareProducts(state,data){
            
            state.compareProducts=data
            
        },
        //set giá trị cho danh sách sản phẩm
        setProductByID(state,data){
            state.product=data
        },
        setProduct(state,data){
            state.product=data
        },
        //set giá trị cho danh sách sản phẩm
        setProducts(state,data){
            state.products=data
        },
        //set giá trị cho danh sách chi tiết sản phẩm
        setProductDetails(state,data){
            state.productDetails=data
        },
        //set giá trị cho danh sách sản phẩm mới
        setNewProducts(state,data){
            state.newProducts=data
        },
        //set giá trị cho danh sách sản phẩm liên quan cùng hãng SX
        setRelatedProducts(state,data){
            state.relatedProducts=data
        },
        //set giá trị cho danh sách sản phẩm gợi ý so sánh
        setRecommoandProducts(state,data){
            state.recommoandProducts=data
        },
        //set giá trị cho danh sách sản phẩm bán chạy
        setBestSellingProducts(state,data){
            state.bestSellingProducts=data
        },
        //set giá trị cho tổng sản phẩm lấy ra
        setProductTotal(state,data){
            state.productTotal=data
        },
        //set giá trị cho tổng số chi tiết sản phẩm lấy ra
        setProductDetailTotal(state,data){
            state.productDetailTotal=data
        },
        //set giá trị cho tổng trang
        setPageTotal(state,data){
            state.pageTotal=data
        },
        setQueryText(state,data){
            state.queryText=data
        },
        setNewProductPosts(state,data){
            if(data.length===0){
                state.newProductPosts=[]
            }else{
                state.newProductPosts=state.newProductPosts.concat(data)
            }
        },
        setNewProductTotal(state,data){
            state.newProductTotal=data
        },
        setNewPageTotal(state,data){
            state.newPageTotal=data
        },
    },
    actions:{
        /**
         * Lấy danh sách sản phẩm
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 23/02/2022  
         */
        async getProductByID({commit},param){
            await ProductAPI.getById(param).then((res)=>{
                
                commit('setProductByID',res.data.Data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        async getProductAttributeByID({commit},param){
            await ProductAPI.getProductAttributeByID(param).then((res)=>{
                
                commit('setProductByID',res.data.Data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Lấy danh sách sản phẩm
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 23/02/2022  
         */
        async getProducts({commit},param){
            await ProductAPI.getAll().then((res)=>{
                
                commit('setProducts',res.data.Data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Lấy danh sách sản phẩm phân trang
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 23/02/2022  
         */
        async getProductsPaging({commit},param){
            await ProductAPI.getProductsPaging(param).then((res)=>{
                const data=res?.data?.Data
                commit('setProducts',data.Data)
                commit('setProductTotal',data.TotalRecord)
                commit('setPageTotal',data.TotalPage)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Lấy danh sách sản phẩm phân trang
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 23/02/2022  
         */
        async getProductAndProductAttribute({commit},param){
            await ProductAPI.getProductAndProductAttribute(param).then((res)=>{
                const data=res?.data?.Data
                commit('setProducts',data.Data)
                commit('setProductTotal',data.TotalRecord)
                commit('setPageTotal',data.TotalPage)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Lấy danh sách sản phẩm mới nhất
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 23/02/2022  
         */
         async getNewProducts({commit},param){
            await ProductAPI.getNewProducts(param).then((res)=>{
                
                commit('setNewProducts',res.data.Data)
            }).catch((e)=>{
                console.log(e)
            })
        },
         async getNewProductPosts({commit},param){
            await ProductAPI.getNewProductPosts(param).then((res)=>{
                const data=res.data.Data
                commit('setNewProductPosts',data.Data)
                commit('setNewProductTotal',data.TotalRecord)
                commit('setNewPageTotal',data.TotalPage)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Lấy danh sách sản phẩm liên quan cùng hãng sản xuất
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 04/03/2022  
         */
         async getRelatedProducts({commit},param){
            await ProductAPI.getRelatedProducts(param).then((res)=>{
                
                commit('setRelatedProducts',res.data.Data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Lấy danh sách sản phẩm bán chạy nhất
         * @param {*} param
         * @param {*} param
         * @author TTLONG
         * CreatedDate 23/02/2022  
         */
         async getBestSellingProducts({commit},param){
            await ProductAPI.getBestSellingProducts(param).then((res)=>{
                
                commit('setBestSellingProducts',res.data.Data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Lấy danh sách sản phẩm theo danh mục
         * @author TTLONG
         * CreatedDate 26/02/2022
         */
        async getProductsByCategory({commit},param){
            await ProductAPI.getProductsByCategory(param).then((res)=>{
                commit('setProducts',res.data.Data.Data)
                commit('setProductTotal',res.data.Data.TotalRecord)
                commit('setPageTotal',res.data.Data.TotalPage)
            }).catch((e)=>{
                console.log(e)
            })
        },
        /**
         * Lấy danh sách sản phẩm gợi ý so sánh
         * @author TTLONG
         * CreatedDate 26/02/2022
         */
        async getRecommendProducts({commit},param){
            await ProductAPI.getRecommendProducts(param).then((res)=>{
                commit('setRecommoandProducts',res.data.Data)
                
            }).catch((e)=>{
                console.log(e)
            })
        },
        insertProduct({commit},param){
            return new Promise((resolve,reject)=>{
            ProductAPI.createProduct(param).then((res)=>{
                resolve(true)
            }).catch((e)=>{
                reject(false)
            })
        })
        },
        updateProduct({commit},param){
            return new Promise((resolve,reject)=>{
            ProductAPI.updateProduct(param.id,param.formData).then((res)=>{
                resolve(true)
            }).catch((e)=>{
                reject(false)
            })
        })
        },
        deleteProduct({commit},id){
            return new Promise((resolve,reject)=>{
            ProductAPI.delete(id).then((res)=>{
                resolve(true)
            }).catch((e)=>{
                reject(false)
            })
        })
        },
        deleteProductDetail({commit},id){
            return new Promise((resolve,reject)=>{
            ProductAPI.deleteProductDetail(id).then((res)=>{
                resolve(true)
            }).catch((e)=>{
                reject(false)
            })
        })
        },
        async getProductDetailsPaging({commit},param){
            await ProductAPI.getProductDetailsPaging(param).then((res)=>{
                const data=res?.data?.Data
                commit('setProductDetails',data.Data)
                commit('setProductDetailTotal',data.TotalRecord)
                commit('setPageTotal',data.TotalPage)
            }).catch((e)=>{
                console.log(e)
            })
        },
        async warrentyLookup({commit},param){
            await ProductAPI.warrentyLookup(param).then((res)=>{
                const data=res?.data?.Data
                commit('setProduct',data)
            }).catch((e)=>{
                console.log(e)
            })
        },
    },
}