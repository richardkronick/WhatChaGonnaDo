using DoWhatNow.Models;
using DoWhatNow.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DoWhatNow.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        private const string _defaultCreatedBy = "DoWhatNow.ToDoController";

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        /// <summary>
        /// Get all to do lists along with their associated to do items.
        /// </summary>
        /// <param name="orderByDesc">An optional boolean parameter indicating if the items should be sorted by created on descending.</param>
        /// <returns>All active to do lists.</returns>
        [HttpGet("toDoLists")]
        public async Task<IActionResult> GetAllToDoLists([FromQuery] bool orderByDesc = false)
        {
            // Get to do lists and to do items.
            List<ToDoList> _todoLists = await _toDoService.GetAllToDoListsAsync(orderByDesc);

            return Ok(_todoLists);
        }

        /// <summary>
        /// Get all aactive to do items.
        /// </summary>
        /// <returns>All active to do items.</returns>
        [HttpGet("toDoItems")]
        public async Task<IActionResult> GetAllToDoItems()
        {
            // Get all to do items.
            List<ToDoItem> _todoItems = await _toDoService.GetAllToDoItemsAsync();

            return Ok(_todoItems);
        }

        /// <summary>
        /// Get a single to do item by its id.
        /// </summary>
        /// <param name="toDoItemId">The to do item's id.</param>
        /// <returns>The to do item.</returns>
        [HttpGet("toDoItems/{toDoItemId}")]
        public async Task<IActionResult> GetSingleToDoItem(int toDoItemId)
        {
            // Get the to do item by its id.
            ToDoItem? _todoItem = await _toDoService.GetToDoItemByIdAsync(toDoItemId);

            return Ok(_todoItem);
        }

        /// <summary>
        /// Get all to do items within a given list.
        /// </summary>
        /// <param name="toDoListId">The to do list's id.</param>
        /// <returns>The to do item within the given to do list.</returns>
        [HttpGet("toDoLists/{toDoListId}/toDoItems")]
        public async Task<IActionResult> GetAllToDoItemsByListId(int toDoListId)
        {
            // Get all to do items in the list with list id of toDoListId.
            List<ToDoItem> _toDoItems = await _toDoService.GetToDoItemsByListIdAsync(toDoListId);

            return Ok(_toDoItems);
        }

        /// <summary>
        /// Creates a new to do list.
        /// </summary>
        /// <param name="toDoListRequest">The request details for creating a new list.</param>
        /// <returns>The newly created list.</returns>
        [HttpPost("toDoLists")]
        public async Task<IActionResult> CreateToDoList([FromBody] CreateToDoListRequestVersion1 toDoListRequest)
        {
            // Validate the request.
            if (toDoListRequest == null) return BadRequest("The request object was empty. No to-do list details were provided.");

            if (IsStringNullOrEmpty(toDoListRequest.Label)) return BadRequest("A to do list label is required.");

            if (toDoListRequest.Label.Length > 100)
                return (BadRequest($"The following label exceeds the limit of 100 characters: {toDoListRequest.Label}."));

            // The request is valid. Create the new to do list.
            ToDoList _toDoList = new()
            {
                Label = toDoListRequest.Label,
                CreatedBy = GetCreatedOrModifiedBy(toDoListRequest.CreatedBy),
                CreatedOn = DateTimeOffset.UtcNow,
            };

            await _toDoService.CreateToDoListsAsync(new List<ToDoList> { _toDoList });

            return Ok(_toDoList);
        }

        /// <summary>
        /// Creates a new to do item.
        /// </summary>
        /// <param name="toDoItemRequest">The request details for creating a new item.</param>
        /// <returns>The newly created item.</returns>
        [HttpPost("toDoLists/{toDoListId}/toDoItems")]
        public async Task<IActionResult> CreateTodoItem(int toDoListId, [FromBody] CreateToDoItemRequestVersion1 toDoItemRequest)
        {
            // Validate the request.
            if (toDoItemRequest == null) return BadRequest("The request object was empty. No to-do details were provided.");

            if (IsStringNullOrEmpty(toDoItemRequest.ToDoTask)) return BadRequest("Please provide a to do item.");

            // Validate that a to do list with the id of toDoListId exists.
            ToDoList? _todoList = await _toDoService.GetToDoListByIdAsync(toDoListId);

            if (_todoList == null) return BadRequest("No to do list was found to add this item to.");

            // Create the new to do item, and associate it with its intended to do list.
            ToDoItem _toDoItem = new()
            {
                ToDoTask = toDoItemRequest.ToDoTask,
                CreatedBy = GetCreatedOrModifiedBy(toDoItemRequest.CreatedBy),
                CreatedOn = DateTimeOffset.UtcNow,
                ToDoListId = toDoListId,
            };

            await _toDoService.CreateToDoItemsAsync(new List<ToDoItem> { _toDoItem });

            return Ok(_toDoItem);
        }

        /// <summary>
        /// Update a to do list's label.
        /// </summary>
        /// <param name="updateToDoListLabelRequest">The request details for updating the list's label.</param>
        /// <returns>A task.</returns>
        [HttpPut("toDoLists/{toDoListId}/label")]
        public async Task<IActionResult> UpdateTodoListLabel(int toDoListId, [FromBody] UpdateToDoListLabelRequestVersion1 updateToDoListLabelRequest)
        {
            // Get the to do list and validate it.
            ToDoList? _toDoList = await _toDoService.GetToDoListByIdAsync(toDoListId);

            if (_toDoList == null) return BadRequest("No to do list was found.");

            if (IsStringNullOrEmpty(updateToDoListLabelRequest.NewLabel)) return BadRequest("A to do list label must be provided.");

            if (updateToDoListLabelRequest.NewLabel.Length > 100) 
                return (BadRequest($"The following label exceeds the limit of 100 characters: {updateToDoListLabelRequest.NewLabel}."));

            // Update the label.
            _toDoList.Label = updateToDoListLabelRequest.NewLabel;
            _toDoList.ModifiedOn = DateTimeOffset.UtcNow;
            _toDoList.ModifiedBy = GetCreatedOrModifiedBy(updateToDoListLabelRequest.ModifiedBy);

            await _toDoService.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Update a to do item's completed state.
        /// </summary>
        /// <param name="updateToDoItemToggleRequest">The request details for toggling the isCompleted state of a to do item.</param>
        /// <returns>A task.</returns>
        [HttpPost("toDoItems/{toDoItemId}/toggle")]
        public async Task<IActionResult> UpdateTodoItemToggle(int toDoItemId, [FromBody] UpdateToDoItemToggleRequestVersion1 updateToDoItemToggleRequest)
        {
            // Get the to do item and validate it.
            ToDoItem? _toDoItem = await _toDoService.GetToDoItemByIdAsync(toDoItemId);

            if (_toDoItem == null) return BadRequest("No to do item was found");

            // Update toggle state.
            _toDoItem.IsCompleted = updateToDoItemToggleRequest.IsCompleted;
            _toDoItem.ModifiedOn = DateTimeOffset.UtcNow;
            _toDoItem.ModifiedBy = GetCreatedOrModifiedBy(updateToDoItemToggleRequest.ModifiedBy);

            await _toDoService.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Updates a to do list's status. This is a soft deletion or re-enaling of data.
        /// </summary>
        /// <param name="updateStatusRequest">The request details for toggling the IsDisabled condition of a to do list and it's to do items.</param>
        /// <returns>The to do list whose state was toggled.</returns>
        [HttpPost("toDoLists/{toDoListId}/status")]
        public async Task<IActionResult> UpdateTodoListStatus(int toDoListId, [FromBody] UpdateStatusRequest updateStatusRequest)
        {
            // Get the to do list by id and validate it.
            ToDoList? _toDoList = await _toDoService.GetToDoListByIdAsync(toDoListId);

            if (_toDoList == null) return BadRequest("No to do list was found.");

            // Update the to do list to be disabled or enabled.
            _toDoList.IsDisabled = updateStatusRequest.IsDisabled;
            _toDoList.ModifiedOn = DateTimeOffset.UtcNow;
            _toDoList.ModifiedBy = GetCreatedOrModifiedBy(updateStatusRequest.ModifiedBy);

            if (_toDoList.ToDoItems.Count > 0)
            {
                foreach (ToDoItem toDoItem in _toDoList.ToDoItems)
                {
                    toDoItem.IsDisabled = updateStatusRequest.IsDisabled;
                }
            }

            await _toDoService.SaveChangesAsync();


            return Ok(_toDoList);
        }

        /// <summary>
        /// Updates a to do item's status. This is a soft deletion or re-enaling of data.
        /// </summary>
        /// <param name="updateStatusRequest">The request details for toggling the IsDisabled condition of a to do item.</param>
        /// <returns>The to do item whose state was toggled.</returns>
        [HttpPost("toDoItems/{toDoItemId}/status")]
        public async Task<IActionResult> UpdateTodoItemStatus(int toDoItemId, [FromBody] UpdateStatusRequest updateStatusRequest)
        {
            // Get to do item by id and validate it.
            ToDoItem? _toDoItem = await _toDoService.GetToDoItemByIdAsync(toDoItemId);

            if (_toDoItem == null) return BadRequest("No to do list was found.");

            // If the request is to enable an item, ensure that the list it is in is also enabled.
            if (updateStatusRequest.IsDisabled == false)
            {
                ToDoList? _toDoList = await _toDoService.GetToDoListByIdAsync(_toDoItem.ToDoListId);
                if (_toDoList == null || _toDoList.IsDisabled == true) return BadRequest("Cannot enable a to do item in a disabled of deleted list");
            }

            // Update the to do item to be disabled or enabled.
            _toDoItem.IsDisabled = updateStatusRequest.IsDisabled;
            _toDoItem.ModifiedOn = DateTimeOffset.UtcNow;
            _toDoItem.ModifiedBy = GetCreatedOrModifiedBy(updateStatusRequest.ModifiedBy);

            await _toDoService.SaveChangesAsync();

            return Ok(_toDoItem);
        }

        /// <summary>
        /// Performs a hard delete of a to do list. This is non-recoverable.
        /// </summary>
        /// <param name="toDoListId">The id of the list to delete.</param>
        /// <returns>A task.</returns>
        [HttpDelete("toDoLists/{toDoListId}")]
        public async Task<IActionResult> DeleteTodoList(int toDoListId)
        {
            // Get the to do list by id and its associated to do items.
            ToDoList? _toDoList = await _toDoService.GetToDoListByIdAsync(toDoListId);

            // Validate that the list exists.
            if (_toDoList == null) return BadRequest("No to do list was found to remove.");

            // Delete the to do list.
            await _toDoService.DeleteToDoListsAsync(new List<ToDoList> { _toDoList });

            return Ok();
        }

        /// <summary>
        /// Performs a hard delete of a to do item. This is non-recoverable.
        /// </summary>
        /// <param name="toDoListId">The id of the item to delete.</param>
        /// <returns>A task.</returns>
        [HttpDelete("toDoItems/{toDoItemId}")]
        public async Task<IActionResult> DeleteTodoItem(int toDoItemId)
        {
            // Get the to do item by id and validate it.
            ToDoItem? _toDoItem = await _toDoService.GetToDoItemByIdAsync(toDoItemId);

            if (_toDoItem == null) return BadRequest("No to do list was found to remove.");

            // Delete the to do item.
            await _toDoService.DeleteToDoItemsAsync(new List<ToDoItem> { _toDoItem });

            return Ok();
        }

        /// <summary>
        /// Validates and gets the request's createdBy or ModifiedBy value and returns a default value if invalid.
        /// </summary>
        /// <param name="requestValue">The createdBy or ModifiedBy value from the request.</param>
        /// <returns>The createdBy or modifiedBy value to use.</returns>
        private static string GetCreatedOrModifiedBy(string? requestValue)
        {
            string _createdOrModifiedBy = (IsStringNullOrEmpty(requestValue) || requestValue!.Length > 100)
                ? _defaultCreatedBy
                : requestValue;

            return _createdOrModifiedBy;
        }

        /// <summary>
        /// Checks if a string is null or empty.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>A boolean indicating if the string is null or empty or not.</returns>
        private static bool IsStringNullOrEmpty(string? value)
        {
            return value == null || value == string.Empty;
        }
    }
}
