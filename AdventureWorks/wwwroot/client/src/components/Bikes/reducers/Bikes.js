import * as types from '../actions/ActionTypes';

const initialState = {
	bikes: []
};

function bikesReducer (state = initialState, action){
	switch(action.type) {
		case types.BIKES_LOADED: {
			return updateObject(state, {
				bikes: action.bikes
			});
		}
		case types.BIKE_DELETED: {
			return updateObject(state, {
					bikes: state.bikes.filter(b => b.productId !== action.id)
			});
		}
		default:
			return state;
	}
}

function updateObject(obj, newProperties){
	return Object.assign({}, obj, newProperties);
}

export default bikesReducer;