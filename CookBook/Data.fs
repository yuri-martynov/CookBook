module Data

open FSharp.Data


type DishXml = XmlProvider<"../recipe/Db/борщ.xml">

let get id : DishXml.Dish =
    let str = System.IO.File.ReadAllText """D:\home\site\wwwroot\recipe\db\""" + id + ".xml"
    DishXml.Parse str