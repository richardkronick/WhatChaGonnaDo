import React, { useState } from 'react';
import TodoList from "./TodoList";
import TodoForm from "./TodoForm";
import ToDoListLabel from "./ToDoListLabel";
import CreateNewToDoList from "./CreateNewToDoList";
import Typography from "@material-ui/core/Typography";
import Paper from "@material-ui/core/Paper";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Grid from "@material-ui/core/Grid";
import Divider from "@material-ui/core/Divider";
import useTodoState from "../hooks/useTodoState";
import apiService from "../services/apiService";

function TodoApp() {
  const initialTodos = [{ id: 1, task: "Walk The Goldfish", completed: true }];
  const { editTodo } = useTodoState(initialTodos);
  const [dbToDoItems, setToDoItems] = useState(initialTodos);
  const [dbToDoLists, setToDoLists] = useState();
  const [isDataLoaded, setIsDataLoaded] = useState(false);

  let _defaultCreatedBy = "react app";

  React.useEffect(() => {
    apiService.getAllToDoLists(getAllToDoListsCallback);
  }, [dbToDoLists]);

  const toggleTodo = (toDoItemId, isCurrentlyCompleted) => {
    setIsDataLoaded(false);
    apiService.UpdateTodoItemToggle(toDoItemId, isCurrentlyCompleted, _defaultCreatedBy);

    apiService.getAllToDoLists(getAllToDoListsCallback);
    setIsDataLoaded(true);
  }

  const editTodoListLabel = (toDoItemId, newLabel) => {
    setIsDataLoaded(false);
    apiService.UpdateTodoListLabel(toDoItemId, newLabel, _defaultCreatedBy);

    apiService.getAllToDoLists(getAllToDoListsCallback);
    setIsDataLoaded(true);
  }

  const addToDoItem = (toDoListId, toDoItemTask) => {
    setIsDataLoaded(false);
    apiService.CreateToDoItem(toDoListId, toDoItemTask, _defaultCreatedBy);

    apiService.getAllToDoLists(getAllToDoListsCallback);
    setIsDataLoaded(true);
  }

  const removeTodoItem = (toDoItemId) => {
    setIsDataLoaded(false);
    apiService.RemoveToDoItem(toDoItemId, _defaultCreatedBy);

    apiService.getAllToDoLists(getAllToDoListsCallback);
    setIsDataLoaded(true);
  }

  const createToDoList = (id, toDoListLabel) => {
    setIsDataLoaded(false);
    apiService.CreateToDoList(toDoListLabel, _defaultCreatedBy);

    apiService.getAllToDoLists(getAllToDoListsCallback);
    setIsDataLoaded(true);
  }

  const removeTodoList = (id) => {
    setIsDataLoaded(false);
    apiService.RemoveToDoList(id, _defaultCreatedBy);

    apiService.getAllToDoLists(getAllToDoListsCallback);
    setIsDataLoaded(true);
  }

  const getAllToDoListsCallback = (toDoListsFromDB) => {
    let todoLists = [];
    toDoListsFromDB.forEach((toDoList) => {
      let todos = [];
      toDoList.toDoItems.forEach((toDoItem) => {
        let toDo = { id: toDoItem.toDoItemId, task: toDoItem.toDoTask, completed: toDoItem.isCompleted };
        todos.push(toDo);
      });

      let toDoListCollection = {id: toDoList.toDoListId, label: toDoList.label.toUpperCase(), todos: todos, createdBy: toDoList.createdBy};
      todoLists.push(toDoListCollection);
    });

    setToDoLists(todoLists);
    setIsDataLoaded(true);
  }

  return (
    isDataLoaded && <Paper
      style={{
        padding: 0,
        margin: 0,
        height: "100vh",
        backgroundColor: "#F0F8FF"
      }}
      elevation={0}
    >
      <AppBar color='primary' position='static' style={{ height: "64px" }}>
        <Toolbar>
          <Typography color='inherit'>TODOS APP</Typography>
        </Toolbar>
      </AppBar>
      <Grid container justify='center'>
        <CreateNewToDoList createToDoList={createToDoList}/>
      </Grid>
        {dbToDoLists.map((toDoList, i) => (
          <Grid container style={{ backgroundColor: "#C6E6FB", marginBottom: "3rem", border: 1}}>
          <Grid container justify='center' style={{ marginBottom: "2rem" }}>
          <Grid item xs={11} md={8} lg={4} style={{ marginTop: "1rem", marginBottom: "1rem" }}>
              <React.Fragment key={i}>
                <ToDoListLabel
                  id={toDoList.id}
                  label={toDoList.label}
                  editTodoListLabel={editTodoListLabel}
                  createdBy={toDoList.createdBy}
                  removeTodoList={removeTodoList}
                />
                <TodoForm
                  id={toDoList.id}
                  addTodo={addToDoItem}
                />
                <TodoList
                  todos={toDoList.todos}
                  removeTodo={removeTodoItem}
                  toggleTodo={toggleTodo}
                  editTodo={editTodo}
                />
                <p>Created By: {toDoList.createdBy}</p>
                {i < toDoList.todos.length - 1 && <Divider />}
              </React.Fragment>
              </Grid>
          </Grid>
          <Divider style={{ backgroundColor: "#555555" }} />
          </Grid>
            ))}
    </Paper>
  );
}
export default TodoApp;
