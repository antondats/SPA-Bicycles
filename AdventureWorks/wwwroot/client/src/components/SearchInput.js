import React from 'react';

const SearchInput = ({onChange}) => {
	return (
		<input
			type="text"
			className="form-control"
			onChange={onChange}
			placeholder="Search input"
		/>
	);
}

export default SearchInput;
