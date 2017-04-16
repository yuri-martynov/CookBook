module Data

open FSharp.Data
open Db


type DishXml = XmlProvider<"../Db/Resources/борщ.xml">

let get (id:string) : Async<DishXml.Dish> = async {
    let stream = Files.getStream id
    return DishXml.Load stream
}