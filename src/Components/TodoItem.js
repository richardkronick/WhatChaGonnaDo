import React from "react";
import useToggleState from "../hooks/useToggleState";
import EditTodoForm from "./EditTodoForm";
import ListItem from "@material-ui/core/ListItem";
import ListItemText from "@material-ui/core/ListItem";
import Checkbox from "@material-ui/core/Checkbox";
import IconButton from "@material-ui/core/IconButton";
import DeleteIcon from "@material-ui/icons/Delete";
import ListItemSecondaryAction from "@material-ui/core/ListItemSecondaryAction";

function TodoItem({ id, task, completed, removeTodo, toggleTodo, editTodo }) {
    const [isEditing, toggle] = useToggleState(false);
    return (
      <ListItem style={{ height: "64px" }}>
        {isEditing ? (
          <EditTodoForm
            editTodo={editTodo}
            id={id}
            task={task}
            toggleEditForm={toggle}
          />
        ) : (
          <>
            <Checkbox
              tabIndex={-1}
              checked={completed}
              onClick={() => toggleTodo(id, completed)}
            />
            <ListItemText
              style={{ textDecoration: completed ? "line-through" : "none" }}
            >
              {task}
            </ListItemText>
            <ListItemSecondaryAction>
              <IconButton aria-label='Delete' onClick={() => removeTodo(id)}>
                <DeleteIcon />
              </IconButton>
            </ListItemSecondaryAction>
          </>
        )}
      </ListItem>
    );
  }

export default TodoItem;
