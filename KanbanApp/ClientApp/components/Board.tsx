import * as React from 'react';

import { CardListComponent } from './CardList';

interface BoardState {
	lists: number[];

	newListTitle: string;
}

export class BoardComponent extends React.Component<{}, BoardState> {
	constructor() {
		super();
		
		this.handleNewListTitleChange = this.handleNewListTitleChange.bind(this);

		this.state = { lists: [], newListTitle: "" };

		fetch('api/cardlist')
			.then(response => response.json() as Promise<number[]>)
			.then(data => this.setState(prevState => ({lists: data})));
	}

	render() {
		return <div className='container-fluid board'>
			<div className='row'>
				{this.state.lists.map(id =>
				<div key={id} className='col-sm-2'>
					<div className='card'>
						<CardListComponent key={id} id={id}></CardListComponent>
						<button 
							onClick={() => this.removeList(id)} 
							type="button" 
							className='btn btn-danger'>Delete</button>
					</div>
				</div>
				)}	
				<div className='col-md-2'>
					<div className='input-group'>
						<input type="text" 
							className='form-control' 
							placeholder='Create new list...'
							value={this.state.newListTitle} 
							onChange={this.handleNewListTitleChange}></input>
						<span className='input-group-btn'>
							<button onClick={ () => this.createList() } 
								className='btn btn-secondary' 
								type='button'>
								<a className='glyphicon glyphicon-ok'></a>
							</button>
						</span>
					</div>
				</div>
			</div>
		</div>;
	}

	private handleNewListTitleChange(event: any) {
		this.setState({newListTitle: event.target.value});
	  }

	private createList() {
		fetch('api/cardlist', {
			method: 'POST',
			headers: {
			  'Accept': 'application/json',
			  'Content-Type': 'application/json',
			},
			body: JSON.stringify({
			  title: this.state.newListTitle
			})
		  })
			  .then(response => response.json())
			  .then(data => this.setState(prevState => ({
				  lists: prevState.lists.concat(data.id),
				  newListTitle: ""
			  })));
	}

	private removeList(id: number) {
		fetch(`api/card/${id}`, { method: 'DELETE' })
			.then(() => this.setState(prevState => ({
				lists: prevState.lists.filter(l => l !== id)
			})));
	}
}
