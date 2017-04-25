module DishDataAccess

open System.Reflection
open Microsoft.FSharp.Reflection
open Types
open Dishes


let private all p s =
    not <| Seq.exists (not << p) s 


let letBindings<'t> (assembly: Assembly) : 't array =
    let valueOfBinding (mi : MemberInfo) =
        if mi |> isNull then None
        else
            let property = mi.Name
            match mi.DeclaringType.GetProperty(property).GetValue null with
            | :? 't as x -> Some x
            | _ -> None

    assembly.GetExportedTypes () 
        |> Array.filter FSharpType.IsModule
        |> Array.collect (fun m -> m.GetMembers ())
        |> Array.choose valueOfBinding


let private dishes : Dish array = 
    let assembly = typeof<Data.AssemblyInfo.MarkerType>.Assembly
    assembly |> letBindings<Dish> 

let private dict : Map<string, Dish> = 
    dishes
    |> Seq.map (fun d -> (d.name, d))
    |> Map.ofSeq

let getById id = 
    dict |> Map.find id

let getByIngredients (productNames: seq<string>) : seq<string> =
    dishes
    |> Seq.filter (fun d -> productNames |> all (fun p -> d |> Utils.ingredients |> Seq.collect Utils.productsQuantities |> Seq.map (fun i -> Utils.productName i.product ) |> Seq.contains p))
    |> Seq.map (fun d -> d.name)