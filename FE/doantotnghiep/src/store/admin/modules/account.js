import AccountAPI from '@/apis/components/AccountApi.js'
import Const from '@/scripts/constants'
export default {
    namespaced: true,
    state: {
        statusAccount: '', //Trạng thái hoạt động của tài khaonr
        isLogin:false,
        accounts:[],
        accountTotal:0,
        pageTotal:0,
        jwtToken:localStorage.getItem('access_token') || null,
        accountID:'',
        account:null,
        accountExists:[]
    },
    getters: {},
    mutations: {
        setStatusAccount(state, data) {
            state.statusAccount = data
        },
        setIsLogin(state, data) {
            state.isLogin = data
        },
        setAccounts(state, data) {
            state.accounts = data
        },
        setAccountTotal(state, data) {
            state.accountTotal = data
        },
        setPageTotal(state, data) {
            state.pageTotal = data
        },
        setJWTToken(state,data){
            state.jwtToken=data
        },
        setAccountID(state,data){
            state.accountID=data
        },
        setAccount(state, data){
            state.account=data
        },
        setCheckAccountExists(state, data){
            
            state.accountExists=data
        },


    },
    actions: {
        async login({
            commit
        }, params) {
            return await new Promise((resolve,reject)=>{
                AccountAPI.checkAccountExists(params).then( (res) => {
                    if(res.data.MISACode===Const.Login.Success){
                        commit('setIsLogin', true)
                    }
    
                    commit('setAccount', res.data.Data)
                    commit('setStatusAccount', res.data.MISACode)
                    localStorage.setItem('access_token', res.data.Data.Token);
                    commit('setJWTToken',res.data.Data.Token)
                    resolve(true)
                }).catch((e) => {
                    reject(e)
                })
            })
            
        },
        getAccountsPaging({commit},params){
            AccountAPI.getPaging(params).then((res)=>{
                const data=res?.data?.Data
                commit('setAccounts',data.Data)
                commit('setAccountTotal',data.TotalRecord)
                commit('setPageTotal',data.TotalPage)
            })
        },
        /**
         * Thêm
         * @param {*} param0 
         * @param {*} params 
         */
        async  insertAccount({commit},params){
            return await new Promise((resolve,reject)=>{
            AccountAPI.insert(params).then((res)=>{
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
         updateAccount({commit},params){
            return new Promise((resolve,reject)=>{
                AccountAPI.update(params.id,params.params).then((res)=>{
                    resolve(true)
                }).catch((e)=>{
                    reject(false)
                })

            })
        },
        deleteAccount({commit},id){
            return new Promise((resolve,reject)=>{
                AccountAPI.delete(id).then((res)=>{
                    resolve(true)
                }).catch((e)=>{
                    reject(false)
                })

            })
        },
        async getAccountID({commit},param){
            await AccountAPI.getAccountID(param).then((res)=>{
                commit("setAccountID",res.data.Data)
            }).catch((e)=>{
                console.log(e);
            })
        },
        async getAccountByUserName({commit},param){
            await AccountAPI.getAccountByUserName(param).then((res)=>{
                commit("setAccount",res.data.Data)
            }).catch((e)=>{
                console.log(e);
            })
        },
        async checkAccountExistsInDB({commit},param){
            await AccountAPI.checkAccountExistsInDB(param).then((res)=>{
                commit("setCheckAccountExists",res.data.Data)
            }).catch((e)=>{
                console.log(e);
            })
        }
    }
}