import React, { Component } from 'react';

export class Challenge extends Component {
    static displayName = Challenge.name;

    constructor(props) {
        super(props);
        this.state = { name: "", sourceCode: "" };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }


    handleChange(event) {
        this.setState({ [event.target.name]: event.target.value });
    }

    async handleSubmit(event) {
        event.preventDefault();
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(this.state)
        };
        const response = await fetch('challenge/submitTask', requestOptions);
    }

    render() {
        return (
            <form onSubmit={ this.handleSubmit }>
                <div class="form-group row">
                    <label for="enterName" class="col-sm-2 col-form-label">NAME</label>
                    <div class="col-sm-10">
                        <input type="name" name="name" onChange={this.handleChange} id="enterName" />
                    </div>
                </div>

                <div class="form-group row">
                    <label for="selectTask" class="col-sm-2 col-form-label">SELECT TASK</label>
                    <div class="col-sm-10">
                        <select class="form-control" id="selectTask">
                            <option>1</option>
                            <option>2</option>
                            <option>3</option>
                            <option>4</option>
                            <option>5</option>
                        </select>
                    </div>
                </div>

                <div class="form-group row">
                    <label for="description" class="col-sm-2 col-form-label">DESCRIPTION</label>
                </div>
                <div class="form-group row">
                    <label for="userCode" class="col-sm-2 col-form-label">SOLUTION CODE</label>
                    <textarea class="col-sm-10" name="sourceCode" onChange={this.handleChange} id="userCode" rows="3"></textarea>
                </div>

                <button type="submit" class="btn btn-primary">Submit</button>
            </form>
        );
    }
}
