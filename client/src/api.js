import axios from "axios";

// CHANGE THIS PORT TO MATCH YOUR BACKEND TERMINAL!
// const API_URL = "http://localhost:5087/api/items";
const API_URL = "https://azure-cicd-poc-backend-b0b2enarf6bxfdga.southindia-01.azurewebsites.net/";

export const getItems = () => axios.get(API_URL);
export const createItem = (item) => axios.post(API_URL, item);
export const deleteItem = (id) => axios.delete(`${API_URL}/${id}`);
