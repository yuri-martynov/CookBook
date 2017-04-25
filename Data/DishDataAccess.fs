module DishDataAccess

open System.Reflection
open Microsoft.FSharp.Reflection
open Types
open Dishes


let private all p s =
    not <| Seq.exists (not << p) s 


let letBindings<'t> (assembly: Assembly) : 't array =
    let publicTypes = assembly.GetExportedTypes ()
    let modules = publicTypes |> Array.filter FSharpType.IsModule
    let members = modules |> Array.collect (fun m -> m.GetMembers ())

    let valueOfBinding (mi : MemberInfo) =
        let property = mi.Name
        mi.DeclaringType.GetProperty(property).GetValue null

    members 
        |> Array.map valueOfBinding 
        |> Array.choose (fun o ->  match o with
                                    | :? 't as x -> Some x
                                    | _ -> None)


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