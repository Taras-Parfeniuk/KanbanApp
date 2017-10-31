import * as React from 'react';
import * as ReactDOM from 'react-dom';

import { CardComponent } from './Card';

interface CardListProps {
	id: number;
}

interface CardListState {
	title: string;
	cards: number[];

	newCardTitle: string;
	isEditable: boolean;
}

interface CardList {
	title: string;
}

export class CardListComponent extends React.Component<CardListProps, CardListState> {	
	static dragSource: CardListComponent;

	constructor(props: CardListProps) {
		super(props);

		document.addEventListener("dragover", function( event ) {
			event.preventDefault();
		}, false);

		this.handleNewCardChange = this.handleNewCardChange.bind(this);
		this.handleListTitleChange = this.handleListTitleChange.bind(this);
		this.handleOutsideClick = this.handleOutsideClick.bind(this);
		this.dragStartHandler = this.dragStartHandler.bind(this);
		this.dropCardHandler = this.dropCardHandler.bind(this);
		
		this.state = {
			title: "",
			cards: [],
			newCardTitle: "",
			isEditable: false
		}

		fetch(`api/cardlist/${this.props.id}`)
			.then(response => response.json() as Promise<CardList>)
			.then(data => this.setState(prevState => ({
				title: data.title,
				cards: prevState.cards,
				isEditable: prevState.isEditable
			})));

		fetch(`api/cardlist/${this.props.id}/cards`)
			.then(response => response.json() as Promise<number[]>)
			.then(data => this.setState(prevState => ({
				title: prevState.title,
				cards: data,
				isEditable: prevState.isEditable
			})));
	}

	public render() {
		let title = this.state.isEditable 
		? <input className='form-control form-control-lg' type="text" value={this.state.title} onChange={this.handleListTitleChange}></input> 
		: <h4 className='card-title' onClick={() => this.openEdit()}>{ this.state.title }</h4>;

		return <div className='card-list' onDrop={this.dropCardHandler} > {title}
			{this.state.cards.map(id => <div draggable onDragStart={ event => this.dragStartHandler(id, event) } 			
				className='card' key={id}>
			<div className='card-body'>
				<CardComponent id={id}></CardComponent>
				<a href='#' className='card-link' onClick={() => this.removeCard(id)}>Delete</a>
			</div>
			</div>
			)}
			<div className='input-group'>
				<input type="text" 
					className='form-control' 
					placeholder='Create new card...'
					value={this.state.newCardTitle} 
					onChange={this.handleNewCardChange}></input>
				<span className='input-group-btn'>
					<button onClick={() => this.createCard()} 
						className='btn btn-secondary' 
						type='button'>
						<a className='glyphicon glyphicon-ok'></a>
					</button>
				</span>
    		</div>
		</div>;
	}

	private openEdit() {
		this.setState(prevState => ({
			isEditable: true
		}));

		document.addEventListener('click', this.handleOutsideClick, false);
	}

	private handleOutsideClick(event: any) {
		if (!ReactDOM.findDOMNode(this).contains(event.target)) {
			this.setState(prevState => ({
				isEditable: false
			}));

			fetch(`api/cardlist/${this.props.id}`, {
				method: 'PUT',
				headers: {
				  'Accept': 'application/json',
				  'Content-Type': 'application/json',
				},
				body: JSON.stringify({
					id: this.props.id,
					title: this.state.title
				})
			  });
			
			document.removeEventListener('click', this.handleOutsideClick, false);
		}
	}

	private handleListTitleChange(event: any) {
		this.setState({title: event.target.value});
	}

	private handleNewCardChange(event: any) {
		this.setState({newCardTitle: event.target.value});
	  }

	private createCard() {
		fetch('api/card', {
			method: 'POST',
			headers: {
			  'Accept': 'application/json',
			  'Content-Type': 'application/json',
			},
			body: JSON.stringify({
			  title: this.state.newCardTitle,
			  listId: this.props.id
			})})
			  .then(response => response.json())
			  .then(data => this.setState(prevState => ({
				  cards: prevState.cards.concat(data.id),
				  newCardTitle: ""
			  })));
	}

	private removeCard(id: number) {
		fetch(`api/card/${id}`, { method: 'DELETE' })
			.then(() => this.setState(prevState => ({
				cards: prevState.cards.filter(c => c !== id)
			})));
	}

	private dragStartHandler(cardId: number, event: any) {
		CardListComponent.dragSource = this;

		event.dataTransfer.setData("text/plain", cardId);
	}

	private dropCardHandler(event: any) {
		if (event.currentTarget.className === "card-list") {
			event.currentTarget.style.border = "";
			
			var data = +event.dataTransfer.getData("text");
			
			CardListComponent.dragSource.setState(prevState => ({
				cards: prevState.cards.filter(c => c !== data)
			}));
			
			this.setState(prevState => ({cards: prevState.cards.concat(data)}));		
			
			event.dataTransfer.clearData();

			fetch(`api/card/${this.props.id}/move`, {
				method: 'PUT',
				headers: {
				  'Accept': 'application/json',
				  'Content-Type': 'application/json',
				},
				body: JSON.stringify({
					id: data,
					listId: this.props.id
				})
			});
		}
	}
}