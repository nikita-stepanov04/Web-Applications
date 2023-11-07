import {createRouter, createWebHistory} from "vue-router";

const routes = [
    {
        path: '',
        component: null,
        meta: {
            title: ''
        }
    }
]

const router = createRouter({
    routes,
    history: createWebHistory()
})

router.beforeEach((to, from) =>
    document.title = to?.meta?.title ?? "Title");

export default router;
