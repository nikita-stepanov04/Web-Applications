import {createStore} from "vuex";

export default createStore({
    state: {
        lastId: 1,
        registrants: [],
        registrantsTableVisibility: false
    },
    mutations: {
        incrementId(state) {
            state.lastId++;
        },
        setTableVisibility(state, exp) {
            state.registrantsTableVisibility = exp;
            console.log(state);
        },
        addRegistrant(state, registrant) {
            state.registrants.push(registrant);
        },
        removeCheckedRegistrants(state) {
            state.registrants.forEach(reg => {
                if (reg.checked) {
                    state.registrants = state.registrants.filter(r => r !== reg);
                }
            });
            if (state.registrants.length === 0)
                state.registrantsTableVisibility = false;
        },
        copyCheckedRegistrants(state) {
            state.registrants.forEach(reg => {
                if (reg.checked) {
                    reg.checked = false;
                    const newReg = Object.assign({}, reg);
                    newReg.id = state.lastId++;
                    state.registrants.push(newReg);
                }
            });
        }
    }
})