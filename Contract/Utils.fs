module Utils

open Types

let rec products (ingredient: Ingredient) : ProductQuantity seq = 
    match ingredient with
    | Only p -> seq { yield p }
    | Optional p -> seq { yield p }
    | And ps | Xor ps -> 
        ps 
        |> Seq.map products 
        |> Seq.concat

let rec productName product =
    match product with
    |Whole p -> p
    |Part (p,_) -> productName p
    
let stepIngredients (step : Step) : Ingredient seq =
    match step with
    | Manual s -> s.ingredients
    | _ -> Seq.empty
    
let ingredients (dish : Dish) : Ingredient seq =
    dish.recipe.steps 
    |> Seq.map stepIngredients
    |> Seq.concat
    
let duration step = 
    match step with
    | Manual s -> s.duration
    | Process s -> s.duration