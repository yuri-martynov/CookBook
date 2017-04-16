module Data

open FSharp.Data
open Db


type DishXml = XmlProvider<"../Db/Resources/борщ.xml">

let get (id:string) : Async<DishXml.Dish> = async {
    let str = Files.get id
    return DishXml.Parse str
}