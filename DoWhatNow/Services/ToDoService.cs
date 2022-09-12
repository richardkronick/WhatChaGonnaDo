using DoWhatNow.Models;

namespace DoWhatNow.Services
{
    public class ToDoService : IToDoService
    {
        private readonly DataContext _dataContext;

        public ToDoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// The service level method for getting all to do lists from the database.
        /// </summary>
        /// <param name="orderByDesc">An optional value indicating if the results should be sorted by created on desc or not.</param>
        /// <returns>All active to do lists.</returns>
        public async Task<List<ToDoList>> GetAllToDoListsAsync(bool orderByDesc = false)
        {
            List<ToDoList> _todoLists = new List<ToDoList>();

            if (orderByDesc)
            {
                _todoLists = await _dataContext.ToDoLists
                    .Where(t => t.IsDisabled == false)
                    .OrderByDescending(d => d.CreatedOn)
                    .ToListAsync();
            }
            else
            {
                _todoLists = await _dataContext.ToDoLists
                    .Where(t => t.IsDisabled == false)
                    .ToListAsync();
            }

            await GetAllToDoItemsAsync();

            return _todoLists;
        }

        /// <summary>
        /// The service level method for getting all to do items from the database.
        /// </summary>
        /// <returns>All active to do items.</returns>
        public async Task<List<ToDoItem>> GetAllToDoItemsAsync()
        {
            List<ToDoItem> _toDoItems = await _dataContext.ToDoItems
                .Where(t => t.IsDisabled == false)
                .ToListAsync();

            return _toDoItems;
        }

        /// <summary>
        /// The service level method for getting a single to do item from the database.
        /// </summary>
        /// <param name="toDoItemId">The id of the to do item to get.</param>
        /// <returns>A to do item.</returns>
        public async Task<ToDoItem> GetToDoItemByIdAsync(int toDoItemId)
        {
            ToDoItem? _todoItem = await _dataContext.ToDoItems.FindAsync(toDoItemId);

            return _todoItem!;
        }

        /// <summary>
        /// The service level method for getting a single to do list and its to do items from the database.
        /// </summary>
        /// <param name="toDoListId">The id of the to do list to get.</param>
        /// <returns>A to do list and its to do items.</returns>
        public async Task<ToDoList> GetToDoListByIdAsync(int toDoListId)
        {
            ToDoList? _todoList = await _dataContext.ToDoLists.FindAsync(toDoListId);
            await GetToDoItemsByListIdAsync(toDoListId);

            return _todoList!;
        }

        /// <summary>
        /// The service level method for getting to do items for a specific to do list from the database.
        /// </summary>
        /// <param name="toDoListId">The id of the to do list for which to get to do items.</param>
        /// <returns>A list to do items.</returns>
        public async Task<List<ToDoItem>> GetToDoItemsByListIdAsync(int toDoListId)
        {
            List<ToDoItem> _toDoItems = await _dataContext.ToDoItems
                .Where(t => t.ToDoListId == toDoListId)
                .ToListAsync();

            return _toDoItems;
        }

        /// <summary>
        /// The service level method for creating to do lists in the database.
        /// </summary>
        /// <param name="toDoLists">A collection of to do list models to insert in the database.</param>
        /// <returns>A task.</returns>
        public async Task CreateToDoListsAsync(IList<ToDoList> toDoLists)
        {
            _dataContext.ToDoLists.AddRange(toDoLists);
            await SaveChangesAsync();
        }

        /// <summary>
        /// The service level method for creating to do items in the database.
        /// </summary>
        /// <param name="toDoLists">A collection of to do items models to insert in the database.</param>
        /// <returns>A task.</returns>
        public async Task CreateToDoItemsAsync(IList<ToDoItem> toDoItems)
        {
            _dataContext.ToDoItems.AddRange(toDoItems);
            await SaveChangesAsync();
        }

        /// <summary>
        /// The service level method for deleting to do items from the database.
        /// </summary>
        /// <param name="toDoLists">A collection of to do items models to delete from the database.</param>
        /// <returns>A task.</returns>
        public async Task DeleteToDoListsAsync(IList<ToDoList> toDoLists)
        {
            _dataContext.ToDoLists.RemoveRange(toDoLists);

            foreach(var _toDoList in toDoLists)
            {
                if (_toDoList.ToDoItems.Count > 0)
                {
                    _dataContext.ToDoItems.RemoveRange(_toDoList.ToDoItems);
                }
            }
            
            await SaveChangesAsync();
        }

        /// <summary>
        /// The service level method for deleting to do lists and their to do items from the database.
        /// </summary>
        /// <param name="toDoLists">A collection of to do lists and their to do items to delete from the database.</param>
        /// <returns>A task.</returns>
        public async Task DeleteToDoItemsAsync(IList<ToDoItem> toDoItems)
        {
            _dataContext.ToDoItems.RemoveRange(toDoItems);
            await SaveChangesAsync();
        }

        /// <summary>
        /// The service level method for saving database changes.
        /// </summary>
        /// <returns>A task.</returns>
        public async Task SaveChangesAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
