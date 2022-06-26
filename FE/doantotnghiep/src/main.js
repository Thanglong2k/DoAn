import Vue from 'vue'
import App from './App.vue'
import router from './router/index'
import Store from './store/index'
import Bus from "vue-plugin-event-bus"
import vClickOutside from 'vue-click-outside'
import './assets/css/tailwind.css'
import vuetify from './plugins/vuetify'
import common from './scripts/common'
import enumeration from './scripts/enumeration'
import constant from './scripts/constants'
import VueI18n from 'vue-i18n'
import Element from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import ProductZoomer from 'vue-product-zoomer'
import locale from 'element-ui/lib/locale/lang/vi'
import style from './styles/style.scss'
import CommentHub from '@/scripts/comment-hub.js'

Vue.use(Element,{ locale })
Vue.use(ProductZoomer)
Vue.use(CommentHub)


Vue.use(VueI18n)
Vue.use(style)
//Load all locales and remember context
function loadMessages() {
  const context = require.context("./locales", true, /[A-Za-z0-9-_]+\/[A-Za-z0-9-_]+\.js$/i);
  const contextFolder = require.context("./locales", true, /[A-Za-z0-9-_]+\/[A-Za-z0-9-_]+\/[A-Za-z0-9-_]+\.js$/i);
  const messages = {};
  context.keys().forEach(key => {
        const matched = key.match(/([A-Za-z0-9-_]+)/);
        if (matched && matched.length > 1) {
            const locale = matched[1];
            const message=context(key).default;
            messages[locale] = {...messages[locale],...message}
        }
    });
    contextFolder.keys().forEach(key => {
      const matched = key.match(/([A-Za-z0-9-_]+)/);
      if (matched && matched.length > 1) {
          const locale = matched[1];
          const message=context(key).default;
          messages[locale] = {...messages[locale],...message}
      }
  });
    return { context, contextFolder, messages };
}

const { context, contextFolder, messages } = loadMessages();
const messages1 = {
  vi: {
      Input: {
          Ca: 'Hello'
      }
  },
  de: {
      message: {
          hello: 'Guten Tag, {name}!'
      }
  }
};
// VueI18n instance
const i18n = new VueI18n({
  locale: "vi",
  messages,
});

Vue.use(vClickOutside);
Vue.prototype.$commonFn = common;
Vue.prototype.$enumeration = enumeration;
Vue.prototype.$constant = constant;
// Vue.prototype.$t = (text)=>{
//   let t=require(`./locales/vi/${text.split('.')[0].toLowerCase()}`)
//   debugger
//   // let t=text.split('.')
//   // let result=t.map((value)=>{
//   //   return eval(value)
//   // })
//   let result=common.executeFunctionByName(text,t.default)
//   //let result=t.default["Button"]["Long"]
//   return result
// };
Vue.use(Bus);
new Vue({
  router,
  store:Store,
  vuetify,
  i18n,
  render: h => h(App)
}).$mount('#app')
//Định nghĩa prototy format
String.prototype.format = function() {
  var ans = this;
  for (let k in arguments) {
    ans = ans.replace('{' + k + '}', arguments[k]);
  }
  return ans;
};
// Hot updates
// if (module.hot) {
//   module.hot.accept(context.id, () => {
//     const { messages: newMessages } = loadMessages();

//     Object.keys(newMessages)
//       .filter((locale) => messages[locale] !== newMessages[locale])
//       .forEach((locale) => {
//         messages[locale] = newMessages[locale];
//         i18n.setLocaleMessage(locale, messages[locale]);
//       });
//   });
// }
