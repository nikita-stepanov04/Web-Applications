import { createApp } from 'vue'
import App from './App.vue'
import router from '@/router/router'

import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.min'

import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

const app = createApp(App)
library.add(fas);

    app
        .component('font-awesome-icon', FontAwesomeIcon)
        .use(router)
        .mount('#app')
