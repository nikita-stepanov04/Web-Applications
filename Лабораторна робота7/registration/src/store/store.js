import {createStore} from "vuex";

export default createStore({
    state: {
        newId: 1,
        registrants: [],
    },
    mutations: {
        incrementId(state) {
            state.newId++;
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
        },
        copyCheckedRegistrants(state) {
            state.registrants.forEach(reg => {
                if (reg.checked) {
                    reg.checked = false;
                    const newReg = Object.assign({}, reg);
                    newReg.id = state.newId++;
                    state.registrants.push(newReg);
                }
            });
        }
    }
})