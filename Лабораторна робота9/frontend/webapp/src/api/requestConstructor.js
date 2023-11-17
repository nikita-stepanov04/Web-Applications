import axios from "axios";

export default {
    url: 'http://localhost:5000/api/',
    async get(path, params) {
        return axios.get(this.url + path, {
            params: params
        });
    }
}