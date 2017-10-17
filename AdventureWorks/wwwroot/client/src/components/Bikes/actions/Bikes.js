import * as types from './ActionTypes';
import {NotificationContainer, NotificationManager} from 'react-notifications';

import {loadTopFiveBikesRequest, searchBikesRequest, showBikeDetailsRequest, deleteBikeRequest, createBikeRequest} from '../../../api/server';

const ERROR_CREATING_MSG = "Error, check your fields";
const ERROR_DELETING_MSG = "Error. Bike wasn't deleted";
const SUCCESS_CREATING_MSG = "Bike was created";
const SUCCESS_DELETING_MSG = "Bike was deleted";

export const allBikesLoaded = (bikes) => {
	return {
		type: types.BIKES_LOADED,
		bikes
	}
}

export const detailesLoaded = (detailes) => {
	return {
		type: types.BIKES_DETAILES_LOADED,
		detailes
	}
}

export const addToFavorites = (bike) => {
	return {
		type: types.ADDED_TO_FAVORITES,
		bike
	}
};

export const removeFavorites = (id) => {
	return {
		type: types.REMOVE_FROM_FAVORITES,
		id
	}
}

export const clearDetailes = () => {
	return {
		type: types.CLEAR_DETAILES
	}
}

export const bikeDeleted = (id) => {
	return {
		type: types.BIKE_DELETED,
		id
	}
}

export const bikeCreated = () => {
	return {
		type: types.BIKE_CREATED,
	}
}

export const loadTopFive = () => {
	return (dispatch) => {
		loadTopFiveBikesRequest()
			.then(bikes => dispatch(allBikesLoaded(bikes)))
			.catch(e => console.error(e))
	}
};

export const searchAllBikes = (string) => {
	return (dispatch) => {
		searchBikesRequest(string)
			.then(bikes => dispatch(allBikesLoaded(bikes)))
			.catch(e => console.error(e))
	}
};

export const loadBikeDetailes = (id) => {
	return (dispatch) => {
		showBikeDetailsRequest(id)
			.then(detailes => dispatch(detailesLoaded(detailes)))
			.catch(e => console.error(e))
	}
};

export const deleteBike = (id) => {
	return (dispatch) => {
		deleteBikeRequest(id)
			.then((result) => {
				dispatch(bikeDeleted(id));
				dispatch(removeFavorites(id));
				NotificationManager.success('Deleting', SUCCESS_DELETING_MSG);
			})
			.catch((e) => {
				console.error(e);
				NotificationManager.error('error', 'Deleting', ERROR_DELETING_MSG);
			});
	}
}

export const createBike = (bike) => {
	return (dispatch) => {
		createBikeRequest(bike)
			.then((result) => {
				dispatch(bikeCreated());
				NotificationManager.success('Creating',  SUCCESS_CREATING_MSG);
			})
			.catch((error) => { 
				NotificationManager.error('Creating',  ERROR_CREATING_MSG);
				console.error(error);
			});
	}
}