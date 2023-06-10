import axios from "axios";

export const axiosInstance = axios.create({
	baseURL: import.meta.env.VITE_BASE_URL,
});

export const url = {
	signUp: "user/signup",
};

export const axiosGet = async (url, config = {}, instance = axiosInstance) => {
	const response = await instance.get(url, config);
	return response.data;
};

export const axiosPost = async (url, data = {}, config = {}, instance = axiosInstance) => {
	const response = await instance.post(url, data, config);
	return response.data;
};
