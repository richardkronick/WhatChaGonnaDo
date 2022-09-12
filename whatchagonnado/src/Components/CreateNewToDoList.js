import React from "react";
import useToggleState from "../hooks/useToggleState";
import EditTodoForm from "./EditTodoForm";
import ListItem from "@material-ui/core/ListItem";
import Button from "@material-ui/core/Button";

function CreateNewToDoList({ createToDoList }) {
    const [isEditing, toggle] = useToggleState(false);
    return (
      <ListItem alignItems={'center'} style={{ height: "64px", width: "300px" }}>
        {isEditing ? (
          <EditTodoForm
            editTodo={createToDoList}
            id={1}
            task={''}
            toggleEditForm={toggle}
          />
        ) : (
          <>
          <Button color="primary" variant="contained" disableElevation aria-label='createList' onClick={toggle}>
            Create a new to do list!
            </Button>
          </>
        )}
      </ListItem>
    );
  }

export default CreateNewToDoList;