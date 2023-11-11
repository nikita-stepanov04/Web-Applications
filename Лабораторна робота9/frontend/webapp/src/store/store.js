import {createStore} from "vuex";

export default createStore({
    state: {
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
        }
    }
})
