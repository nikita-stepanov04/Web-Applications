import {createRouter, createWebHistory} from "vue-router";
import MenuPage from "@/pages/MenuPage.vue";

const routes = [
    {
        path: '/',
        redirect: '/menu'
    },
    {
        path: '/menu',
        component: MenuPage,
        meta: {
            title: 'Menu'
        }
    }
]

const router = createRouter({
    routes,
    history: createWebHistory()
})

router.beforeEach(to => {
    document.title = to.meta?.title ?? ''
})

export default router;
