module Contains

open Types

let private extractNames : (Ingredient -> seq<string>) =
    let rec name product =
        match product with
        |Whole p -> p
        |Part (p,_) -> name p

    Utils.products >> Seq.map (fun pq -> pq.product |> name)

let get (getDishById: getDishById) dishId (productNames: seq<string>) = async {
    let! dish = getDishById dishId
    let ingredients = 
        dish.ingredients
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

