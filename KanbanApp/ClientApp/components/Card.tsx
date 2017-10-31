import * as React from 'react';
import * as ReactDOM from 'react-dom';

interface CardProps {
	id: number;
}

interface CardState {
	title: string;
	description: string;

	isEditable: boolean;
}

interface Card {
	id: number;
	title: string;
	description: string;	
}

export class CardComponent extends React.Component<CardProps, CardState> {	
	constructor(props: CardProps) {
		super(props);
		
		this.handleCardChange = this.handleCardChange.bind(this);
		this.handleOutsideClick = this.handleOutsideClick.bind(this);

		this.state = {
			title: "",
			description: "",
		
			isEditable: false
		}

		fetch(`api/card/${this.props.id}`)
			.then(response => response.json() as Promise<Card>)
			.then(data => this.setState(prevState => ({
				title: data.title || "",
				description: data.description || "",
				isEditable: false
			})));
	}

	public render() {
		return this.state.isEditable 
		?
		<div className='card-edit'>
			<div className='form-group'>
				<label htmlFor='card-title-edit'>Title</label>
				<input type='text' className='form-control' id='card-title-edit' name='title' value={ this.state.title } onChange={ this.handleCardChange }></input> 
				<label htmlFor='card-description-edit'>Description</label>
				<input type='text' className='form-control' id='card-description-edit' name='description' value={ this.state.description } onChange={ this.handleCardChange }></input> 
  			</div>
		</div> 
		:
	 	<div className='card-preview'>
			<h4 className='card-title' onClick={() => this.openEdit()}>{ this.state.title }</h4>
			<p className="card-text">{ this.state.description }</p>
		</div>;
	}

	private openEdit() {
		this.setState(prevState => ({
			isEditable: true,
		}));

		document.addEventListener('click', this.handleOutsideClick, false);
	}

	private handleOutsideClick(event: any) {
		if (!ReactDOM.findDOMNode(this).contains(event.target)) {
			this.setState(prevState => ({
				isEditable: false
			}));

			fetch(`api/card/${this.props.id}`, {
				method: 'PUT',
				headers: {
				  'Accept': 'application/json',
				  'Content-Type': 'application/json',
				},
				body: JSON.stringify({
					id: this.props.id,
					title: this.state.title,
					description: this.state.description
				})
			  });
			
			document.removeEventListener('click', this.handleOutsideClick, false);
		}
	}

	private handleCardChange(event: any) {
		const target = event.target;
		const name = target.name;
	
		this.setState({
		  [name]: target.value
		});
	}
}