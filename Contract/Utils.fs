module Utils

open Types

let rec productsQuantities (ingredient: Ingredient) : ProductQuantity seq = 
    match ingredient with
    | Only p -> seq { yield p }
    | Optional p -> seq { yield p }
    | And ps | Xor ps -> 
        ps 
        |> Seq.map productsQuantities
        |> Seq.concat

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
            | (Grams x, Grams y) ->  x + y    |> Grams   |> sum' rest 
            | (Glasses x, Glasses y) -> x + y |> Glasses |> sum' rest
            | (Items x, Items y) -> x + y     |> Items   |> sum' rest
            | (Liters x, Liters y) -> x + y   |> Liters  |> sum' rest
            | (TeaSpoons x, TeaSpoons y) -> x + y |> TeaSpoons |> sum' rest
            | (TableSpoons x, TableSpoons y) -> x + y |> TableSpoons |> sum' rest
            | (ToTaste, y) -> y
            | (x, ToTaste) -> x
            | _ -> failwith "can not sum up quantities"

    match q |> Seq.toList with
    | [] -> failwith "empty seq in sumQuantity"
    | h :: rest -> sum' rest h
    
let ingredients (dish : Dish) : Ingredient seq =
    dish.recipe.steps 
    |> Seq.map stepIngredients
    |> Seq.concat
    |> Seq.map productsQuantities
    |> Seq.concat
    |> Seq.groupBy (fun pq -> pq.product) // by quantity type
    |> Seq.map (fun g -> Only {product = fst g; quantity = g |> snd |> Seq.map (fun pq -> pq.quantity) |> sumQuantity})
    
let duration step = 
    match step with
    | Manual s -> s.duration
    | Process s -> s.duration