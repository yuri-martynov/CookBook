module DishDataAccess

open Types

open Dishes

let private all p s =
    not <| Seq.exists (not << p) s 

let private dict : Map<string, Dish> = 
    dishes()  
    |> Seq.map (fun d -> (d.name, d))
    |> Map.ofSeq

let getById id = 
    dict |> Map.find id

let getByIngredients (productNames: seq<string>) : seq<string> =
    dishes()
    |> Seq.filter (fun d -> productNames |> all (fun p -> d |> Utils.ingredients |> Seq.collect Utils.productsQuantities |> Seq.map (fun i -> Utils.productName i.product ) |> Seq.contains p))
    |> Seq.map (fun d -> d.name)