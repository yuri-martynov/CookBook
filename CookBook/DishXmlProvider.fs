module DishXmlProvider

open FSharp.Data

type private DishXml = XmlProvider<"../DataAccess/борщ.xml">

let parse str : DishXml.Dish = 
    DishXml.Parse str


