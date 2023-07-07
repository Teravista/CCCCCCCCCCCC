function refreshList(prop) {

    fetch('contact')
        .then(response => response.json())
        .then(data => {
            prop.setState({ Contacts: data });
        });

}

//Display create Popup with variables zeroed
function addClick(prop) {
     prop.setState({
        buttonPopup: true,
        Id: 0,
        Name: "",
        Surname: "",
        Email: "",
        Category: 0,
        SubCategory: 0,
        Other: "",
        PhoneNumber: 0,
        Date: "",
    })
}
//Display update Popup with values from selected contaqct
function editClick(dep,prop) {
    prop.setState({
        buttonPopup: true,
        Id: dep.id,
        Name: dep.name,
        Surname: dep.surname,
        Email: dep.email,
        Category: dep.category,
        SubCategory: dep.subCategory,
        Other: dep.other,
        PhoneNumber: dep.phoneNumber,
        Date: dep.date,
    })
}
//function sending created item to server by api
 function createClick(prop) {
    fetch('contact', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Name: prop.state.Name,
            Surname: prop.state.Surname,
            Email: prop.state.Email,
            Category: '' + prop.state.Category,
            Subcategory: '' + prop.state.SubCategory,
            Other: prop.state.Other,
            PhoneNumber: parseInt(prop.state.PhoneNumber, 10),
            Date: prop.state.Date,
            login: window.$login,
            password: window.$password,
        })
    })
        .then(res => res.json())
        .then((result) => {
            alert(result);
            refreshList(prop);
        }, (error) => {
            alert('Failed');
        })
}
//function sending item to server to update it by api

function updateClick(prop) {
    fetch('contact/' + prop.state.Id, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Name: prop.state.Name,
            Surname: prop.state.Surname,
            Email: prop.state.Email,
            Category: '' + prop.state.Category,
            Subcategory: '' + prop.state.SubCategory,
            other: prop.state.Other + '',
            PhoneNumber: parseInt(prop.state.PhoneNumber, 10),
            Date: prop.state.Date,
            login: window.$login,
            password: window.$password,
        })
    })
        .then(res => res.json())
        .then((result) => {
            alert(result);
            refreshList(prop);
        }, (error) => {
            alert('Failed');
            refreshList(prop);
        })
}
//function sending item to delete to server by api
 function deleteClick(item,prop) {
    if (window.confirm('Are you sure?')) {

        fetch('contact/' + item.id, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                login: window.$login,
                password: window.$password,
            })
        })
            .then(res => res.json())
            .then((result) => {
                alert(result);
                refreshList(prop);
            }, (error) => {
                alert('Failed');
               refreshList(prop);
            })
    }
}
export { addClick, editClick, createClick, updateClick, deleteClick, refreshList }