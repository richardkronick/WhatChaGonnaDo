import axios from 'axios';

const apiService = {
    getToDoItem: function(toDoItemId, callback) {
        let _url = `https://localhost:7245/api/v1/ToDo/ToDoItems/${toDoItemId}`;

        axios.get(_url).then((response) => {
            callback(response.data);
        });
    },

    getAllToDoItemsByListId: function(toDoListId, callback) {
        let _url = `https://localhost:7245/api/v1/ToDo/ToDoLists/${toDoListId}/toDoItems`;

        axios.get(_url).then((response) => {
            callback(response.data);
        });
    },

    getAllToDoLists: function(callback) {
        let _url = `https://localhost:7245/api/v1/ToDo/toDoLists?orderByDesc=true`;

        axios.get(_url).then((response) => {
            callback(response.data);
        });
    },

    UpdateTodoItemToggle: function(toDoItemId, toggleState, modifiedBy) {
        let _url = `https://localhost:7245/api/v1/ToDo/toDoItems/${toDoItemId}/toggle`;

        let _data = {'IsCompleted' : !toggleState, 'modifiedBy': modifiedBy}
        axios.post(_url, _data).then((response) => {
        });
    },

    UpdateTodoListLabel: function(toDoListId, newLabel, modifiedBy) {
        let _url = `https://localhost:7245/api/v1/ToDo/toDoLists/${toDoListId}/label`;

        let _data = {'newLabel' : newLabel, 'modifiedBy': modifiedBy}
        axios.put(_url, _data).then((response) => {
        });
    },

    CreateToDoItem: function(toDoListId, toDoItemTask, createdBy) {
        let _url = `https://localhost:7245/api/v1/ToDo/toDoLists/${toDoListId}/toDoItems`;

        let _data = {'ToDoTask' : toDoItemTask, 'createdBy': createdBy}
        axios.post(_url, _data).then((response) => {
        });
    },

    RemoveToDoItem: function(toDoItemId, createdBy) {
        let _url = `https://localhost:7245/api/v1/ToDo/toDoItems/${toDoItemId}/status`;

        let _data = {'IsDisabled' : true, 'createdBy': createdBy}
        axios.post(_url, _data).then((response) => {
        });
    },

    CreateToDoList: function(toDoListLabel, createdBy) {
        let _url = `https://localhost:7245/api/v1/ToDo/toDoLists`;

        let _data = {'Label' : toDoListLabel, 'createdBy': createdBy}
        axios.post(_url, _data).then((response) => {
        });
    },

    RemoveToDoList: function(toDoListId, createdBy) {
        let _url = `https://localhost:7245/api/v1/ToDo/toDoLists/${toDoListId}/status`;

        let _data = {'IsDisabled' : true, 'createdBy': createdBy}
        axios.post(_url, _data).then((response) => {
        });
    },
  };



  
  
  export default apiService;