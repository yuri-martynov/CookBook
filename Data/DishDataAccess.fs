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
            let type' = mi.DeclaringType
            if type' |> isNull then None
            else
                let name = mi.Name
                let prop = type'.GetProperty name
                if prop |> isNull then None
                else
                    match prop.GetValue null with
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
    |> Seq.map (fun d -> (d.name.ToLowerInvariant(), d))
    |> Map.ofSeq

let getById (id: string) = 
    dict |> Map.find (id.ToLowerInvariant())

let getByIngredients (productNames: seq<string>) : seq<string> =
    dishes
    |> Seq.filter (fun d -> productNames |> all (fun p -> d |> Utils.ingredients |> Seq.collect Utils.productsQuantities |> Seq.map (fun i -> Utils.productName i.product ) |> Seq.contains p))
    |> Seq.map (fun d -> d.name)