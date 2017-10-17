import React from 'react'

const BikeDetail = ({bike}) => {
	return (
		<div>
			<div>
				<label>ID: </label>
				<div>
					{bike.productId}
				</div>
			</div>
			<div>
				<label>Name: </label>
				<div>
					{bike.name}
				</div>
			</div>
			<div>
				<label>Price: </label>
				<div>
					{bike.listPrice}
				</div>
			</div>
			<div>
				<label>Number: </label>
				<div>
					{bike.productNumber}
				</div>
			</div>
			<div>
				<label>Weight: </label>
				<div>
					{bike.weight}
				</div>
			</div>
			<div>
				<label>Size: </label>
				<div>
					{bike.size}
				</div>
			</div>
			<div>
				<label>Description: </label>
				<div>
					{bike.description}
				</div>
			</div>
			<div>
				<label>Model: </label>
				<div>
					{bike.model}
				</div>
			</div>
			<div>
				<label>Color: </label>
				<div>
					{bike.color}
				</div>
			</div>
			<div>
				<label>Category: </label>
				<div>
					{bike.category}
				</div>
			</div>
		</div>
	)
}

export default BikeDetail;