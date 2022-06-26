
import OverviewApi from '../../../apis/components/OverviewApi'
export default{
    namespaced:true,
    state:{
        turnoverTotal:0,
        orderTotal:0,
        receiptTotal:0,
        newProductTotal:0,
        monthlyTurnover:[],
        quantitySoldByManufacturer:[],
        quantityOrderCancelled:0,
        productBestSellings:[],
        cancelledOrders:[],
        month:new Date().getMonth()+1
        
    },
    getters:{},
    mutations:{
        setTurnoverTotal(state,data){
            state.turnoverTotal=data
        },
        setOrderTotal(state,data){
            state.orderTotal=data
        },
        setReceiptTotal(state,data){
            state.receiptTotal=data
        },
        setNewProductTotal(state,data){
            state.newProductTotal=data
        },
        setMonthlyTurnover(state,data){
            state.monthlyTurnover=data
        },
        setQuantitySoldByManufacturer(state,data){
            state.quantitySoldByManufacturer=data
        },
        setQuantityOrderCancelled(state,data){
            state.quantityOrderCancelled=data
        },
        setProductBestSellings(state,data){
            state.productBestSellings=data
        },
        setCancelledOrders(state,data){
            state.cancelledOrders=data
        },
        setMonth(state,data){
            state.month=data
        }
        
        
    },
    actions:{
        
        getTurnoverTotal({commit},month){
            OverviewApi.getTurnoverTotal(month).then((res)=>{
                const data=res?.data?.Data
                commit('setTurnoverTotal',data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        getOrderTotal({commit},month){
            OverviewApi.getOrderTotal(month).then((res)=>{
                const data=res?.data?.Data
                commit('setOrderTotal',data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        getReceiptTotal({commit},month){
            OverviewApi.getReceiptTotal(month).then((res)=>{
                const data=res?.data?.Data
                commit('setReceiptTotal',data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        getNewProductTotal({commit},month){
            OverviewApi.getNewProductTotal(month).then((res)=>{
                const data=res?.data?.Data
                commit('setNewProductTotal',data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        async getMonthlyTurnover({commit}){
            
            await OverviewApi.getMonthlyTurnover().then((res)=>{
                const data=res?.data?.Data
                const dataNew=[]
                for (const property in data) {
                    dataNew.push(data[property])
                }
                commit('setMonthlyTurnover',dataNew)
            }).catch((e)=>{
                console.log(e)
            })
        },
        getQuantitySoldByManufacturer({commit},month){
            
             OverviewApi.getQuantitySoldByManufacturer(month).then((res)=>{
                const data=res?.data?.Data
                
                commit('setQuantitySoldByManufacturer',data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        getQuantityOrderCancelled({commit},month){
            
             OverviewApi.getQuantityOrderCancelled(month).then((res)=>{
                const data=res?.data?.Data
                
                commit('setQuantityOrderCancelled',data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        getProductBestSellingsInMonth({commit},param){
            
             OverviewApi.getProductBestSellingsInMonth(param).then((res)=>{
                const data=res?.data?.Data
                
                commit('setProductBestSellings',data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        getCancelledOrders({commit},month){
            
             OverviewApi.getCancelledOrders(month).then((res)=>{
                const data=res?.data?.Data
                
                commit('setCancelledOrders',data)
            }).catch((e)=>{
                console.log(e)
            })
        },
        
        
    }
}