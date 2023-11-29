import {createRouter, createWebHistory} from "vue-router";
import store from "@/store/store"
import MenuPage from "@/pages/MenuPage.vue";
import CartPage from "@/pages/CartPage.vue";
import RegistrationPage from "@/pages/RegistrationPage.vue";
import LoginPage from "@/pages/LoginPage.vue";
import MyAccountPage from "@/pages/MyAccountPage.vue";
import DishPage from "@/pages/DishPage.vue";
import OrderPage from "@/pages/OrderPage.vue";
import OrdersPage from "@/pages/OrdersPage.vue";
import SchedulePage from "@/pages/SchedulePage.vue";
import AdminOrdersPage from "@/pages/adminPages/AdminOrdersPage.vue";
import AdminEditOrderPage from "@/pages/adminPages/AdminEditOrderPage.vue";

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
        path: '/menu/:id',
        name: 'dish',
        meta: {
            title: 'Dish'
        },
        component: DishPage,
    },
    {
        path: '/cart',
        component: CartPage,
        meta: {
            title: 'Cart'
        }
    },
    {
        path: '/order',
        component: OrderPage,
        meta: {
            title: 'Order'
        }
    },
    {
        path: '/orders',
        component: OrdersPage,
        meta: {
            title: 'Orders',
            authRequired: true
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
    {
        path: '/schedule',
        component: SchedulePage,
        meta: {
            title: 'Schedule'
        }
    },

    // Admin pages

    {
        path: '/admin',
        redirect: '/admin/orders'
    },
    {
        path: '/admin/orders',
        component: AdminOrdersPage,
        meta: {
            title: 'Orders',
            authRequired: true,
            adminPage: true
        }
    },
    {
        path: '/admin/edit/order/:id',
        component: AdminEditOrderPage,
        name: 'edit-order',
        meta: {
            title: 'Edit order',
            authRequired: true,
            adminPage: true
        }
    },

]

const router = createRouter({
    routes,
    history: createWebHistory()
})

router.beforeEach(async (to, from, next) => {
    document.title = to.meta?.title || 'Title';
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
