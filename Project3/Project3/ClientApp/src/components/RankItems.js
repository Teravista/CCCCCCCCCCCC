import React, { Component, useState } from 'react';
import PopUp from './PopUp';
export class RankItems extends Component {
    static displayName = RankItems.name;

    constructor(props) {
        super(props);
        this.state = {
            items: [],
            loading: true,
            modalTitle: "",
            DepartmentName: "",
            DepartmentId: 0,
            buttonPopup:false
        };
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    refreshList() {
            
        fetch('department')
            .then(response => response.json())
            .then(data => {
                this.setState({ items: data });
            });
        
    }

    addClick() {
        this.setState({
            buttonPopup:true,
            modalTitle: "Add Department",
            DepartmentId: 0,
            DepartmentName:""
        })
    }

    editClick(dep) {
        this.setState({
            buttonPopup: true,
            modalTitle: "Edit Department",
            DepartmentId: dep.id,
            DepartmentName: dep.departmentName
        })
    }


    createClick() {
        fetch('department', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                departmentName: this.state.DepartmentName,
            })
        })
            .then(res => res.json())
            .then((result) => {
                alert(result);
                this.refreshList();
            }, (error) => {
                alert('Failed');
            })
    }
    updateClick() {
        fetch('department/' + this.state.DepartmentId, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                departmentName: this.state.DepartmentName,
            })
        })
            .then(res => res.json())
            .then((result) => {
                alert(result);
                this.refreshList();
            }, (error) => {
                alert('Failed');
                this.refreshList();
            })
    }
    deleteClick(item) {
        if (window.confirm('Are you sure?')) {

            fetch('department/' + item.id, {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            })
                .then(res => res.json())
                .then((result) => {
                    alert(result);
                    this.refreshList();
                }, (error) => {
                    alert('Failed');
                    this.refreshList();
                })
        }
    }
    changeDepartmentName = (e) => {
        this.setState({ DepartmentName: e.target.value });
    }



    render() {
        const {
            items,
            modalTitle,
            DepartmentId,
            DepartmentName
        }=this.state


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

                            <tr key={item.id}>
                                <td>{item.id}</td>
                                <td>{item.departmentName}</td>
                                <td>
                                    <button type="button" className="btn btn-light mr-1"
                                        data-bs-toggle="modal"
                                        data-bs-target="#exampleModal"
                                        onClick={() => this.editClick(item)}>
                                        edit
                                    </button>
                                    <button type="button" className="btn btn-light mr-1"
                                        onClick={() => this.deleteClick(item)}>
                                        del
                                    </button>
                                </td>
                            </tr>
                        )}
                    </tbody>

                </table>
                <PopUp trigger={this.state.buttonPopup} setTrigger={this }>
                    <div className="input-group mb-3">
                        <span className="input-group-text">DepartmentName</span>
                        <input type="text" className="form-control"
                            value={DepartmentName}
                            onChange={this.changeDepartmentName} />
                    </div>
                    <div>
                        <select>
                            <option>name</option>
                            <option>laplaya</option>
                            <option>perya</option>
                        </select>
                    </div>
                    <div>
                        <input type="date"></input>
                    </div>
                    {DepartmentId === 0 ?
                        <button type="button"
                            className="btn btn-primary float-start"
                            onClick={() => this.createClick()}
                        >Create</button>
                        : null}

                    {DepartmentId !== 0 ?
                        <button type="button"
                            className="btn btn-primary float-start"
                            onClick={() => this.updateClick()}
                        >Update</button>
                        : null}
                </PopUp>
                
                                

                            

            </div>
        );
    }

    async populateWeatherData() {
        const response = await fetch('department');
        const data = await response.json();
        this.setState({ items: data, loading: false });
    }
}
