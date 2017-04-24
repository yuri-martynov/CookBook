module Contains

open Types

let private extractNames : (Ingredient -> seq<string>) =
    Utils.productsQuantities 
    >> Seq.map (fun pq -> pq.product |> Utils.productName)

let get (getDishById: getDishById) dishId (productNames: seq<string>) = async {
    let! dish = getDishById dishId
    let ingredients = 
        dish
        |> Utils.ingredients
        |> Seq.filter (fun i -> productNames |> Seq.exists (fun name -> i |> extractNames |> Seq.contains name ))
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

