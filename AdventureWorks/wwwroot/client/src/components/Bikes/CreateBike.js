import React from 'react'
import { Table } from 'react-bootstrap';

const CreateBike = ({onCreateChange}) => {
	return (
		<div>
			<div>
				<label>Name: </label>
				<input type="text" name="name" className="form-control"
							onChange={onCreateChange} />
			</div>
			<div>
				<label>Price: </label>
				<input type="text" name="listPrice" className="form-control"
							onChange={onCreateChange} />
			</div>
			<div>
				<label>Number: </label>
				<input type="text" name="productNumber" className="form-control"
							onChange={onCreateChange} />
			</div>
			<div>
				<label>Weight: </label>
				<input type="text" name="weight" className="form-control"
							onChange={onCreateChange} />
			</div>
			<div>
				<label>Size: </label>
				<input type="text" name="size" className="form-control"
							onChange={onCreateChange} />
			</div>
			<div>
				<label>Description: </label>
				<textarea name="description" rows="9" cols="40" maxLength="400" className="form-control"
							onChange={onCreateChange}></textarea>
			</div>
			<div>
				<label>Model: </label>
				<input type="text" name="model" className="form-control"
							onChange={onCreateChange} />
			</div>
			<div>
				<label>Color: </label>
				<input type="text" name="color" className="form-control"
							onChange={onCreateChange} />
			</div>
			<div>
				<label>Category: </label>
					<select name="categoryId" onChange={onCreateChange} className="form-control">
						<option selected value="1">Mountain Bikes</option>
						<option value="2">Road Bikes</option>
						<option value="3">Touring Bikes</option>
					</select>
			</div>
		</div>
	);
}

export default CreateBike;