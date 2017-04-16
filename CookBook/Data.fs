module Data

open FSharp.Data
open DataAccess


type DishXml = XmlProvider<"../DataAccess/борщ.xml">

let private parse str : DishXml.Dish = 
    DishXml.Parse str

let private dataAccess = FilesDataAccess()

let get id =
    let str = dataAccess.Get id
    parse str