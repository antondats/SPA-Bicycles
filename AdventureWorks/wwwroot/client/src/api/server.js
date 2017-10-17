import axios from 'axios';

const GET_TOP_FIVE_URL = `http://localhost:4448/home/bikes`;
const GET_SEARCH_BIKES_URL = `http://localhost:4448/home/bikes/`;
const GET_BIKE_DETAILS = `http://localhost:4448/home/bikes/details/`;
const DELETE_BIKE = `http://localhost:4448/home/bikes/`;
const CREATE_BIKE_URL = `http://localhost:4448/home/bikes`;


export const loadTopFiveBikesRequest = () => {
    return axios.get(GET_TOP_FIVE_URL)
        .then(response => {
            return response.data
    })
};

export const searchBikesRequest = (string) => {
    return axios.get(GET_SEARCH_BIKES_URL + string)
        .then(response => {
            return response.data
    })
};

export const showBikeDetailsRequest = (id) => {
    return axios.get(GET_BIKE_DETAILS + id)
        .then(response => {
            return response.data
    })
};

export const deleteBikeRequest = (id) => {
    return axios.delete(DELETE_BIKE + id)
        .then(response => {
            if(response.data == true)
                return response.data;
            else
                throw "Error";
        })
};

export const createBikeRequest = (bike) => {
    return axios.post(CREATE_BIKE_URL, bike)
        .then(response => {
            if(response.data == true)
                return response.data;
            else
                throw "Error";
    })
};
