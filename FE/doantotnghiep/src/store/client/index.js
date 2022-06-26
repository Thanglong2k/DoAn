import {createStore} from 'vuex'

const modules = {}
const vuexModules = require.context('./modules', true, /[A-Za-z0-9-_,\s]+\.(js)$/i)

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
mapModules(vuexModules)

export default {
    modules:modules
}