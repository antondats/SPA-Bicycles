import React from 'react';
import {Table} from 'react-bootstrap';

import BikeListItem from './BikeListItem';

const BikesList = ({bikes, onRemoveFromList, toggleDetails, onAddToFavorites}) => {

	return (
		<div>
			<Table striped bordered condensed hover>
				<thead>
					<tr>
						<th>#</th>
						<th>Name</th>
						<th>Number</th>
						<th>Color</th>
						<th>Price</th>
						<th>Delete</th>
						<th>Favorite</th>
					</tr>
				</thead>
				<tbody>
					{
						bikes.map((item, index) => <BikeListItem 
							key={index}
							bike={item}
							toggleDetails={toggleDetails}
							onRemoveFromList={onRemoveFromList}
							onAddToFavorites={onAddToFavorites}
						/>)
					}
				</tbody>
			</Table>
		</div>);
}

export default BikesList;
