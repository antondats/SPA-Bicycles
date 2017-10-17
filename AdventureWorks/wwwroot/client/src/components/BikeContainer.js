import React, { Component } from 'react';
import Modal from 'react-modal';
import {Table, Button, ButtonGroup, Grid, Col, Row} from 'react-bootstrap';
import {NotificationContainer, NotificationManager} from 'react-notifications';
import {connect} from 'react-redux';

import BikesList from './Bikes/BikesList';
import SearchInput from './SearchInput';
import BikeDetail from './Bikes/BikeDetail';
import CreateBike from './Bikes/CreateBike';
import FavoritesList from './Bikes/FavoritesList';

import {loadTopFive, searchAllBikes, addToFavorites, removeFavorites, loadBikeDetailes, clearDetailes, deleteBike, createBike} from './Bikes/actions/Bikes';
import {loadTopFiveBikesRequest, searchBikesRequest, showBikeDetailsRequest, deleteBikeRequest, createBikeRequest} from "../api/server"; 


class BikeContainer extends Component {
	constructor(props) {
		super(props);

		this.state = {
			showDetail: false,
			showCreate: false,
			showFavorites: false,
			searchString: null,

			//Model for new bike
			productId: 0,
			name: null,
			color: null,
			productNumber: null,
			weight: null,
			size: null,
			listPrice: 0,
			description: null,
			model: null,
			categoryId: 1
		}

		this.onRemoveFromList = this.onRemoveFromList.bind(this);
		this.onSearchInputChange = this.onSearchInputChange.bind(this);
		this.toggleDetails = this.toggleDetails.bind(this);
		this.toggleCreateForm = this.toggleCreateForm.bind(this);
		this.onCreateChange = this.onCreateChange.bind(this);
		this.onCreateNewBike = this.onCreateNewBike.bind(this);
		this.createNewBike = this.createNewBike.bind(this);
		this.onAddToFavorites = this.onAddToFavorites.bind(this);
		this.toggleFavorites = this.toggleFavorites.bind(this);
		this.onRemoveFavorites = this.onRemoveFavorites.bind(this);
		this.onClickSearchInput = this.onClickSearchInput.bind(this);
	}

	componentDidMount() {
		this.props.loadTopFive();
	}

	onRemoveFromList(id) {
		this.props.deleteBike(id);
	}

	onClickSearchInput()
	{
		if(!this.state.searchString || this.state.searchString <= 3)
			this.props.loadTopFive();
		else
			this.props.searchAllBikes(this.state.searchString);
	}

	onSearchInputChange(event) {
		this.setState({
			searchString: event.target.value
		});
	}

	onCreateChange(event){
		this.setState({
			[event.target.name]: event.target.value
		});
	}

	createNewBike(bike){
		this.props.createBike(bike);
		this.setState(
			{ 
				showCreate: !this.state.showCreate
			});
	}

	onCreateNewBike(){
		let bike = {
			ProductId: this.state.productId,
			Name: this.state.name,
			Color: this.state.color,
			ProductNumber: this.state.productNumber,
			Weight: this.state.weight,
			Size: this.state.size,
			ListPrice: this.state.listPrice,
			Description: this.state.description,
			Model: this.state.model,
			CategoryId: this.state.categoryId
		};
		this.createNewBike(bike);
	}

	toggleCreateForm()
	{
		this.setState({
			showCreate: !this.state.showCreate,
			name: null,
			color: null,
			productNumber: null,
			weight: null,
			size: null,
			listPrice: 0,
			description: null,
			model: null,
			categoryId: 1
		});
	}

	toggleDetails(id) {
		if(this.state.showDetail == true)
		{
			this.setState({
				showDetail: !this.state.showDetail
			});
			this.props.clearDetailes();
			return;
		}
		this.props.loadBikeDetailes(id);
		this.setState({
					showDetail: !this.state.showDetail,
				});
	}

	toggleFavorites(){
		this.setState({
			showFavorites: !this.state.showFavorites
		});
	}

	onAddToFavorites(id){
		this.props.addToFavorites(this.props.bikes.find(bike => bike.productId === id ));
	}

	onRemoveFavorites(id){
		this.props.removeFavorites(id);
	}


	render() {
		const { searchResult, bikes, showDetail, bikeDetail, showCreate, showFavorites } = this.state;
		return (
			<div>
				<NotificationContainer/>
				<Grid>
					<Row>
						<Col md={6}>
							<SearchInput
								onChange={this.onSearchInputChange}
							/>
						</Col>
						<Col md={6}>
							<ButtonGroup>
								<Button bsStyle='primary' onClick={this.onClickSearchInput}>Search</Button>
								<Button bsStyle="success" onClick={this.toggleCreateForm}>Create new</Button>
								<Button bsStyle='warning' onClick={this.toggleFavorites}>Favorites</Button>
							</ButtonGroup>
						</Col>
					</Row>
				</Grid>
				<BikesList
					bikes={this.props.bikes}
					onRemoveFromList={this.onRemoveFromList}
					toggleDetails={this.toggleDetails}
					onAddToFavorites={this.onAddToFavorites}
				/>
				<Modal 
					isOpen={showDetail}
					onRequestClose={this.toggleDetails}
					contentLabel="Modal"
					style={{
						content : {
						width: 400,
						height: 600,
						margin: '0 auto',
						}
					}}
				>
					<BikeDetail 
						bike={this.props.detailes}
					/>
					<Button bsStyle="danger" onClick={this.toggleDetails}>Close</Button>
				</Modal>
				<Modal 
					isOpen={showCreate}
					onRequestClose={this.toggleCreateForm}
					contentLabel="Modal"
					style={{
						content : {
							width: 400,
							height: 600,
							margin: '0 auto',
						}
					}}
				>
					<CreateBike 
						onCreateChange={this.onCreateChange}
					/>			
					<ButtonGroup>
						<Button bsStyle="danger" onClick={this.toggleCreateForm}>Close</Button>
						<Button bsStyle="success" onClick={this.onCreateNewBike}>Add</Button>
					</ButtonGroup>
				</Modal>
				<Modal
					isOpen={showFavorites}
					onRequestClose={this.toggleFavorites}
					contentLabel="Modal"
					style={{
						content:{
							width: 500,
							height: 400,
							margin: '0 auto',
						}
					}}
				>
					<FavoritesList
						bikes={this.props.favorites}
						onRemoveFavorites={this.onRemoveFavorites}
					/>
					<Button bsStyle='danger' onClick={this.toggleFavorites}>Close</Button>
				</Modal>
			</div>
		);
	}
}

const mapStateToProps = (state, ownProps) => {
	return{
		bikes: state.bikesReducer.bikes,
		favorites: state.favoritesReducer.favorites,
		detailes: state.detailesReducer.detailes
	}
}

const mapDispatchToProps = (dispatch, ownProps) => {
	return {
		loadTopFive: () => dispatch(loadTopFive()),
		searchAllBikes: (string) => dispatch(searchAllBikes(string)),
		addToFavorites: (bike) => dispatch(addToFavorites(bike)),
		removeFavorites: (id) => dispatch(removeFavorites(id)),
		loadBikeDetailes: (id) => dispatch(loadBikeDetailes(id)),
		clearDetailes:  () => dispatch(clearDetailes()),
		deleteBike: (id) => dispatch(deleteBike(id)),
		createBike: (bike) => dispatch(createBike(bike))
	}
}

export default connect(mapStateToProps, mapDispatchToProps)(BikeContainer);
