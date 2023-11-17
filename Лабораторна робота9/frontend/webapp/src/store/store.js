import {createStore} from "vuex";
import request from "@/api/requestConstructor";

export default createStore({
    state: {
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
            return state.orders.length;
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
        changeCartPage(state, page) {
            state.ordersPagination.currentPage = page;
        }
    },
    actions: {
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
        }
    }
})
