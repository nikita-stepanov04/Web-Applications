import {createRouter, createWebHistory} from "vue-router";
import store from "@/store/store"
import MenuPage from "@/pages/MenuPage.vue";
import CartPage from "@/pages/CartPage.vue";
import RegistrationPage from "@/pages/RegistrationPage.vue";
import LoginPage from "@/pages/LoginPage.vue";
import MyAccountPage from "@/pages/MyAccountPage.vue";

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
    },
    {
        path: '/cart',
        component: CartPage,
        meta: {
            title: 'Cart'
        }
    },
    {
        path: '/register',
        component: RegistrationPage,
        meta: {
            title: 'Register'
        }
    },
    {
        path: '/login',
        component: LoginPage,
        meta: {
            title: 'Log in'
        }
    },
    {
        path: '/my-account',
        component: MyAccountPage,
        meta: {
            title: 'My account',
            authRequired: true
        }
    },

]

const router = createRouter({
    routes,
    history: createWebHistory()
})

router.beforeEach(async (to, from, next) => {
    document.title = to.meta?.title || '';
    if (to.matched.some(route => route.meta.authRequired)) {
        const isAdminPage = to.matched.some(route => route.meta.adminPage);
        const user = store.state.userAuthenticationRole
            ?? await store.dispatch('checkUserAuthorization');

        if (isAdminPage && user !== 'Admin') {
            return next(redirectToLogin(to.fullPath));
        }

        if (!user) {
            return next(redirectToLogin(to.fullPath));
        }
    }

    next();
});


function redirectToLogin(successfulLoginRedirectTo) {
    return {
        path: '/login',
        query: {
            redirect: successfulLoginRedirectTo
        }
    }
}

export default router;
