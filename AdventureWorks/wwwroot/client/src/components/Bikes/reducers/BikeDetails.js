import * as types from '../actions/ActionTypes';

const initialState = {
	detailes: {}
};

function detailesReducer (state = initialState, action){
	switch(action.type) {
		case types.BIKES_DETAILES_LOADED: {
			return updateObject(state, {
				detailes: action.detailes
			});
		}
		case types.CLEAR_DETAILES: {
			return updateObject(state,{
				detailes: {}
			})
		}
		default:
			return state;
	}
}

function updateObject(obj, newProperties){
	return Object.assign({}, obj, newProperties);
}

export default detailesReducer;