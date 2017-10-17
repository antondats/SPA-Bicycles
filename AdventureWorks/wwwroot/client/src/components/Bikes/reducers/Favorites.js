import * as types from '../actions/ActionTypes';

const initialState = {
	favorites: []
};

function favoritesReducer (state = initialState, action){
	switch(action.type) {
		case types.ADDED_TO_FAVORITES: {
			if(!state.favorites.find(b => b.productId === action.bike.productId))
				return updateObject(state,{
					favorites: state.favorites.concat([action.bike])
				});
		}
		case types.REMOVE_FROM_FAVORITES:{
			return updateObject(state, {
					favorites: state.favorites.filter(b => b.productId !== action.id)
				});
		}
		default:
			return state;
	}
}

function updateObject(obj, newProperties){
	return Object.assign({}, obj, newProperties);
}

export default favoritesReducer;