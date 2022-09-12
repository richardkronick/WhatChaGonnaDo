using DoWhatNow.Models;

public interface IToDoService
{
    /// <summary>
    /// The service level method for getting all to do lists from the database.
    /// </summary>
    /// <param name="orderByDesc">An optional value indicating if the results should be sorted by created on desc or not.</param>
    /// <returns>All active to do lists.</returns>
    Task<List<ToDoList>> GetAllToDoListsAsync(bool orderByDesc = false);

    /// <summary>
    /// The service level method for getting all to do items from the database.
    /// </summary>
    /// <returns>All active to do items.</returns>
    Task<List<ToDoItem>> GetAllToDoItemsAsync();

    /// <summary>
    /// The service level method for getting a single to do item from the database.
    /// </summary>
    /// <param name="toDoItemId">The id of the to do item to get.</param>
    /// <returns>A to do item.</returns>
    Task<ToDoItem> GetToDoItemByIdAsync(int toDoItemId);

    /// <summary>
    /// The service level method for getting to do items for a specific to do list from the database.
    /// </summary>
    /// <param name="toDoListId">The id of the to do list for which to get to do items.</param>
    /// <returns>A list to do items.</returns>
    Task<List<ToDoItem>> GetToDoItemsByListIdAsync(int toDoListId);

    /// <summary>
    /// The service level method for creating to do lists in the database.
    /// </summary>
    /// <param name="toDoLists">A collection of to do list models to insert in the database.</param>
    /// <returns>A task.</returns>
    Task CreateToDoListsAsync(IList<ToDoList> toDoLists);

    /// <summary>
    /// The service level method for creating to do items in the database.
    /// </summary>
    /// <param name="toDoLists">A collection of to do items models to insert in the database.</param>
    /// <returns>A task.</returns>
    Task CreateToDoItemsAsync(IList<ToDoItem> toDoItems);

    /// <summary>
    /// The service level method for getting a single to do list and its to do items from the database.
    /// </summary>
    /// <param name="toDoListId">The id of the to do list to get.</param>
    /// <returns>A to do list and its to do items.</returns>
    Task<ToDoList> GetToDoListByIdAsync(int toDoListId);

    /// <summary>
    /// The service level method for deleting to do items from the database.
    /// </summary>
    /// <param name="toDoLists">A collection of to do items models to delete from the database.</param>
    /// <returns>A task.</returns>
    Task DeleteToDoListsAsync(IList<ToDoList> toDoLists);

    /// <summary>
    /// The service level method for deleting to do lists and their to do items from the database.
    /// </summary>
    /// <param name="toDoLists">A collection of to do lists and their to do items to delete from the database.</param>
    /// <returns>A task.</returns>
    Task DeleteToDoItemsAsync(IList<ToDoItem> toDoItems);

    /// <summary>
    /// The service level method for saving database changes.
    /// </summary>
    /// <returns>A task.</returns>
    Task SaveChangesAsync();
}