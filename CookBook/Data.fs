module Data

open FSharp.Data
open DataAccess


type DishXml = XmlProvider<"../DataAccess/борщ.xml">

let private dataAccess = FilesDataAccess()

let get id : DishXml.Dish =
    let str = dataAccess.Get id
    DishXml.Parse str