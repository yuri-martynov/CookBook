module Utils

open Types

let rec productsQuantities (ingredient: Ingredient) : ProductQuantity seq = 
    match ingredient with
    | Only p -> seq { yield p }
    | Optional p -> seq { yield p }
    | And ps | Xor ps -> 
        ps 
        |> Seq.collect productsQuantities

let rec productName product =
    match product with
    |Whole p -> p
    |Part (p,_) -> productName p
    
let stepIngredients (step : Step) : Ingredient seq =
    match step with
    | Manual s -> s.ingredients
    | _ -> Seq.empty

let sumQuantity (q : Quantity seq): Quantity =
    let rec sum' lst acc =
        match lst with
        | [] -> acc
        | h ::rest ->
            match (h, acc) with
            | (Value x, Value y) when x.unit = y.unit ->  
                { unit = x.unit
                ; value = x.value + y.value 
                } |> Value |> sum' rest 

            | (ToTaste, y) -> y
            | (x, ToTaste) -> x
            | _ -> failwith "can not sum up quantities"

    match q |> Seq.toList with
    | [] -> failwith "empty seq in sumQuantity"
    | h :: rest -> sum' rest h
    
let ingredients (dish : Dish) : Ingredient seq =
    dish.recipe.steps 
    |> Seq.collect stepIngredients
    |> Seq.collect productsQuantities
    |> Seq.groupBy (fun pq -> pq.product) 
    |> Seq.map (fun g -> Only {product = fst g; quantity = g |> snd |> Seq.map (fun pq -> pq.quantity) |> sumQuantity})
    
let duration step = 
    match step with
    | Manual s -> s.duration
    | Process s -> s.duration