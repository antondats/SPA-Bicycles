import React from 'react';
import { Button, Glyphicon } from 'react-bootstrap';

const BikeListItem = ({bike, onRemoveFromList, toggleDetails, onAddToFavorites}) => {
	return (
		<tr>
			<td>{bike.productId}</td>
			<td>
				<Button className="btn-link" onClick={() => toggleDetails(bike.productId)}>{bike.name}</Button>
			</td>
			<td>{bike.productNumber}</td>
			<td>{bike.color}</td>
			<td>{bike.listPrice}</td>
			<td>
				<Button bsStyle="danger" onClick={() => onRemoveFromList(bike.productId)}>Delete</Button>
			</td>
			<td>
				<Button onClick={() => onAddToFavorites(bike.productId)}>
					<Glyphicon glyph="heart" />
				</Button>
			</td>
		</tr>
	);
}

export default BikeListItem;
