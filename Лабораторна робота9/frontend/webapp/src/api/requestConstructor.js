import axios from "axios";

export default {
    url: 'http://localhost:5000/api/',
    async get(path, params, withCredentials) {
        return axios.get(this.url + path, {
            params: params,
            withCredentials: withCredentials
        });
    },
    async post(path, body, withCredentials) {
        return axios.post(this.url + path, body, {
            withCredentials: withCredentials
        })
    },
    async put(path, body, withCredentials) {
        return axios.put(this.url + path, body, {
            withCredentials: withCredentials
        })
    },
    async delete(path, data, withCredentials) {
        await axios.delete(this.url + path, {
            data: data,
            withCredentials: withCredentials
        });
    },
    async patch(path, updateData, withCredentials) {
        await axios.patch(this.url + path, updateData, {
            withCredentials: withCredentials
        });
    }
}