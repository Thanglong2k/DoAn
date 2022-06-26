
export default{
    namespaced:true,
    state:{
        toggleNavbar:false,
        toggleMenu:false,
        headerTitle:''

    },
    getters:{
    
    },
    mutations:{
    
        
        setToggleNavbar:(state)=>{state.toggleNavbar=!state.toggleNavbar},
        setToggleManu:(state)=>(state.toggleMenu=!state.toggleMenu),
        setHeaderTitle(state,data){
            state.headerTitle=data
        }

    },
}