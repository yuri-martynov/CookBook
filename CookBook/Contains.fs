module Contains

open Functions
open Types

let get (getDishById: getDishById) dishId (products: seq<string>) = async {
    let! dish = getDishById dishId
    let ingredients = 
        dish.ingredients
        |> Seq.filter (fun p -> products |> Seq.contains p.product.name )
        |> Seq.toList

    match ingredients with
    | [] -> 
        return "нет там такого"

    | _ -> 
        return 
            ingredients 
            |> Seq.map Format.ingredient 
            |> Format.list "<br/>"
    
}

