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