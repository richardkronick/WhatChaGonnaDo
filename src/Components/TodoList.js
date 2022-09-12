import React from "react";
import TodoItem from "./TodoItem";
import Paper from "@material-ui/core/Paper";
import List from "@material-ui/core/List";
import Divider from "@material-ui/core/Divider";

function TodoList({ todos, removeTodo, toggleTodo, editTodo }) {
    if (todos.length)
      return (
        <Paper>
          <List>
            {todos.map((todo, i) => (
              <React.Fragment key={i}>
                <TodoItem
                  {...todo}
                  key={todo.id}
                  removeTodo={removeTodo}
                  toggleTodo={toggleTodo}
                  editTodo={editTodo}
                />
                {i < todos.length - 1 && <Divider />}
              </React.Fragment>
            ))}
          </List>
        </Paper>
      );
    return null;
  }
export default TodoList;
