import React from "react";
import useToggleState from "../hooks/useToggleState";
import EditTodoForm from "./EditTodoForm";
import ListItem from "@material-ui/core/ListItem";
import ListItemText from "@material-ui/core/ListItem";
import IconButton from "@material-ui/core/IconButton";
import DeleteIcon from "@material-ui/icons/Delete";
import EditIcon from "@material-ui/icons/Edit";
import ListItemSecondaryAction from "@material-ui/core/ListItemSecondaryAction";

function ToDoListLabel({ id, label, editTodoListLabel, createdBy, removeTodoList }) {
    const [isEditing, toggle] = useToggleState(false);
    return (
      <ListItem alignItems={'center'} style={{ height: "64px" }}>
        {isEditing ? (
          <EditTodoForm
            editTodo={editTodoListLabel}
            id={id}
            task={label}
            toggleEditForm={toggle}
          />
        ) : (
          <>
            <ListItemText>
              "{label}"
            </ListItemText>
            <ListItemSecondaryAction>
              <IconButton aria-label='Edit' onClick={toggle}>
                <EditIcon />
              </IconButton>
              <IconButton aria-label='Delete' onClick={() => removeTodoList(id)}>
              <DeleteIcon />
            </IconButton>
            </ListItemSecondaryAction>
          </>
        )}
      </ListItem>
    );
  }

export default ToDoListLabel;