import {applyMiddleware, combineReducers, createStore} from 'redux';
import thunk from 'redux-thunk';
import {createLogger} from 'redux-logger';

import bikesReducer from './components/Bikes/reducers/Bikes';
import favoritesReducer from './components/Bikes/reducers/Favorites';
import detailesReducer from './components/Bikes/reducers/BikeDetails';

const middleware = [thunk, createLogger()];

const store = createStore(
	combineReducers({
		bikesReducer: bikesReducer,
		favoritesReducer: favoritesReducer,
		detailesReducer: detailesReducer
	}),
	applyMiddleware(...middleware)
);

export default store;