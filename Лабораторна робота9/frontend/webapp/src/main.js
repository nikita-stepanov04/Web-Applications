import { createApp } from 'vue'
import App from './App.vue'
import VueCookies from 'vue-cookies'
import router from '@/router/router'
import store from '@/store/store'

import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.min'
import '@/assets/css/style.css'

import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { fab } from "@fortawesome/free-brands-svg-icons";
import { fas } from "@fortawesome/free-solid-svg-icons";
library.add( fas, fab );

const app = createApp(App);
app
    .component('font-awesome-icon', FontAwesomeIcon)
    .use(VueCookies)
    .use(store)
    .use(router);

const properties = app.config.globalProperties;
properties.$store.dispatch('fetchDishTypes');
properties.$store.dispatch('fetchDishes', {});
properties.$store.commit('setCart', properties.$cookies.get('orders'));

app.mount('#app');
