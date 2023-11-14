import {createStore} from "vuex";

export default createStore({
    state: {
        dishTypes: [],
        chosenDishTypeId: null,
        dishes: [],
        dishesPagingInfo: {
            itemsPerPage: 6
        },
        orders: [
            {
                dishId: 0,
                quantity: 1,
                name: 'Second pizza',
                price: 45
            },
            {
                dishId: 1,
                quantity: 2,
                name: 'First burger',
                price: 65
            },
            {
                dishId: 2,
                quantity: 3,
                name: 'Second burger',
                price: 45
            },
            {
                dishId: 3,
                quantity: 10,
                name: 'First soup',
                price: 55
            },
            {
                dishId: 4,
                quantity: 200,
                name: 'Second soup',
                price: 70
            },
        ]
    },
    getters: {
        total(state) {
            return state.orders.reduce((total, order) =>
                total + order.price * order.quantity, 0);
        },
        itemsInCart(state) {
            return state.orders.length;
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
        }
    },
    actions: {
        async fetchDishTypes({commit}) {
            await fetch('http://localhost:5000/api/dishes/types')
                .then(response => response.json())
                .then(data => commit('updateDishTypes', data))
                .catch(error => console.log("Error: ", error.message));
        },
        async fetchDishes({commit, state}, {dishTypeId, currentPage}) {
            currentPage = currentPage ?? 1;
            const itemsPerPage = state.dishesPagingInfo.itemsPerPage;

            dishTypeId = dishTypeId ?? state.chosenDishTypeId;
            dishTypeId = dishTypeId === 0
                ? null
                : dishTypeId;
            commit('updateChosenDishTypeId', dishTypeId);

            const url = `http://localhost:5000/api/dishes/${itemsPerPage}/${currentPage}`
                + `/${dishTypeId ?? ''}`;

            fetch(url)
                .then(response => {
                    if (!response.ok) {
                        response = response.text();
                        throw new Error(`Http error, status: ${response.status}`)
                    }
                    return response.json()
                })
                .then(data => {
                    const dishesInfo = data;
                    commit('updateDishes', dishesInfo.dishes);
                    commit('updateDishesPagingInfo', dishesInfo.paginationInfo);
                })
                .catch(error => console.log('Error: ', error.message));
        }
    }
})
