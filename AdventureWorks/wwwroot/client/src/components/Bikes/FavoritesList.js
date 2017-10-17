import React from 'react';
import {Table, Button, Glyphicon} from 'react-bootstrap';

const FavoritesList = ({bikes, onRemoveFavorites}) => {
	return (
		<div>
			<Table striped bordered condensed hover>
				<thead>
					<tr>
						<th>Name</th>
						<th>Number</th>
						<th>Color</th>
						<th>Price</th>
						<th>Remove</th>
					</tr>
				</thead>
				<tbody>
					{
						bikes.map((item, index) => 
							<tr key={index}>
								<td>{item.name}</td>
								<td>{item.productNumber}</td>
								<td>{item.color}</td>
								<td>{item.listPrice}</td>
								<td>
									<Button bsStyle='danger' onClick={() => onRemoveFavorites(item.productId)}>Remove</Button>
								</td>
							</tr>
						)
					}
				</tbody>
			</Table>
		</div>
	)
}

export default FavoritesList;