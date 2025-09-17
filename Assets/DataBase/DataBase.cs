using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Resolvers;

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


    /// <summary>
    /// Updates a row in specified table with given dictionary of updates
    /// </summary>
    /// <param name="TableName">Table to update</param>
    /// <param name="querryConditions">Determines withc row or rows to update</param>
    /// <param name="rowUpdates">Tetermines what to upadte into a given row</param>
    public static void UpdateRows(String TableName, String querryConditions, Godot.Variant rowUpdates)
    {
        database_script_node.Call("update_rows", TableName, querryConditions, rowUpdates);
    }

    /// <summary>
    /// Selects specifieds rows from the table can also work with query function
    /// </summary>
    /// <param name="TableName">Table to select from</param>
    /// <param name="querryConditions">Determine which rows to select</param>
    /// <param name="selectedColums">Determine which colums to select</param>
    public static void SelectRows(String TableName, String querryConditions, Godot.Variant selectedColums)
    {
        database_script_node.Call("select_rows", TableName, querryConditions, selectedColums);
    }

    public static void addImageToDB(String ImagePath, String TableName, String querryConditions)
    {
        Image image = (Image)GD.Load(ImagePath);
        Byte[] pba = image.SaveJpgToBuffer();
        Godot.Collections.Dictionary<String, Byte[]> blob = new Godot.Collections.Dictionary<string, byte[]>();
        blob.Add("AbilitySprite", pba);
        database_script_node.Call("update_rows", TableName, querryConditions, blob);
    }
}
