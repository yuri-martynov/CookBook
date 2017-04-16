module Data

open FSharp.Data


type DishXml = XmlProvider<"../recipe/Db/борщ.xml">

let get id : DishXml.Dish =
    let fileName = """D:\home\site\wwwroot\recipe\db\""" + id + ".xml"
    let str = System.IO.File.ReadAllText fileName
    DishXml.Parse str