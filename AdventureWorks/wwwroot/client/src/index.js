import React from 'react';
import ReactDOM from 'react-dom';
import App from './layouts/App';
import {Provider} from 'react-redux';
import store from './store';

ReactDOM.render(
	<Provider store={store}>
		<App />
	</Provider>, 
	document.getElementById('root')
);

module.hot.accept();