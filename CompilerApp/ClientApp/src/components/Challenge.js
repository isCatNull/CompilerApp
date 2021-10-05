import React, { Component } from 'react';

export class Challenge extends Component {
    static displayName = Challenge.name;

    constructor(props) {
        super(props);
        this.state = {
            requestName: "",
            requestSourceCode: `print("Hello World")`,
            responseOutput: "",
            responseStatusCode: "",
            responseMemory: "",
            responseCpuTime: "",
            responseError: ""
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        this.setState({
            [event.target.name]: event.target.value
        });
    }

    async handleSubmit(event) {
        event.preventDefault();

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                "Name": this.state.requestName,
                "SourceCode": this.state.requestSourceCode
            })
        };

        const response = await fetch('challenge/submitTask', requestOptions);
        const responseData = await response.json();

        this.setState({
            responseOutput: responseData.output,
            responseStatusCode: responseData.statusCode,
            responseMemory: responseData.memory,
            responseCpuTime: responseData.cpuTime,
            responseError: responseData.error
        });
    }

    render() {
        return (
            <div>
                <h1>Coding challenge</h1>
                <br></br>
                <form onSubmit={this.handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="requestName">Name</label>
                        <input type="name" name="requestName" className="form-control" onChange={this.handleChange} id="requestName" />
                    </div>

                    <div className="form-group">
                        <label htmlFor="task">Task</label>
                        <select className="form-control" id="task">
                            <option>1</option>
                            <option>2</option>
                            <option>3</option>
                            <option>4</option>
                            <option>5</option>
                        </select>
                    </div>

                    <div className="form-group">
                        <label htmlFor="description">Description</label>
                        <p id="description" className="text-info" readOnly={true}>{'Please enter Python script'}</p>
                    </div>

                    <div className="form-group">
                        <label htmlFor="requestSourceCode">Solution code</label>
                        <textarea className="form-control" value={this.state.requestSourceCode} name="requestSourceCode" onChange={this.handleChange} id="requestSourceCode"></textarea>
                    </div>

                    <button type="submit" className="btn btn-primary">Submit</button>
                </form>

                <br></br>

                <div className="form-group">
                    <label htmlFor="responseOutput">Output</label>
                    <textarea className="form-control" id="responseOutput" readOnly={true} value={this.state.responseOutput} />
                </div>
            </div>
        );
    }
}
