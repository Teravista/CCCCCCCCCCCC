import React, { Component } from 'react';
import PopUp from './PopUp';
import { addClick, editClick, createClick, updateClick, deleteClick }  from './ContactsFunctions'
export class ContactsList extends Component {
    static displayName = ContactsList.name;

    constructor(props) {
        super(props);
        this.state = {
            Contacts: [],
            Categories: [],
            SubCategories: [],
            Id: 0,
            loading: true,
            Name: "",
            Surname: "",
            Email: "",
            Category: "",
            SubCategory: "",
            Other: "",
            PhoneNumber: 0,
            Date:"",
            buttonPopup: false,
        };
    }
    //method to get initial data
    componentDidMount() {
        this.populateData();
    }
    // change... methods to update variable state after user input
    changeName = (e) => {
        this.setState({ Name: e.target.value });
    }
    changeSurname = (e) => {
        this.setState({ Surname: e.target.value });
    }
    changeEmail = (e) => {
        this.setState({ Email: e.target.value });
    }

    changeCategory = (e) => {
        this.setState({ Category: e.target.value });
    }
    changeSubCategory = (e) => {
        this.setState({ SubCategory: e.target.value });
    }
    changeOther = (e) => {
        this.setState({ Other: e.target.value });
    }

    changePhoneNumber = (e) => {
        this.setState({ PhoneNumber: e.target.value });
    }

    changeDate = (e) => {
        this.setState({ Date: e.target.value });
    }

    render() {
        const {
            Id,
            Contacts,
            Name,
            Surname,
            Email,
            Category,
            SubCategory,
            Other,
            PhoneNumber,
            Date,
            Categories,
            SubCategories,
        }=this.state

        return (
            <div>
                <button type="button"
                    disabled={!window.$logged}
                    className="btn btn-primary m-2 float-end"
                    data-bs-toggle="modal"
                    data-bs-target="#exampleModal"
                    onClick={() => addClick(this)}>
                    Add Department
                </button>
                
                <table>
                    <thead>
                        <th>
                            Name
                        </th>
                        <th>
                            Surname
                        </th>
                        <th>
                            Number
                        </th>
                        <th>
                            Actions
                        </th>
                    </thead>
                    <tbody>
                        {Contacts.map(item =>

                            <tr key={item.email}>
                                <td>{item.name}</td>
                                <td>{item.surname}</td>
                                <td>{item.phoneNumber}</td>
                                <td>
                                    <button type="button" className="btn btn-light mr-1"
                                        data-bs-toggle="modal"
                                        data-bs-target="#exampleModal"
                                        onClick={() => editClick(item,this)}>
                                        {window.$logged ? "edit" : "open" }
                                    </button>
                                    <button type="button" disabled={!window.$logged} className="btn btn-light mr-1"
                                        onClick={() => deleteClick(item,this)}>
                                        del
                                    </button>
                                </td>
                            </tr>
                        )}
                    </tbody>

                </table>
                <PopUp trigger={this.state.buttonPopup} setTrigger={this }>
                    <div className="input-group mb-3">
                        <span className="input-group-text">Name</span>
                        <input type="text" disabled={!window.$logged} className="form-control"
                            value={Name}
                            onChange={this.changeName} />
                    </div>
                    <div className="input-group mb-3">
                        <span className="input-group-text">Surname</span>
                        <input type="text" disabled={!window.$logged} className="form-control"
                            value={Surname}
                            onChange={this.changeSurname} />
                    </div>
                    <div className="input-group mb-3">
                        <span className="input-group-text">Email</span>
                        <input type="text" disabled={!window.$logged} className="form-control"
                            value={Email}
                            onChange={this.changeEmail} />
                    </div>
                    <div>
                        <h5>Category</h5>
                        <select disabled={!window.$logged} value={Category } onChange={this.changeCategory}>
                            {Categories.map(category =>
                                <option>{category.category}</option>
                            )}
                        </select>
                    </div><br />
                    {Category === "prywatny" ?
                        <div><h5>SubCategory</h5>
                            <select disabled={!window.$logged} value={SubCategory} onChange={ this.changeSubCategory}>
                                {SubCategories.map(subCategory =>
                                    <option>{subCategory.subCategory}</option>
                                )}
                        </select></div>
                        : null}
                    {Category === "inny" ?
                        <div className="input-group mb-3">
                            <span className="input-group-text">Other</span>
                            <input type="text" disabled={!window.$logged} className="form-control"
                                value={Other}
                                onChange={this.changeOther} />
                        </div>
                        : null}
                    <div className="input-group mb-3">
                        <span className="input-group-text">PhoneNumber</span>
                        <input disabled={!window.$logged}type="number" className="form-control"
                            value={PhoneNumber}
                            onChange={this.changePhoneNumber} />
                    </div>
                    <div>
                        <h5>Date</h5>
                        <input type="date" disabled={!window.$logged} value={Date } onChange={this.changeDate }></input>
                    </div><br />
                    {Id === 0 ?
                        <button type="button"
                            disabled={!window.$logged}
                            className="btn btn-primary float-start"
                            onClick={() => createClick(this)}
                        >Create</button>
                        : null}

                    {Id !== 0 ?
                        <button type="button"
                            disabled={!window.$logged}
                            className="btn btn-primary float-start"
                            onClick={() => updateClick(this)}
                        >Update</button>
                        : null}
                </PopUp>
                
                                

                            

            </div>
        );
    }

    async populateData() {
        const response = await fetch('contact');
        const data = await response.json();
        this.setState({ Contacts: data });
        const response2 = await fetch('category');
        const data2 = await response2.json();
        this.setState({ Categories: data2 });
        const response3 = await fetch('subCategory');
        const data3 = await response3.json();
        this.setState({ SubCategories: data3, loading: false });
    }
}
