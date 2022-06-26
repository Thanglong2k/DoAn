import Vue from 'vue';
import Vuex from 'vuex';


Vue.use(Vuex)

const storeData={
    state: {
        sidebarVisible: '',
        sidebarUnfoldable: false,
      },
      mutations: {
        toggleSidebar(state) {
          state.sidebarVisible = !state.sidebarVisible
        },
        toggleUnfoldable(state) {
          state.sidebarUnfoldable = !state.sidebarUnfoldable
        },
        updateSidebarVisible(state, payload) {
          state.sidebarVisible = payload.value
        },
      },
      actions: {},
      modules: {},
    
}
const store=new Vuex.Store(storeData)
export default store