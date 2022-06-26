import Vue from 'vue';
import Vuex from 'vuex';



Vue.use(Vuex)
const modules = {}
const vuexClientModules = require.context('./client/modules', true, /[A-Za-z0-9-_,\s]+\.(js)$/i)
const vuexAdminModules = require.context('./admin/modules', true, /[A-Za-z0-9-_,\s]+\.(js)$/i)
function mapModules(context) {
    const keys = context.keys() 
    const values = keys.map(context)
    for (let i = 0; i < keys.length; i++) {
        const key = keys[i]
        const matched = key.match(/([A-Za-z0-9-_]+)\./i)
        if (matched && matched.length > 1) {
            const moduleName = matched[1]
            const module = values[i].default
            if (module) {
                modules[moduleName] = values[i].default
            }
        }
    }
}
mapModules(vuexClientModules)
mapModules(vuexAdminModules)
const storeData={
    
    modules:modules
    
}
const store=new Vuex.Store(storeData)

export default store