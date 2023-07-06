import React, { Component } from 'react';

export class RankItems extends Component {
    static displayName = RankItems.name;

    constructor(props) {
        super(props);
        this.state = {
            items: [],
            loading: true,
            modalTitle: "",
            DepartmentName: "",
            DepartmentId:0
        };
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    addClick() {
        this.setState({
            modalTitle: "Add Department",
            DepartmentId: 0,
            DepartmentName:""
        })
    }
    editClick(dep) {
        this.setState({
            modalTitle: "Edit Department",
            DepartmentId: dep.DepartmentId,
            DepartmentName: dep.DepartmentName
        })
    }
    changeDepartmentName = (e) => {
        this.setState({ DepartmentName: e.target.value });
    }

    static renderForecastsTable(items) {
        return (

               
                <table>
                    <thead>
                        <th>
                            DepartmentId
                        </th>
                        <th>
                            DepartmentName
                        </th>
                        <th>
                            Options
                        </th>
                    </thead>
                    <tbody>
                        {items.map(item =>
                               
                                    <tr key={item.Id}>
                                        <td>{item.id}</td>
                                        <td>{item.departmentName}</td>
                                        <td>
                                    <button type="button" className="btn btn-light mr-1"
                                        data-bs-toggle="modal"
                                        data-bs-target="#exampleModal"
                                        onClick={() => this.editClick(item)}>
                                        edit</button>
                                            <button type="button" className="btn btn-light mr-1">del</button>
                                        </td>
                                    </tr>    
                        )}
                    </tbody>

                </table>
                

        );
    }

    render() {
        const {
            items,
            modalTitle,
            DepartmentId,
            DepartmentName
        }=this.state
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : RankItems.renderForecastsTable(this.state.items);

        return (
            <div>
                <button type="button"
                    className="btn btn-primary m-2 float-end"
                    data-bs-toggle="modal"
                    data-bs-target="#exampleModal"
                    onClick={() => this.addClick()}>
                    Add Department
                </button>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
                <div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
                    <div className="modal-dialog modal-lg modal-dialog-centered">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title">{modalTitle}</h5>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"
                                ></button>
                            </div>

                            <div className="modal-body">
                                <div className="input-group mb-3">
                                    <span className="input-group-text">DepartmentName</span>
                                    <input type="text" className="form-control"
                                        value={DepartmentName}
                                        onChange={this.changeDepartmentName} />
                                </div>

                                {DepartmentId == 0 ?
                                    <button type="button"
                                        className="btn btn-primary float-start"
                                        onClick={() => this.createClick()}
                                    >Create</button>
                                    : null}

                                {DepartmentId != 0 ?
                                    <button type="button"
                                        className="btn btn-primary float-start"
                                        
                                    >Update</button>
                                    : null}

                            </div>

                        </div>
                    </div>
                </div>

            </div>
        );
    }

    async populateWeatherData() {
        const response = await fetch('department');
        const data = await response.json();
        this.setState({ items: data, loading: false });
    }
}
