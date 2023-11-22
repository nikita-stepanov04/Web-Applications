import {createStore} from "vuex";
import request from "@/api/requestConstructor";

export default createStore({
    state: {
        userAuthenticationRole: null,
        userAuthenticationTimer: null,
        userAuthenticationTime: 2 * 60 * 60 * 1000, // 2 hours in milliseconds

        cookies: null,

        dishTypes: [],
        chosenDishTypeId: null,
        dishes: [],
        dishesPagingInfo: {
            itemsPerPage: 6
        },

        orders: [],
        ordersPagination: {
            currentPage: 1,
            itemsPerPage: 6,
        }
    },
    getters: {
        total(state) {
            return state.orders.reduce((total, order) =>
                total + order.price * order.quantity, 0);
        },
        itemsInCart(state) {
            return state.orders.reduce((total, order) =>
                total + order.quantity, 0);
        },
        getCart(state) {
            const pagInfo = state.ordersPagination;
            pagInfo.totalPages = Math.ceil(state.orders.length / pagInfo.itemsPerPage);
            const skip = (pagInfo.currentPage - 1) * pagInfo.itemsPerPage;
            return state.orders
                .slice(skip, skip + pagInfo.itemsPerPage);
        }
    },
    mutations: {
        updateDishTypes(state, dishTypes) {
            state.dishTypes = dishTypes;
        },
        updateDishes(state, dishes) {
            state.dishes = dishes;
        },
        updateDishesPagingInfo(state, pagInfo) {
            state.dishesPagingInfo = pagInfo;
        },
        updateChosenDishTypeId(state, dishTypeId) {
            state.chosenDishTypeId = dishTypeId;
        },
        setCart(state, cart) {
            if (cart != null) {
                state.orders = cart;
            }
        },
        buy(state, dishId) {
            let orders = state.orders;
            const order = orders.filter(o => o.dishId === dishId)[0];
            if (order != null) {
                order.quantity++;
            } else {
                const dish = state.dishes.filter(d => d.id === dishId)[0];
                const order = {
                    dishId: dishId,
                    quantity: 1,
                    name: dish.name,
                    price: dish.price
                }
                state.orders.push(order);
            }
            state.cookies.set('orders', state.orders, '1d');
        },
        changeCartPage(state, page) {
            state.ordersPagination.currentPage = page;
        },
        setCookies(state, cookies) {
            state.cookies = cookies;
        },
        increaseDishQuantity(state, {id, increaseBy}) {
            const order = state.orders.filter(o => o.dishId === id)[0]
            if (order.quantity + increaseBy > 0) {
                order.quantity += increaseBy;
            }
            state.cookies.set('orders', state.orders, '1d');
        },
        removeDishFromCart(state, id) {
            state.orders = id > 0
                ? state.orders.filter(o => o.dishId !== id)
                : []
            state.cookies.set('orders', state.orders, '1d');
        },
        addUser(state, userRole) {
            state.userAuthenticationRole = userRole;
            state.userAuthenticationTimer =
                setTimeout(() =>  {
                    state.userAuthenticationRole = null;
                }, state.userAuthenticationTime);
        },
        clearUser(state) {
            state.userAuthenticationRole = null;
            clearTimeout(state.userAuthenticationTimer);
            state.userAuthenticationTimer = null;
        }
    },
    actions: {
        updateUser({state, commit}, userRole) {
            if (state.userAuthenticationRole != null) {
                commit('clearUser')
            }
            commit('addUser', userRole);
        },

        async logout({commit}) {
            commit('clearUser');
            try {
                await request.get('auth/logout', {}, true);
            } catch (error) {
                console.log('something went wrong')
            }
        },

        async checkUserAuthorization({dispatch}) {
            try {
                const response = await request.get('auth/get-role', {}, true);
                const role = response.data
                dispatch('updateUser', role);
                return role;
            } catch (error) {
                console.log('User is not authenticated');
            }
        },

        async fetchDishTypes({commit}) {
            await request.get('dishes/types')
                .then(response => commit('updateDishTypes', response.data))
                .catch(error => console.log("Error: ", error.message));
        },

        async fetchDishes({commit, state}, {dishTypeId, currentPage}) {
            dishTypeId = dishTypeId ?? state.chosenDishTypeId;
            dishTypeId = dishTypeId === 0 ? null : dishTypeId;
            commit('updateChosenDishTypeId', dishTypeId);
            const params = {
                type: dishTypeId,
                page: currentPage ?? 1,
                perPage: state.dishesPagingInfo.itemsPerPage ?? 6
            }
            await request.get('dishes', params)
                .then(response => {
                    if (response.status !== 200) {
                        throw new Error(`Server responded with type: ${response.status}`);
                    }
                    return response.data;
                })
                .then(data => {
                    commit('updateDishes', data.dishes);
                    commit('updateDishesPagingInfo', data.paginationInfo);
                })
                .catch(error => console.log("Error: ", error.message));
        },
    }
})
