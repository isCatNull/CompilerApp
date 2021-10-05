import React, { Component } from 'react';

export class Challenge extends Component {
    static displayName = Challenge.name;

    constructor(props) {
        super(props);
        this.state = {
            requestName: "",
            requestSourceCode: 'print("Hello World")',
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
                <h1>Challenge</h1>
                <br></br>
                <form onSubmit={this.handleSubmit}>
                    <div className="form-group row">
                        <label htmlFor="enterName" className="col-sm-2 col-form-label">NAME</label>
                        <div className="col-sm-10">
                            <input type="name" name="requestName" onChange={this.handleChange} id="enterName" />
                        </div>
                    </div>

                    <div className="form-group row">
                        <label htmlFor="selectTask" className="col-sm-2 col-form-label">SELECT TASK</label>
                        <div className="col-sm-10">
                            <select className="form-control" id="selectTask">
                                <option>1</option>
                                <option>2</option>
                                <option>3</option>
                                <option>4</option>
                                <option>5</option>
                            </select>
                        </div>
                    </div>

                    <div className="form-group row">
                        <label htmlFor="description" className="col-sm-2 col-form-label">DESCRIPTION</label>
                    </div>
                    <div className="form-group row">
                        <label htmlFor="userCode" className="col-sm-2 col-form-label">SOLUTION CODE</label>
                        <textarea className="col-sm-10" value={this.state.requestSourceCode} name="requestSourceCode" onChange={this.handleChange} id="userCode" rows="3"></textarea>
                    </div>
                    <button type="submit" className="btn btn-primary">Submit</button>
                </form>

                <div>
                    <label>output</label>
                    <textarea readOnly={true} value={this.state.responseOutput} ></textarea>
                </div>
            </div>
        );
    }
}
