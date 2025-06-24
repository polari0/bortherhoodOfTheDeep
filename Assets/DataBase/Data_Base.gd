class_name DataBase extends Node

var database: SQLite = SQLite.new()
var database_path: String = "res://Assets/DataBase/BrotherHoodOfTheDeep.db"
var _is_persistent_db_conn_open: bool = false

func _ready() -> void:
	database.path = database_path
	open_persistent_db_connection()

func query(query_string: String) -> Array:
	
	if not _is_persistent_db_conn_open:
		database.open_db()
	
	database.query(query_string)
	print_stack_on_database_error(database.error_message)
	var data = database.query_result
	
	if not _is_persistent_db_conn_open:
		database.close_db()
	
	database.verbosity_level = 0
	return data

func query_with_bindings(query_string: String, bindings: Array) -> Array:
	
	if not _is_persistent_db_conn_open:
		database.open_db()
	
	database.query_with_bindings(query_string, bindings)
	print_stack_on_database_error(database.error_message)
	var data = database.query_result
	
	if not _is_persistent_db_conn_open:
		database.close_db()
	
	database.verbosity_level = 0
	return data

func open_persistent_db_connection() -> bool:
	if !_is_persistent_db_conn_open:
		_is_persistent_db_conn_open = database.open_db()
	return _is_persistent_db_conn_open


# Close the persistent database connection. Returns true if database connection was closed.
func close_persistent_db_connection() -> bool:
	_is_persistent_db_conn_open = !database.close_db()
	return _is_persistent_db_conn_open

func print_stack_on_database_error(error_msg: String) -> void:
	if error_msg != "" and error_msg != "not an error":
		var stack: Array = get_stack()
		# Remove this function from the call stack
		if stack.size() > 0:
			stack.remove_at(0)
		print("Database error: " + error_msg + "\nDatabase error call stack: " + str(stack))

func is_persistent_db_connection_open() -> bool:
	return _is_persistent_db_conn_open
