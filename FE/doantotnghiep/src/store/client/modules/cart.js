export default{
    namespaced:true,
    state:{
        productCartLength:0,//Số sản phẩm trong giỏ
        productCart:[]//danh sách sản phẩm trong giỏ
    },
    getters:{},
    mutations:{
        //set giá trị Số sản phẩm trong giỏ
        setProductCartLength(state,data){
            
            state.productCartLength=data
        },
        //set giá trị cho danh sách sản phẩm trong giỏ
        setProductCart(state,data){
            
            state.productCart=data
        },
        
    },
    actions:{
        
    }
}