import NotifyApi from "../../../apis/components/NotifyApi"
export default{
    namespaced:true,
    state:{
        notifies:[],
        notifyNotViewTotal:0
    },
    getters:{

    },
    mutations:{
        setNotifies(state,data){
            state.notifies=data
        },
        setNotifyNotViewTotal(state,data){
            state.notifyNotViewTotal=data
        },

    },
    actions:{
        getNotifies({commit}){
            NotifyApi.getNotifies().then((res)=>{
                commit("setNotifies",res.data.Data)
            })
        },
        getNotifyNotViewTotal({commit}){
            NotifyApi.getNotifyNotViewTotal().then((res)=>{
                commit("setNotifyNotViewTotal",res.data.Data)
            })
        },
        updateIsView({commit}){
            return new Promise((resolve,reject)=>{
            NotifyApi.updateIsView().then((res)=>{
                resolve(true)
            }).catch(e=>{
                reject(false)
            })
        })
        },
        updateIsRead({commit},params){
            return new Promise((resolve,reject)=>{
                NotifyApi.updateIsRead(params).then((res)=>{
                    resolve(true)
                }).catch(e=>{
                    reject(false)
                })
            }) 
        },
    }
}