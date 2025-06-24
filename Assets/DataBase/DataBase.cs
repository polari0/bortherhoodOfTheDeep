using Godot;
using Godot.NativeInterop;
using System;
using System.Linq;

public partial class DataBase : Node
{
    static GDScript database_script = GD.Load<GDScript>("res://Assets/DataBase/Data_Base.gd");
    static GodotObject database_script_node = (GodotObject)database_script.New();

    public static Godot.Collections.Array query(String query)
    {
        Godot.Collections.Array result = (Godot.Collections.Array)database_script_node.Call("query", query);
        return result;
    }

        public static Godot.Collections.Array query_with_bindings(String query, Godot.Collections.Array bindings)
    {
        Godot.Collections.Array result = (Godot.Collections.Array)database_script_node.Call("query_with_bindings", query, bindings);
        return result;
    }
}
